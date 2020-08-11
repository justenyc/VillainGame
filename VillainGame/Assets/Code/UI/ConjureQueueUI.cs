using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConjureQueueUI : MonoBehaviour
{
    public static ConjureQueueUI instance;
    Text[] uiTexts;

    private void Start()
    {
        if (instance == null)
            instance = this;

        uiTexts = GetComponentsInChildren<Text>();
    }

    public void UpdateQueue()
    {
        for (int i = 0; i < uiTexts.Length; i++)
        {
            try
            {
                uiTexts[i].text = Cursor.cursor.conjureQueue[i];
                ChangeColour(Cursor.cursor.conjureQueue[i], i);
            }
            catch
            {
                uiTexts[i].text = "";
                ChangeColour("", i);
            }
        }
    }

    void ChangeColour(string element, int index)
    {
        switch(element)
        {
            case "Lightning":
                uiTexts[index].GetComponentInParent<Image>().color = new Color(255, 0, 255);
                break;

            case "Fire":
                uiTexts[index].GetComponentInParent<Image>().color = new Color(255, 198, 0);
                break;

            case "Earth":
                uiTexts[index].GetComponentInParent<Image>().color = Color.gray;
                break;

            case "Water":
                uiTexts[index].GetComponentInParent<Image>().color = new Color(0, 255, 255);
                break;

            default:
                uiTexts[index].GetComponentInParent<Image>().color = Color.white;
                break;
        }
    }
}
