using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicZone : MonoBehaviour
{
    [SerializeField]
    float timer = 5.5f;

    [SerializeField]
    List<string> elements;

    // Start is called before the first frame update
    void Start()
    {
        if (elements == null)
        {
            elements = new List<string>();
        }
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
                Instantiate(vfxHolder.MagicSparklePrefabs["FireSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, this.transform);
                break;

            case "Water":
                Instantiate(vfxHolder.MagicSparklePrefabs["WaterSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, this.transform);
                break;

            case "Earth":
                Instantiate(vfxHolder.MagicSparklePrefabs["EarthSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, this.transform);
                break;

            case "Lightning":
                Instantiate(vfxHolder.MagicSparklePrefabs["LightningSparkle"], this.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, this.transform);
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
                    elements[elements.Count - 1] = "Burst";
                    Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
                    break;
                }
                else if (elements[elements.Count - 1] == "Earth")
                {
                    elements[elements.Count - 1] = "Lava";
                    Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
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
                    elements[elements.Count - 1] = "Vacuum";
                    Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
                    break;
                }
                else if (elements[elements.Count - 1] == "Earth")
                {
                    elements[elements.Count - 1] = "Mud";
                    Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
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
                    elements[elements.Count - 1] = "Lava";
                    Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
                    break;
                }
                else if (elements[elements.Count - 1] == "Water")
                {
                    elements[elements.Count - 1] = "Mud";
                    Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
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
                    elements[elements.Count - 1] = "Burst";
                    Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
                    break;
                }
                else if (elements[elements.Count - 1] == "Water")
                {
                    elements[elements.Count - 1] = "Vacuum";
                    Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
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
                        elements.RemoveAt(i);
                        ParticleSystem[] Sparkles = GetComponentsInChildren<ParticleSystem>();
                        Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position, Quaternion.identity);
                        Destroy(Sparkles[i + 1].gameObject);
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
                        elements.RemoveAt(i);
                        ParticleSystem[] Sparkles = GetComponentsInChildren<ParticleSystem>();
                        Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position, Quaternion.identity);
                        Destroy(Sparkles[i + 1].gameObject);
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
                        elements.RemoveAt(i);
                        ParticleSystem[] Sparkles = GetComponentsInChildren<ParticleSystem>();
                        Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position, Quaternion.identity);
                        Destroy(Sparkles[i + 1].gameObject);
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
                        elements.RemoveAt(i);
                        ParticleSystem[] Sparkles = GetComponentsInChildren<ParticleSystem>();
                        Instantiate(vfxHolder.MagicSparklePrefabs["NegationCircle"], this.transform.position, Quaternion.identity);
                        Destroy(Sparkles[i + 1].gameObject);
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

    public void InitializeElementsList(string first)
    {
        elements = new List<string>();
        AddElements(first);
    }
}
