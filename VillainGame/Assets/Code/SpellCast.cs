using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpellCast : MonoBehaviour
{
    public int fireIntensity, waterIntensity, earthIntensity, lightningIntensity, mudIntensity, burstIntensity, vacuumIntensity, lavaIntensity;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void BeginCasting(List<string> elements)
    {
        List<string> enforcedElements;
        enforcedElements = elements;
        CheckIntensity(enforcedElements);
    }

    void Cast(string element)
    {
        Instantiate(vfxHolder.MagicPrefabs[element], transform.position, Quaternion.identity);
    }

    void CheckIntensity(List<string> enforcedElements)
    {
        foreach (string s in enforcedElements)
        {
            switch (s)
            {
                case "Fire":
                    fireIntensity++;
                    break;

                case "Water":
                    waterIntensity++;
                    break;

                case "Earth":
                    earthIntensity++;
                    break;

                case "Lightning":
                    lightningIntensity++;
                    break;

                case "Burst":
                    burstIntensity++;
                    break;

                case "Vacuum":
                    vacuumIntensity++;
                    break;

                case "Lava":
                    lavaIntensity++;
                    break;

                case "Mud":
                    mudIntensity++;
                    break;
            }
        }
        MakeDistinct(enforcedElements);
    }

    void MakeDistinct(List<string> enforcedElements)
    {
        List<string> distinctElements = enforcedElements.Distinct().ToList();

        for (int i = 0; i < distinctElements.Count; i++)
        {
            Cast(distinctElements[i]);
        }
    }
}
