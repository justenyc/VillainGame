using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicZone : MonoBehaviour
{
    [SerializeField]
    float timer = 5.5f;

    [SerializeField]
    List<string> elements;

    [SerializeField]
    MagicZoneUI mzUi;

    // Start is called before the first frame update
    void Start()
    {
        if (elements == null)
        {
            elements = new List<string>();
        }

        mzUi = this.GetComponentInChildren<MagicZoneUI>();
        mzUi.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            this.GetComponent<SpellCast>().BeginCasting(elements);
            Destroy(this.gameObject);
        }
    }

    void AddElements(string element)
    {
        elements.Add(element);

        switch (element)
        {
            case "Fire":
                Instantiate(vfxHolder.MagicSparklePrefabs["FireSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation, this.transform);
                break;

            case "Water":
                Instantiate(vfxHolder.MagicSparklePrefabs["WaterSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation, this.transform);
                break;

            case "Earth":
                Instantiate(vfxHolder.MagicSparklePrefabs["EarthSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation, this.transform);
                break;

            case "Lightning":
                Instantiate(vfxHolder.MagicSparklePrefabs["LightningSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation, this.transform);
                break;

            case "Curse":
                Instantiate(vfxHolder.MagicSparklePrefabs["CurseSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation, this.transform);
                break;
        }
    }

    void CheckCombination(string nextElement)
    {
        switch (nextElement)
        {
            case "Fire":
                if (elements[elements.Count - 1] == "Lightning")
                {
                    mzUi.RemoveUiElement(elements[elements.Count - 1]);
                    elements[elements.Count - 1] = "Burst";
                    Sparkle[] Sparkles = GetComponentsInChildren<Sparkle>();
                    Instantiate(vfxHolder.MagicSparklePrefabs["BurstSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation, this.transform);
                    Destroy(Sparkles[elements.Count - 1].gameObject);
                    mzUi.AddUiElement(elements[elements.Count - 1]);
                    break;
                }
                else if (elements[elements.Count - 1] == "Earth")
                {
                    mzUi.RemoveUiElement(elements[elements.Count - 1]);
                    elements[elements.Count - 1] = "Lava";
                    Sparkle[] Sparkles = GetComponentsInChildren<Sparkle>();
                    Instantiate(vfxHolder.MagicSparklePrefabs["LavaSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation, this.transform);
                    Destroy(Sparkles[elements.Count - 1].gameObject);
                    mzUi.AddUiElement(elements[elements.Count - 1]);
                    break;
                }
                else
                {
                    AddElements(nextElement);
                }
                break;

            case "Water":
                if (elements[elements.Count - 1] == "Lightning")
                {
                    mzUi.RemoveUiElement(elements[elements.Count - 1]);
                    elements[elements.Count - 1] = "Vacuum";
                    Sparkle[] Sparkles = GetComponentsInChildren<Sparkle>();
                    Instantiate(vfxHolder.MagicSparklePrefabs["VacuumSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation, this.transform);
                    Destroy(Sparkles[elements.Count - 1].gameObject);
                    mzUi.AddUiElement(elements[elements.Count - 1]);
                    break;
                }
                else if (elements[elements.Count - 1] == "Earth")
                {
                    mzUi.RemoveUiElement(elements[elements.Count - 1]);
                    elements[elements.Count - 1] = "Mud";
                    Sparkle[] Sparkles = GetComponentsInChildren<Sparkle>();
                    Instantiate(vfxHolder.MagicSparklePrefabs["MudSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation, this.transform);
                    Destroy(Sparkles[elements.Count - 1].gameObject);
                    mzUi.AddUiElement(elements[elements.Count - 1]);
                    break;
                }
                else
                {
                    AddElements(nextElement);
                }
                break;

            case "Earth":

                if (elements[elements.Count - 1] == "Fire")
                {
                    mzUi.RemoveUiElement(elements[elements.Count - 1]);
                    elements[elements.Count - 1] = "Lava";
                    Sparkle[] Sparkles = GetComponentsInChildren<Sparkle>();
                    Instantiate(vfxHolder.MagicSparklePrefabs["LavaSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation, this.transform);
                    Destroy(Sparkles[elements.Count - 1].gameObject);
                    mzUi.AddUiElement(elements[elements.Count - 1]);
                    break;
                }
                else if (elements[elements.Count - 1] == "Water")
                {
                    mzUi.RemoveUiElement(elements[elements.Count - 1]);
                    elements[elements.Count - 1] = "Mud";
                    Sparkle[] Sparkles = GetComponentsInChildren<Sparkle>();
                    Instantiate(vfxHolder.MagicSparklePrefabs["MudSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation, this.transform);
                    Destroy(Sparkles[elements.Count - 1].gameObject);
                    mzUi.AddUiElement(elements[elements.Count - 1]);
                    break;
                }
                else
                {
                    AddElements(nextElement);
                }
                break;

            case "Lightning":
                if (elements[elements.Count - 1] == "Fire")
                {
                    mzUi.RemoveUiElement(elements[elements.Count - 1]);
                    elements[elements.Count - 1] = "Burst";
                    Sparkle[] Sparkles = GetComponentsInChildren<Sparkle>();
                    Instantiate(vfxHolder.MagicSparklePrefabs["BurstSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation, this.transform);
                    Destroy(Sparkles[elements.Count - 1].gameObject);
                    mzUi.AddUiElement(elements[elements.Count - 1]);
                    break;
                }
                else if (elements[elements.Count - 1] == "Water")
                {
                    mzUi.RemoveUiElement(elements[elements.Count - 1]);
                    elements[elements.Count - 1] = "Vacuum";
                    Sparkle[] Sparkles = GetComponentsInChildren<Sparkle>();
                    Instantiate(vfxHolder.MagicSparklePrefabs["VacuumSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation, this.transform);
                    Destroy(Sparkles[elements.Count - 1].gameObject);
                    mzUi.AddUiElement(elements[elements.Count - 1]);
                    break;
                }
                else
                {
                    AddElements(nextElement);
                }
                break;
        }
    }

    void CheckNegation(string nextElement)
    {
        switch (nextElement)
        {
            case "Fire":
                bool negationFound = false;
                for (int i = elements.Count - 1; i > -1; i--)
                {
                    if (elements[i] == "Water")
                    {
                        mzUi.RemoveUiElement(elements[i]);
                        elements.RemoveAt(i);
                        Sparkle[] Sparkles = GetComponentsInChildren<Sparkle>();
                        Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position + new Vector3 (0,0.5f,0), this.transform.rotation);
                        Destroy(Sparkles[i].gameObject);
                        negationFound = true;
                        break;
                    }
                }

                if (negationFound == false)
                {
                    CheckCombination(nextElement);
                }
                break;

            case "Water":
                negationFound = false;
                for (int i = elements.Count - 1; i > -1; i--)
                {
                    if (elements[i] == "Fire")
                    {
                        mzUi.RemoveUiElement(elements[i]);
                        elements.RemoveAt(i);
                        Sparkle[] Sparkles = GetComponentsInChildren<Sparkle>();
                        Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position + new Vector3 (0,0.5f,0), this.transform.rotation);
                        Destroy(Sparkles[i].gameObject);
                        negationFound = true;
                        break;
                    }
                }

                if (negationFound == false)
                {
                    CheckCombination(nextElement);
                }
                break;

            case "Earth":
                negationFound = false;
                for (int i = elements.Count - 1; i > -1; i--)
                {
                    if (elements[i] == "Lightning")
                    {
                        mzUi.RemoveUiElement(elements[i]);
                        elements.RemoveAt(i);
                        Sparkle[] Sparkles = GetComponentsInChildren<Sparkle>();
                        Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position + new Vector3 (0,0.5f,0), this.transform.rotation);
                        Destroy(Sparkles[i].gameObject);
                        negationFound = true;
                        break;
                    }
                }

                if (negationFound == false)
                {
                    CheckCombination(nextElement);
                }
                break;

            case "Lightning":
                negationFound = false;
                for (int i = elements.Count - 1; i > -1; i--)
                {
                    if (elements[i] == "Earth")
                    {
                        mzUi.RemoveUiElement(elements[i]);
                        elements.RemoveAt(i);
                        Sparkle[] Sparkles = GetComponentsInChildren<Sparkle>();
                        Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position + new Vector3 (0,0.5f,0), this.transform.rotation);
                        Destroy(Sparkles[i].gameObject);
                        negationFound = true;
                        break;
                    }
                }

                if (negationFound == false)
                {
                    CheckCombination(nextElement);
                }
                break;
        }
    }

    public void MagicEnforcer(string element)
    {
        if (element == "Curse")
        {
            AddElements(element);
        }
        else
        {
            string nextElement = element;

            if (elements.Count < 1)
            {
                AddElements(nextElement);
            }
            else
            {
                CheckNegation(nextElement);
            }
        }
    }

    public void InitializeElementsList(string first)
    {
        elements = new List<string>();
        mzUi.Initialize();
        AddElements(first);
    }
}
