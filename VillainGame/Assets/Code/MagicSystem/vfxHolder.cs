using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfxHolder : MonoBehaviour
{
    public static vfxHolder instance;

    [SerializeField]
    GameObject[] MagicPrefabsArray = new GameObject[4], MagicSparklePrefabsArray = new GameObject[4];

    public static Dictionary<string, GameObject> MagicPrefabs;
    public static Dictionary<string, GameObject> MagicSparklePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        InitializeMagicPrefabs();
        InitializeMagicSparklePrefabs();
    }

    void InitializeMagicSparklePrefabs()
    {
        MagicSparklePrefabs = new Dictionary<string, GameObject>();
        foreach (GameObject go in MagicSparklePrefabsArray)
        {
            MagicSparklePrefabs.Add(go.name, go);
        }
    }

    void InitializeMagicPrefabs()
    {
        MagicPrefabs = new Dictionary<string, GameObject>();
        foreach (GameObject go in MagicPrefabsArray)
        {
            MagicPrefabs.Add(go.name, go);
        }
    }
}
