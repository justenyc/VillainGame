using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MagicZoneUI : MonoBehaviour
{
    public Image[] elementIndicatorsArray;
    Dictionary<string, Image> elementIndicators = new Dictionary<string, Image>();

    public void Initialize()
    {
        foreach (Image img in elementIndicatorsArray)
        {
            elementIndicators.Add(img.name, img);
        }
    }

    public void AddUiElement(string element)
    {
        Instantiate(elementIndicators[element], transform.position, Quaternion.identity, this.transform);
    }

    public void RemoveUiElement(string element)
    {
        foreach (Image img in GetComponentsInChildren<Image>())
        {
            if (img.name == element)
            {
                Destroy(img);
                break;
            }
        }
    }
}
