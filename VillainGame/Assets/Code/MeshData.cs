using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData : MonoBehaviour
{
    public MeshFilter mf;
    public static MeshData meshData;
    // Start is called before the first frame update
    void Start()
    {
        meshData = this;
        mf = GetComponent<MeshFilter>();

        //LogTriangles();
    }

    void LogTriangles()
    {
        foreach (int tri in mf.sharedMesh.triangles)
        {
            Debug.Log(tri + ", " + transform.TransformPoint(mf.sharedMesh.vertices[tri]));
        }
    }

    void LogVertices()
    {
        foreach (Vector3 vertex in mf.sharedMesh.vertices)
        {
            Debug.Log(transform.TransformPoint(vertex));
        }
    }

    Vector3 StretchToScale(Vector3 v3)
    {
        Vector3 temp = new Vector3(this.transform.localScale.x * v3.x, this.transform.localScale.y * v3.y, this.transform.localScale.z * v3.z);

        return temp;
    }

    Mesh CreateShape()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(1,0,0),
            new Vector3(0,0,1),
            new Vector3(1,0,1),
            new Vector3(0,0,0),
            new Vector3(1,1,0),
            new Vector3(0,1,1),
            new Vector3(0,1,0),
            new Vector3(1,1,1)
        };

        int[] triangles = new int[]
        {
            3, 0, 1, 0, 2, 1, 6, 5, 4, 7, 4, 5
        };

        Mesh mesh = new Mesh();
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
