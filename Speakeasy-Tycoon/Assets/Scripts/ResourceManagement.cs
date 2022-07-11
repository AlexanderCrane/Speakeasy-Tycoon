using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManagement : MonoBehaviour
{
    public float money = 100f;

    public float totalCosts = 0f;

    public float totalIncome = 0f;
    public float moneyMakingInterval;

    public TextMeshProUGUI moneyText;

    public List<GameObject> speakeasyList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateMoneyAfterTime());
        moneyText.text = "Money: $" + money.ToString("F2");
    }

    public void MakeMoney(float value)
    {
        money = money + value;
        UpdateMoneyText();
    }

    public void SpendMoney(float value)
    {
        money = money - value;
        UpdateMoneyText();
    }

    public void ChangeTotalCosts(float change)
    {
        totalCosts = totalCosts + change;
    }

    public void ChangeTotalIncome(float change)
    {
        totalIncome = totalIncome + change;
    }

    private void UpdateMoneyText()
    {
        if(money > 0f)
        {
            moneyText.color = new Vector4(0,255,0,255);
        }
        else
        {
            moneyText.color = new Vector4(255,0,0,255);
        }
        moneyText.text = "Money: $" + money.ToString("F2");
    }

    private IEnumerator UpdateMoneyAfterTime()
    {
        yield return new WaitForSeconds(moneyMakingInterval);
        SpendMoney(totalCosts);
        MakeMoney(totalIncome);
        StartCoroutine(UpdateMoneyAfterTime());
    }
    
}
