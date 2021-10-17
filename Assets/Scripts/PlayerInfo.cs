using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum CardCategorie { tecnology, agro, imobi };
public enum CardRegion { blue, orange, green };
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
                money_txt.text = money.ToString() + "$";
                card.resetCardValue();
            }
            GetEventCard();
            ResetCards();
            GetHandCards();
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
                money_txt.text = money.ToString() + "$";
                money_txt.color = Color.white;
            }
            else
            {
                money_txt.text = money.ToString() + "$";
                money_txt.color = Color.red;
            }
        }
    }

    public void OnInputDeselect()
    {
        previousValue = 0;
    }
}
