using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection : MonoBehaviour
{
    [SerializeField]
    GameObject ps;
    //Deprecated List<Vector3> inBoundTriVerts = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        List<Vector3> verts = new List<Vector3>();
        RaycastHit hit;

        for (int i = 0; i < 9; i++)
        {
            switch (i)
            {
                case 0:
                    try
                    {
                        if (Physics.Raycast(transform.position + new Vector3(0, 10, 0), -Vector3.up * 15, out hit))
                            verts.Add(transform.InverseTransformPoint(hit.point));
                    }
                    catch
                    {
                        verts.Add(Vector3.zero);
                    }
                    break;

                case 1:
                    try
                    {
                        if (Physics.Raycast(transform.position + new Vector3(1f * other.transform.localScale.x, 10, 0), -Vector3.up * 15, out hit))
                            verts.Add(transform.InverseTransformPoint(hit.point));
                    }
                    catch
                    {
                        verts.Add(Vector3.zero);
                    }
                    break;

                case 2:
                    try
                    {
                        if (Physics.Raycast(transform.position + new Vector3(0.7f * other.transform.localScale.x, 10, -0.7f * other.transform.localScale.z), -Vector3.up * 15, out hit))
                            verts.Add(transform.InverseTransformPoint(hit.point));
                    }
                    catch
                    {
                        verts.Add(Vector3.zero);
                    }
                    break;

                case 3:
                    try
                    {
                        if (Physics.Raycast(transform.position + new Vector3(0f, 10, -1f * other.transform.localScale.z), -Vector3.up * 15, out hit))
                            verts.Add(transform.InverseTransformPoint(hit.point));
                    }
                    catch
                    {
                        verts.Add(Vector3.zero);
                    }
                    break;

                case 4:
                    try
                    {
                        if (Physics.Raycast(transform.position + new Vector3(-0.7f * other.transform.localScale.x, 10, -0.7f * other.transform.localScale.z), -Vector3.up * 15, out hit))
                            verts.Add(transform.InverseTransformPoint(hit.point));
                    }
                    catch
                    {
                        verts.Add(Vector3.zero);
                    }
                    break;

                case 5:
                    try
                    {
                        if (Physics.Raycast(transform.position + new Vector3(-1f * other.transform.localScale.x, 10, 0), -Vector3.up * 15, out hit))
                            verts.Add(transform.InverseTransformPoint(hit.point));
                    }
                    catch
                    {
                        verts.Add(Vector3.zero);
                    }
                    break;

                case 6:
                    try
                    {
                        if (Physics.Raycast(transform.position + new Vector3(-0.7f * other.transform.localScale.x, 10, 0.7f * other.transform.localScale.z), -Vector3.up * 15, out hit))
                            verts.Add(transform.InverseTransformPoint(hit.point));
                    }
                    catch
                    {
                        verts.Add(Vector3.zero);
                    }
                    break;

                case 7:
                    try
                    {
                        if (Physics.Raycast(transform.position + new Vector3(0, 10, 1f * other.transform.localScale.z), -Vector3.up * 15, out hit))
                            verts.Add(transform.InverseTransformPoint(hit.point));
                    }
                    catch
                    {
                        verts.Add(Vector3.zero);
                    }
                    break;

                case 8:
                    try
                    {
                        if (Physics.Raycast(transform.position + new Vector3(0.7f * other.transform.localScale.x, 10, 0.7f * other.transform.localScale.z), -Vector3.up * 15, out hit))
                            verts.Add(transform.InverseTransformPoint(hit.point));
                    }
                    catch
                    {
                        verts.Add(Vector3.zero);
                    }
                    break;
            }
        }
        CreateShape(verts);
    }

    void CreateShape(List<Vector3> verts)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = verts.ToArray();
        int[] triangles = new int[24];
        Vector2[] uvs = new Vector2[verts.Count];
        triangles[0] = triangles[3] = triangles[6] = triangles[9] = triangles[12] = triangles[15] = triangles[18] = triangles[21] = 0;
        triangles[1] = triangles[23] = 1;
        triangles[2] = triangles[4] = 2;
        triangles[5] = triangles[7] = 3;
        triangles[8] = triangles[10] = 4;
        triangles[11] = triangles[13] = 5;
        triangles[14] = triangles[16] = 6;
        triangles[17] = triangles[19] = 7;
        triangles[20] = triangles[22] = 8;

        uvs[0] = new Vector2(0.5f, 0.5f);
        uvs[1] = new Vector2(1, 0.5f);
        uvs[2] = new Vector2(1, 0);
        uvs[3] = new Vector2(0.5f, 0);
        uvs[4] = new Vector2(0, 0);
        uvs[5] = new Vector2(0, 0.5f);
        uvs[6] = new Vector2(0, 1);
        uvs[7] = new Vector2(0.5f, 1);
        uvs[8] = new Vector2(1, 1);

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        MeshFilter mf = this.GetComponent<MeshFilter>();
        mf.sharedMesh = mesh;
        transform.position += new Vector3(0, 0.05f, 0);

        this.GetComponent<MeshRenderer>().enabled = true;

        try
        {
            ps.SetActive(true);

        }
        catch
        {

        }
    }

    /*private void OnTriggerEnterDeprecated(Collider other)
    {
        MeshFilter mf = other.GetComponent<MeshFilter>();
        this.GetComponent<MeshFilter>().mesh.Clear();

        //scan for all vertices within the bounding box
        foreach (Vector3 vertex in mf.sharedMesh.vertices)
        {
            if (col.bounds.Contains(other.transform.TransformPoint(vertex)))
            {
                //find all tris that each vertex in the bounding box is involved in
                for (int i = 0; i < mf.sharedMesh.triangles.Length; i++)
                {
                    if (mf.sharedMesh.vertices[mf.sharedMesh.triangles[i]] == vertex)
                    {
                        //If a vertex is the start of a triangle then it's index in the triangle array should be 0, etc.
                        switch (i % 3)
                        {
                            case 0:
                                CheckDupeDeprecated(mf.sharedMesh.vertices[mf.sharedMesh.triangles[i]]);
                                CheckDupeDeprecated(mf.sharedMesh.vertices[mf.sharedMesh.triangles[i + 1]]);
                                CheckDupeDeprecated(mf.sharedMesh.vertices[mf.sharedMesh.triangles[i + 2]]);
                                break;

                            case 1:
                                CheckDupeDeprecated(mf.sharedMesh.vertices[mf.sharedMesh.triangles[i - 1]]);
                                CheckDupeDeprecated(mf.sharedMesh.vertices[mf.sharedMesh.triangles[i]]);
                                CheckDupeDeprecated(mf.sharedMesh.vertices[mf.sharedMesh.triangles[i + 1]]);
                                break;

                            case 2:
                                CheckDupeDeprecated(mf.sharedMesh.vertices[mf.sharedMesh.triangles[i - 2]]);
                                CheckDupeDeprecated(mf.sharedMesh.vertices[mf.sharedMesh.triangles[i - 1]]);
                                CheckDupeDeprecated(mf.sharedMesh.vertices[mf.sharedMesh.triangles[i]]);
                                break;
                        }
                    }
                }
            }
        }

        //CreateShape(inBoundTriVerts, other);

        //SortVerts();

        foreach (Vector3 v3 in inBoundTriVerts)
        {
            Debug.Log(v3 + " from inBoundTriVerts");
        }
    }*/

    void SortVertsDeprecated()
    {
        //Deprecated inBoundTriVerts = inBoundTriVerts.OrderBy(v => v.x).ThenByDescending(v => v.z).ToList<Vector3>();
    }

    void CheckDupeDeprecated(Vector3 check)
    {
        //Deprecated inBoundTriVerts.Add(check);
        /*if (inBoundTriVerts.Count < 1)
        {
            inBoundTriVerts.Add(check);
        }
        else
        {
            bool unique = true;

            foreach (Vector3 v3 in inBoundTriVerts)
            {
                if (v3 == check)
                {
                    unique = false;
                    break;
                }
            }

            if (unique == true)
            {
                inBoundTriVerts.Add(check);
            }
        }*/
    }

    /*void CreateShapeDeprecated(List<Vector3> verts, Collider other)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[verts.Count];
        int[] triangles = new int[verts.Count];
        Vector3 offset = other.transform.position - this.transform.position;

        for (int i = 0; i < verts.Count; i++)
        {
            vertices[i] = verts[i];
            triangles[i] = i;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        for (int i = 0; i < verts.Count; i++)
        {
            Debug.Log(mesh.vertices[i] + " from shape " + verts[i] + " from inBoundTriVerts");
        }
        
        this.GetComponent<MeshFilter>().sharedMesh = mesh;
    }*/

    /*void LogInBoundsVertices(Collider other)
    {
        MeshFilter mf = other.GetComponent<MeshFilter>();
        foreach (Vector3 vertex in mf.sharedMesh.vertices)
        {
            if (col.bounds.Contains(other.transform.TransformPoint(vertex)))
                Debug.Log(other.transform.TransformPoint(vertex));
        }
    }*/

    /*void LogWorldSpaceMeshData(Collider other)
    {
        MeshFilter mf = other.GetComponent<MeshFilter>();
        Vector3 otherTransform = other.transform.position;
        Vector3 distance = other.transform.position - this.transform.position;

        for (int i = 0; i < mf.sharedMesh.vertexCount; i++)
        {
            Debug.Log(other.transform.TransformPoint(mf.sharedMesh.vertices[i]) + ", " + other.transform.TransformPoint(MeshData.meshData.mf.sharedMesh.vertices[i]));
        }
    }*/
}
