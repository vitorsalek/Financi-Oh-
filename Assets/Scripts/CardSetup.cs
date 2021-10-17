using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class CardSetup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardCategorie myCategorie;
    public PlayerInfo playerInfo;
    public TextMeshProUGUI value_txt;
    [HideInInspector]public float value;
    [HideInInspector] public float previousValue;
    public GameObject highLight;
    [HideInInspector] public bool mouseInside = false;
    public float valorization;
    public float desvalorization;
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseInside = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && mouseInside)
        {
            highLight.SetActive(true);
            playerInfo.selectedCard = this;
        }
        else if (Input.GetMouseButtonDown(0) && !mouseInside && !playerInfo.isOnInput)
        {
            highLight.SetActive(false);
            if (playerInfo.selectedCard == this)
                playerInfo.selectedCard = null;
        }
    }

    public void resetCardValue()
    {
        value = 0;
        previousValue = 0;
    }

    public float CalculateValue()
    {
        return value*1.1f;
    }
}
