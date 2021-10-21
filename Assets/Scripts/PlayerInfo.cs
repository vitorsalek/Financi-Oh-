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
    public EventCard curEventCard;
    public CardSetup selectedCard;

    private float previousMoney;

    public bool isOnInput = false;

    private UIManager uiManager;

    // Start is called before the first frame update
    void Start() {
        turn = 0;
        uiManager = GetComponent<UIManager>();
        uiManager.SetTurnText(turn);
        previousMoney = money;
        GetEventCard();
        GetHandCards();
    }

    public void OnInvestmentButton()
    {
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
            ResetCards();
            GetHandCards();
            previousMoney = money;
        }
        else
        {
            print("dinheiro insuficiente");
        }
    }


    private void GetEventCard()
    {
        if (curEventCard != null)
            curEventCard.gameObject.SetActive(false);
        int i = Random.Range(0, eventsCards.transform.childCount);
        EventCard eventCard = eventsCards.transform.GetChild(i).GetComponent<EventCard>();
        while (eventCard == curEventCard)
        {
            i = Random.Range(0, eventsCards.transform.childCount);
            eventCard = eventsCards.transform.GetChild(i).GetComponent<EventCard>();
        }

        curEventCard = eventCard;
        curEventCard.gameObject.SetActive(true);
    }
    private void ResetCards()
    {
        int childCount = tableCards.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform card = tableCards.transform.GetChild(0);
            card.SetParent(handCards.transform);
            card.gameObject.SetActive(false);
        }
        foreach (Transform card in handCards.transform)
        {
            if (card.gameObject.activeInHierarchy)
            {
                card.gameObject.SetActive(false);
            }
        }
    }
    private void GetHandCards()
    {
        int counter = 0;
        while (counter < 4)
        {
            int i = Random.Range(0, handCards.transform.childCount);
            Transform handCard = handCards.transform.GetChild(i);
            if (!handCard.gameObject.activeInHierarchy)
            {
                handCard.gameObject.SetActive(true);
                counter++;
            }
        }
    }

    public void OnInputChange()
    {
        if (selectedCard != null)
        {
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
        else
            uiManager.inputValue_txt.text = "";
    }
    public void OnInputDeselect()
    {
        StartCoroutine(SetInputEmpty());
    }
    public IEnumerator SetInputEmpty()
    {
        yield return new WaitForSeconds(0.1f);
        uiManager.inputValue_txt.text = "0";
    }
}
