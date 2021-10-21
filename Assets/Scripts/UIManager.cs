using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI money_txt;
    public TMP_InputField inputValue_txt;
    public TextMeshProUGUI turn_txt;
    private GameObject lastLog;
    public Transform logPos;
    public Transform logContent;
    public GameObject logText;
    public ScrollRect scrollRect;

    public void SetTurnText(int turn)
    {
        turn_txt.text = "Mês: " + turn.ToString();
    }
    public void SetMoneyText(float money, Color color)
    {
        print(money);
        money_txt.text = money.ToString("F2") + "$";
        print(money_txt.text);
        if (color != Color.clear)
            money_txt.color = color;
        print("foi");
    }

    public void SetDiffText(TextMeshProUGUI diffText, float diffValue)
    {
        diffText.text = diffValue <= 0 ? diffValue.ToString("F2") : "+" + diffValue.ToString("F2");
        diffText.color = diffValue <= 0 ? Color.red : Color.green;
    }

    public void InstantiateLogText(float money, float previousMoney)
    {
        scrollRect.verticalNormalizedPosition = 0;
        GameObject logTextInstance = Instantiate(logText, logContent);
        if (lastLog != null)
            lastLog.SetActive(false);
        lastLog = logTextInstance;
        logTextInstance.transform.position = logPos.position;
        logTextInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = previousMoney.ToString("F2");
        logTextInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = money.ToString("F2");
        TextMeshProUGUI diffText = logTextInstance.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        float diffValue = (money - previousMoney);
        SetDiffText(diffText, diffValue);
    }
}
