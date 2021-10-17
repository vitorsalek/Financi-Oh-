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
    private float previousalue;
    private int turn;
    public GameObject handCards;
    public GameObject tableCards;
    public GameObject eventsCards;
    public Transform logContent;
    public GameObject logText;
    public GameObject logPos;
    private GameObject lastLog;
    public ScrollRect scrollRect;
    public EventCard curEventCard;
    public CardSetup selectedCard;
    public TextMeshProUGUI money_txt;
    public TMP_InputField inputValue_txt;
    public TextMeshProUGUI turn_txt;

    private float previousMoney;

    public bool isOnInput = false;

    // Start is called before the first frame update
    void Start() {
        turn = 0;
        turn_txt.text = "Mês: " + turn.ToString();
        previousMoney = money;
        GetEventCard();
        GetHandCards();
    }
    

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInvestmentButton()
    {
        if (money >= 0)
        {
            foreach (Transform cardTransform in tableCards.transform)
            {
                CardSetup card = cardTransform.GetComponent<CardSetup>();
                money += card.CalculateValue();
                scrollRect.verticalNormalizedPosition = 0;
                GameObject logTextInstance = Instantiate(logText, logContent);
                if (lastLog != null)
                    lastLog.SetActive(false);
                lastLog = logTextInstance;
                logTextInstance.transform.position = logPos.transform.position;
                logTextInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = previousMoney.ToString("F2");
                logTextInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = money.ToString("F2");
                TextMeshProUGUI diffText = logTextInstance.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                float diffValue = (money - previousMoney);
                diffText.text = diffValue <= 0 ? diffValue. ToString("F2") : "+" + diffValue.ToString("F2");
                if (diffValue <= 0)
                    diffText.color = Color.red;
                else
                    diffText.color = Color.green;
                money_txt.text = money.ToString("F2") + "$";
                card.resetCardValue();
                turn++;
                turn_txt.text = "Mês: " + turn.ToString();
            }
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

            selectedCard.value_txt.text = inputValue_txt.text + " $";
            selectedCard.value = int.Parse(inputValue_txt.text);
            money += selectedCard.previousValue;
            money -= selectedCard.value;
            selectedCard.previousValue = selectedCard.value;
            if (money >= 0)
            {
                money_txt.text = money.ToString("F2") + "$";
                money_txt.color = Color.white;
            }
            else
            {
                money_txt.text = money.ToString("F2") + "$";
                money_txt.color = Color.red;
            }
        }
    }
}
