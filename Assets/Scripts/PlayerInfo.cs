using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public float money;
    private float previousValue;
    public GameObject handCards;
    public GameObject tableCards;
    public GameObject eventsCards;
    public EventCard curEventCard;
    public CardSetup selectedCard;
    public TextMeshProUGUI money_txt;
    public TMP_InputField inputValue_txt;

    public bool isOnInput = false;

    // Start is called before the first frame update
    void Start() {
        GetEventCard();
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
                money_txt.text = money.ToString();
            }
            GetEventCard();
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

    }
    private void GetHandCards()
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

    public void OnInputChange()
    {
        if (selectedCard != null)
        {
            
            selectedCard.value_txt.text = inputValue_txt.text;
            selectedCard.value = int.Parse(inputValue_txt.text);
            money += selectedCard.previousValue;
            money -= selectedCard.value;
            selectedCard.previousValue = selectedCard.value;
            if (money >= 0)
            {
                money_txt.text = money.ToString();
                money_txt.color = Color.white;
            }
            else
            {
                money_txt.text = money.ToString();
                money_txt.color=Color.red;
            }
        }
    }

    public void OnInputDeselect()
    {
        previousValue = 0;
    }
}
