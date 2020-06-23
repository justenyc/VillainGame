using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cursor : MonoBehaviour
{
    //TODO List.RemoveAt instead of Queue.dequeue

    public GameObject prefab;
    public static Cursor cursor;
    [SerializeField]
    List<string> conjureQueue = new List<string>();

    public float conjureTime = 1f;
    public float conjureTimer;
    bool conjuring = false;
    Vector3 conjurePoint;

    public Collider col;

    bool foundZone = false; //The ZoneFinder Child will scan for any Magic Zones first. If none are found then instantiate one at the hit.point

    private void Start()
    {
        col = this.GetComponent<Collider>();
        conjureTimer = conjureTime;

        if (cursor == null)
            cursor = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (conjuring == true)
        {
            if (conjureQueue.Count < 1)
            {
                conjureTimer = conjureTime;
                conjuring = false;
            }
            else if (conjureTimer > 0)
                conjureTimer -= Time.deltaTime;
            else
            {
                conjureTimer = conjureTime;
                Conjure();
            }
        }

        if (conjureQueue.Count < 5 && conjuring == false)
        {
            AddconjureQueue();
        }

        if (Input.GetMouseButtonDown(0) && conjuring == false)
        {
            conjurePoint = ShootRay();
            conjuring = true;
            //Instantiate(prefab, ShootRay(), Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
            conjureQueue.Clear();
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            foreach (string s in conjureQueue)
            {
                Debug.Log(s);
            }
        }
    }

    void AddconjureQueue()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            conjureQueue.Add("Lightning");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            conjureQueue.Add("Water");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            conjureQueue.Add("Earth");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            conjureQueue.Add("Fire");
        }
    }

    void Conjure()
    {
        if (conjureQueue.Count < 1)
        {
            conjuring = false;
        }
        else
        {
            List<MagicZone> magicZones = RayCastSphere(conjurePoint);

            if (magicZones.Count <= 0)
            {
                Instantiate(prefab, conjurePoint, Quaternion.identity, this.transform);
                MagicZone mz = GetComponentInChildren<MagicZone>();
                magicZones.Add(mz);
                mz.InitializeElementsList(conjureQueue[0]);
                conjureQueue.RemoveAt(0);
                mz.transform.parent = null;
            }
            else
            {
                foreach (MagicZone mz in magicZones)
                {
                    mz.MagicEnforcer(conjureQueue[0]);
                }
                conjureQueue.RemoveAt(0);
            }
        }
    }

    List<MagicZone> RayCastSphere(Vector3 origin)
    {
        List<MagicZone> magicZones = new List<MagicZone>();
        Collider[] hitColliders = Physics.OverlapSphere(origin, 1f);

        foreach (Collider col in hitColliders)
        {
            if (col.gameObject.tag == "MagicZone")
            {
                magicZones.Add(col.GetComponent<MagicZone>());
            }
        }

        return magicZones;
    }

    Vector3 ShootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        try
        {
            if (Physics.Raycast(ray, out hit))
            {
                return (hit.point);
            }
        }
        catch
        {
            return Vector3.zero;
        }
        return Vector3.zero;
    }
}
