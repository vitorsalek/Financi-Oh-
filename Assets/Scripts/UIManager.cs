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
    public Color colorRed;
    public Color colorGreen;
    public DropMoneyParticle moneyParticle;
    public TextMeshProUGUI goal_txt;


    public void SetTurnText(int turn)
    {
        turn_txt.text = "Mês: " + turn.ToString();
    }
    public void SetMoneyText(float money, Color color)
    {
        money_txt.text = money.ToString("F2") + "$";
        if (color != Color.clear)
            money_txt.color = color;
    }

    public void SetDiffText(TextMeshProUGUI diffText, float diffValue)
    {
        diffText.text = diffValue <= 0 ? diffValue.ToString("F2") : "+" + diffValue.ToString("F2");
        diffText.color = diffValue < 0 ? colorRed : colorGreen;

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
        if (diffValue > 0)
        {
            AudioManager.current.Play("Dinheiro");
            int moneyDroped = Random.Range(4, 6);
            moneyParticle.moneyToDrop = moneyDroped;
            moneyParticle.dropMoney();
        }
        else if (diffValue < 0)
            AudioManager.current.Play("PerdeuDinheiro");
        else
            AudioManager.current.Play("Open");
        SetDiffText(diffText, diffValue);

    }

    public void RefreshGoalUI(GoalSystem.Goal newGoal)
    {
        goal_txt.text = $"Meta: {newGoal.goalAmount}$ até o mês {newGoal.turn}";
    }

    public void OpenCloseSettings(GameObject canvas)
    {
        if (!canvas.activeInHierarchy)
            canvas.gameObject.SetActive(true);
        else
            canvas.gameObject.SetActive(false);

    }
}
