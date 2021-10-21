using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CardCategorie { tecnology, agro, imobi };
public enum CardRegion { blue, orange, green };
public class PlayerInfo : MonoBehaviour
{
    public float money;
    private int turn;
    public GameObject handCards;
    public GameObject tableCards;
    public GameObject eventsCards;
    [HideInInspector]public EventCard curEventCard;
    [HideInInspector]public CardSetup selectedCard;

    private float previousMoney;

    public bool isOnInput = false;
    bool investButtonCooldown = false;
    bool deselected = false;

    private UIManager uiManager;
    private GoalSystem goalSystem;
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        turn = 0;
        audioManager = GameObject.FindGameObjectWithTag("persistentData").GetComponent<AudioManager>();
        goalSystem = GetComponent<GoalSystem>();
        uiManager = GetComponent<UIManager>();
        uiManager.SetTurnText(turn);
        previousMoney = money;
        GetEventCard();
        GetHandCards(0.1f);
        uiManager.SetMoneyText(money, Color.clear);
    }

    public void OnInvestmentButton()
    {
        if (!investButtonCooldown)
        {
            investButtonCooldown = true;
            StartCoroutine(InvestButtonCooldownTimer());
            if (money >= 0)
            {
                foreach (Transform cardTransform in tableCards.transform)
                {
                    CardSetup card = cardTransform.GetComponent<CardSetup>();
                    money += card.CalculateValue();
                    uiManager.InstantiateLogText(money, previousMoney);
                    uiManager.SetMoneyText(money, Color.clear);
                    card.resetCardValue();
                }
                turn++;
                uiManager.SetTurnText(turn);
                GetEventCard();
                DesactivateCards();
                GetHandCards(0.3f);
                previousMoney = money;
                AudioManager.current.Play("NextRound");
            }
            else
            {
                print("dinheiro insuficiente");
            }
        }
    }

    private void GetEventCard()
    {
        if (curEventCard != null)
        {
            curEventCard.GetComponent<Animator>().Play("DesactivateEventCard");
            StartCoroutine(ChangeCardState(curEventCard.transform, 0.25f, false));
        }
        int i = Random.Range(0, eventsCards.transform.childCount);
        EventCard eventCard = eventsCards.transform.GetChild(i).GetComponent<EventCard>();
        while (eventCard == curEventCard)
        {
            i = Random.Range(0, eventsCards.transform.childCount);
            eventCard = eventsCards.transform.GetChild(i).GetComponent<EventCard>();
        }
        
        curEventCard = eventCard;
        StartCoroutine(ChangeCardState(curEventCard.transform, 0.5f, true));
    }
    private void DesactivateCards()
    {
        int childCount = tableCards.transform.childCount;
        List<Transform> tablecards = new List<Transform>();
        for (int i = 0; i < childCount; i++)
        {
            Transform card = tableCards.transform.GetChild(i);
            card.GetComponent<Animator>().Play("DesactivateCard");
            tablecards.Add(card);
        }
        StartCoroutine(DesactivateTableCards(tablecards, 0.25f));
        foreach (Transform card in handCards.transform)
        {
            if (card.gameObject.activeInHierarchy)
            {
                card.GetComponent<Animator>().Play("DesactivateCard");
                StartCoroutine(ChangeCardState(card, 0.25f, false));
            }
        }
    }

    private void GetHandCards(float time)
    {
        int counter = 0;
        List<Transform> cardsToActivate = new List<Transform>();
        while (counter < 4)
        {
            int i = Random.Range(0, handCards.transform.childCount);
            Transform handCard = handCards.transform.GetChild(i);

            if (!handCard.gameObject.activeInHierarchy && !cardsToActivate.Contains(handCard))
            {
                cardsToActivate.Add(handCard);
                counter++;
            }
        }
        foreach (Transform card in cardsToActivate)
        {
            StartCoroutine(ChangeCardState(card, time, true));
        }
    }

    public void OnInputChange()
    {
        if (selectedCard != null && !deselected)
        {
            //if (selectedCard.value)
            selectedCard.value_txt.text = uiManager.inputValue_txt.text + " $";
            selectedCard.value = int.Parse(uiManager.inputValue_txt.text);
            money += selectedCard.previousValue;
            money -= selectedCard.value;
            selectedCard.previousValue = selectedCard.value;
            if (money >= 0)
            {
                uiManager.SetMoneyText(money, Color.white);
            }
            else
            {
                uiManager.SetMoneyText(money, Color.red);
            }
        }
    }
    public void OnInputDeselect()
    {
        deselected = true;
        StartCoroutine(SetInputEmpty());
    }

    private IEnumerator DesactivateTableCards(List<Transform> cards, float time)
    {
        yield return new WaitForSeconds(time);
        foreach (Transform card in cards)
        {
            print("parente desliga " + card.gameObject.name);
            card.SetParent(handCards.transform);
            card.gameObject.SetActive(false);
        }
        
    }
    private IEnumerator ChangeCardState(Transform card, float time, bool state)
    {
        yield return new WaitForSeconds(time);
        card.gameObject.SetActive(state);
    }

    public IEnumerator SetInputEmpty()
    {
        yield return new WaitForSeconds(0.1f);
        uiManager.inputValue_txt.text = "0";
        deselected = false;
    }
    public IEnumerator InvestButtonCooldownTimer()
    {
        yield return new WaitForSeconds(0.5f);
        investButtonCooldown=false;
    }
}
