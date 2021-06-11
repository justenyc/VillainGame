using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cursor : MonoBehaviour
{
    public GameObject prefab;
    public static Cursor cursor;
    [SerializeField]
    public List<string> conjureQueue = new List<string>();

    public float conjureTime = 1f;
    public float conjureTimer;
    bool conjuring = false;
    Vector3 conjurePoint;
    Vector3 conjurePointRotation;

    public Collider col;

    private void Start()
    {
        if (cursor == null)
            cursor = this;

        col = this.GetComponent<Collider>();
        conjureTimer = conjureTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (conjuring == true)
        {
            //Resets Conjure timer when the queue is empty or after the timer hits 0
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

        //Only start conjuring after mouse click
        if (Input.GetMouseButtonDown(0) && conjuring == false)
        {
            ShootRay();
            conjuring = true;
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
            ConjureQueueUI.instance.UpdateQueue();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            conjureQueue.Add("Water");
            ConjureQueueUI.instance.UpdateQueue();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            conjureQueue.Add("Earth");
            ConjureQueueUI.instance.UpdateQueue();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            conjureQueue.Add("Fire");
            ConjureQueueUI.instance.UpdateQueue();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            conjureQueue.Add("Curse");
            ConjureQueueUI.instance.UpdateQueue();
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
                Instantiate(prefab, conjurePoint, Quaternion.Euler(conjurePointRotation), this.transform);
                MagicZone mz = GetComponentInChildren<MagicZone>();
                magicZones.Add(mz);

                mz.InitializeElementsList(conjureQueue[0]);
                conjureQueue.RemoveAt(0);
                ConjureQueueUI.instance.UpdateQueue();
                mz.transform.parent = null;
            }
            else
            {
                foreach (MagicZone mz in magicZones)
                {
                    mz.MagicEnforcer(conjureQueue[0]);
                }
                conjureQueue.RemoveAt(0);
                ConjureQueueUI.instance.UpdateQueue();
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

    void ShootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        try
        {
            if (Physics.Raycast(ray, out hit))
            {
                conjurePoint = (hit.point);
                conjurePointRotation = hit.normal;
            }
        }
        catch
        {
            conjurePoint = Vector3.zero;
            conjurePointRotation = Vector3.zero;
        }
    }
}
