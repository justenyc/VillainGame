using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpell : MonoBehaviour
{
    int spellIntensity;
    string elementType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActualizeSpell(int intensity, string element)
    {
        spellIntensity = intensity;
        elementType = element;
    }
}
