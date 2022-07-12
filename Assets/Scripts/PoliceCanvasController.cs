using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoliceCanvasController : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(Camera.main.transform);
        this.transform.rotation = Quaternion.Euler(45,45,0);
    }

    public void setTextDrinkSold()
    {
        text.SetText("Drink Sold!");
        text.color = Color.green;
    }

    public void setTextAlcoholMade()
    {
        text.SetText("Drink Produced!");
        text.color = Color.yellow;
    }
}
