using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOWMesh_Sam : MonoBehaviour
{
    FOW_Sam fow;
    Mesh mesh;
    RaycastHit2D hit;
    [SerializeField] float meshRes = 2;
    [HideInInspector] public Vector3[] vertices;
    [HideInInspector] public int[] triangles;
    [HideInInspector] public int stepCount;
    public Material redCone;
    public Material greenCone;


    void Start()
    {
        //Malla y campo de visión
        mesh = GetComponent<MeshFilter>().mesh;
        fow = GetComponentInParent<FOW_Sam>();
    }

 
    void LateUpdate()
    {
        //Creación de la malla que fomra el cono de visión, su superposiciónn indicará que el jugador es visible
        MakeMesh();

    }

    //Creamos un cono de visión, obviamos los objetos que se interponen en el raycast desde la posición del enemigo hacia su frente
    void MakeMesh()
    {
        stepCount = Mathf.RoundToInt(fow.viewAngle * meshRes);
        float stepAngle = fow.viewAngle / stepCount;

        List<Vector3> viewVertex = new List<Vector3>();

        hit = new RaycastHit2D();

        //Se añadirá colisión con objeto si el raycast se encuentra algún obstáculo
        for (int i = 0; i <= stepCount; i ++)
        {
            float angle = (fow.transform.eulerAngles.y - fow.viewAngle / 2 + stepAngle * i) + 90;
            Vector3 dir = fow.DirFromAngle(angle, false);

            hit = Physics2D.Raycast(fow.transform.position, dir, fow.viewRadius, fow.obstacleMask);
            if(hit.collider == null)
            {
                viewVertex.Add(transform.position + dir.normalized * fow.viewRadius);
            }
            else
            {
                viewVertex.Add(transform.position + dir.normalized * hit.distance);
            }
        }

        //Con los vértices obtenidos en el raycast creamos nuestra malla
        int vertexCount = viewVertex.Count + 1;

        vertices = new Vector3[vertexCount];
        triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;

        for(int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewVertex[i]);

            if (i < vertexCount -2)
            {
                triangles[i * 3 + 2] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3] = i + 2;
            }
 
        }

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }



    private void Update()
    {
        //Cambio de modo
        if(fow.samurai.chasing == true || fow.samurai.patrolling==false)
        {
            GetComponentInParent<MeshRenderer>().material = redCone;
        }

        if(fow.samurai.patrolling == true)
        {
            GetComponentInParent<MeshRenderer>().material = greenCone;
        }
        
    }
}
