using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingUIContentController : MonoBehaviour
{
    public bool isDemoMode = false;
    public GameObject textContentPrefab;
    List<GameObject> textContentObjects = new List<GameObject>();

    GameObject newObject;

    // Start is called before the first frame update
    void Start()
    {
        newObject = Instantiate(textContentPrefab, this.transform.position, this.transform.rotation);
        newObject.transform.SetParent(this.transform);
        newObject.transform.localScale = Vector3.one;

        if(isDemoMode)
        {
            fillContent(new List<string>{"Dummy Content: 1", "Other Dummy Content: 2", "Some Other Content: 3"});
        }
    }

    public void fillContent(List<string> contents)
    {
        // wipeContent();
        
        string displayText = "";
        foreach(string content in contents)
        {
            displayText += content + "\n";
        }
        newObject.GetComponent<TextMeshProUGUI>().SetText(displayText);
        textContentObjects.Add(newObject);
    }

    public void wipeContent()
    {
        foreach(GameObject textContentObject in textContentObjects)
        {
            // textContentObjects.Remove(textContentObject);
            Destroy(textContentObject);
        }
    }
}
