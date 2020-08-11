using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpellCast : MonoBehaviour
{
    public int fireIntensity, waterIntensity, earthIntensity, lightningIntensity, mudIntensity, burstIntensity, vacuumIntensity, lavaIntensity;

    public void BeginCasting(List<string> elements)
    {
        List<string> enforcedElements;
        enforcedElements = elements;
        InitializeIntensity(enforcedElements);
    }

    void Cast(string element)
    {
        Instantiate(vfxHolder.MagicPrefabs[element], transform.position, Quaternion.identity);
    }

    void AddIntensity(string s)
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

    void InitializeIntensity(List<string> enforcedElements)
    {
        //Iterate through enforcedElement list where i is the pointer
        for (int i = 0; i < enforcedElements.Count; i++)
        {
            //Check the element at the pointer and see if it's not Burst or Vacuum
            switch (enforcedElements[i] != "Burst" && enforcedElements[i] != "Vacuum")
            {
                //If the element at the pointer is not Burst or Vacuum then pause while a second pointer
                //to iterate down the list from where the first pointer is in order to find the end of the list
                //or until we find a different element
                case true:
                    //Start the second pointer on where the first pointer is pointing and interate down
                    //until the list ends or the second pointer finds a different element string and then
                    //put use the element at the first pointer to cast the spell
                    for (int j = i; j < enforcedElements.Count; j++)
                    {
                        if (enforcedElements[j] == enforcedElements[i])
                        {
                            AddIntensity(enforcedElements[j]);

                            try
                            {
                                if (enforcedElements[j + 1] != enforcedElements[i])
                                {
                                    Cast(enforcedElements[i]);
                                    i = j;
                                    ResetIntensity();
                                }
                            }
                            catch
                            {
                                if (j + 1 >= enforcedElements.Count)
                                {
                                    Cast(enforcedElements[i]);
                                    i = j;
                                    ResetIntensity();
                                }
                            }
                        }
                    }
                    break;

                case false:
                    switch (enforcedElements[i])
                    {
                        case "Burst":
                            AddIntensity(enforcedElements[i]);
                            int next = EnforceBurstRules(enforcedElements, i);
                            i = next;
                            ResetIntensity();
                            break;

                        case "Vacuum":
                            AddIntensity(enforcedElements[i]);
                            next = EnforceVacuumRules(enforcedElements, i);
                            i = next;
                            ResetIntensity();
                            break;
                    }
                    break;
            }
        }
    }

    int EnforceBurstRules(List<string> enforcedElements, int pointer)
    {
        try
        {
            if (enforcedElements[pointer + 1] != "Burst" && enforcedElements[pointer + 1] != "Vacuum")
            {
                enforcedElements[pointer] = enforcedElements[pointer] + enforcedElements[pointer + 1];

                for (int j = pointer + 1; j < enforcedElements.Count; j++)
                {
                    if (enforcedElements[j] == enforcedElements[pointer + 1])
                    {
                        AddIntensity(enforcedElements[pointer]);

                        try
                        {
                            if (enforcedElements[j + 1] != enforcedElements[pointer + 1])
                            {
                                Cast(enforcedElements[pointer]);
                                ResetIntensity();
                                return j;
                            }
                        }
                        catch
                        {
                            if (j + 1 >= enforcedElements.Count)
                            {
                                Cast(enforcedElements[pointer]);
                                ResetIntensity();
                                return j;
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            Cast(enforcedElements[pointer]);
            return pointer;
        }
        Cast(enforcedElements[pointer]);
        return pointer;
    }

    int EnforceVacuumRules(List<string> enforcedElements, int pointer)
    {
        try
        {
            if (enforcedElements[pointer + 1] != "Burst" || enforcedElements[pointer + 1] != "Vacuum")
            {
                for (int j = pointer + 1; j < enforcedElements.Count; j++)
                {
                    if (enforcedElements[j] == enforcedElements[pointer + 1])
                    {
                        AddIntensity(enforcedElements[j]);
                        AddIntensity(enforcedElements[j]);

                        try
                        {
                            if (enforcedElements[j + 1] != enforcedElements[pointer])
                            {
                                Cast(enforcedElements[pointer]);
                                ResetIntensity();
                                return j;
                            }
                        }
                        catch
                        {
                            if (j + 1 >= enforcedElements.Count)
                            {
                                Cast(enforcedElements[pointer]);
                                ResetIntensity();
                                return j;
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            Cast(enforcedElements[pointer]);
            return pointer;
        }
        Cast(enforcedElements[pointer]);
        return pointer;
    }

    void ResetIntensity()
    {
        //DebugIntensity();
        fireIntensity = 0;
        waterIntensity = 0;
        lightningIntensity = 0;
        earthIntensity = 0;
        mudIntensity = 0;
        lavaIntensity = 0;
        burstIntensity = 0;
        vacuumIntensity = 0;
    }

    void DebugIntensity()
    {
        Debug.Log("fireIntesity = " + fireIntensity);
        Debug.Log("waterIntensity = " + waterIntensity);
        Debug.Log("lightningIntensity = " + lightningIntensity);
        Debug.Log("earthIntensity = " + earthIntensity);
        Debug.Log("musIntensity = " + mudIntensity);
        Debug.Log("lavaIntensity = " + lavaIntensity);
        Debug.Log("burstIntensity = " + burstIntensity);
        Debug.Log("vacuumIntensity = " + vacuumIntensity);
    }
    /*
    void CheckVacuumBurst(List<string> enforcedElements)
    {
        List<int> removeIndexesAfterBursts = new List<int>();

        for (int i = 0; i < enforcedElements.Count; i++)
        {
            if (enforcedElements[i] == "Vacuum")
            {
                try
                {
                    DoubleIntensity(enforcedElements[i + 1]);
                }
                catch
                {

                }
            }
            else if (enforcedElements[i] == "Burst")
            {
                try
                {
                    if (enforcedElements[i + 1] != "Burst" && enforcedElements[i + 1] != "Vacuum")
                    {
                        enforcedElements[i] = enforcedElements[i] + enforcedElements[i + 1];
                        for (int j = i + 1; j < enforcedElements.Count; j++)
                        {
                            if (enforcedElements[j] == enforcedElements[i + 1])
                            {
                                removeIndexesAfterBursts.Add(j);
                            }
                            else
                                break;
                        }
                    }
                }
                catch
                {

                }
            }
        }

        MakeDistinct(enforcedElements);
    }

    void MakeDistinct(List<string> enforcedElements)
    {
        List<string> distinctElements = enforcedElements.Distinct().ToList();

        CheckVacuumBurst(distinctElements);
    }

    void DoubleIntensity(string s)
    {
        switch (s)
        {
            case "Fire":
                fireIntensity *= 2;
                break;

            case "Water":
                waterIntensity *= 2;
                break;

            case "Earth":
                earthIntensity *= 2;
                break;

            case "Lightning":
                lightningIntensity *= 2;
                break;

            case "Burst":
                burstIntensity *= 2;
                break;

            case "Vacuum":
                vacuumIntensity *= 2;
                break;

            case "Lava":
                lavaIntensity *= 2;
                break;

            case "Mud":
                mudIntensity *= 2;
                break;
        }
    }

    List<int> EnforceBurstRules(List<string> enforcedElements)
    {
        List<int> temp = new List<int>();
        for (int i = 0; i < enforcedElements.Count; i++)
        {
            if (enforcedElements[i] == "Burst")
            {
                try
                {
                    if (enforcedElements[i + 1] != "Burst" && enforcedElements[i + 1] != "Vacuum")
                    {
                        enforcedElements[i] = enforcedElements[i] + enforcedElements[i + 1];
                        for (int j = i + 1; j < enforcedElements.Count; j++)
                        {
                            if (enforcedElements[j] == enforcedElements[i + 1])
                            {
                                temp.Add(j);
                            }
                            else
                                break;
                        }
                    }
                }
                catch
                {

                }
            }
        }
        return temp;
    }*/
}
