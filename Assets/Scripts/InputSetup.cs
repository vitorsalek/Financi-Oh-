using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputSetup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PlayerInfo playerInfo;
    public void OnPointerEnter(PointerEventData eventData)
    {
        playerInfo.isOnInput = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        playerInfo.isOnInput = false;
    }

}
