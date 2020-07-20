using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    public Orbit object1;
    public Orbit2 object2;
    public Transform transformObject1;
    public Transform transformObject2;
    private Vector3 posicion;
    public Vector3 offset;
    public float CamMoveSpeed = 5f;

    void Start()
    {

        offset.Set(0, 0, -10);
        transform.position =(transformObject1.transform.position + offset);

    }

    void Update()
    {

        if (object1.orbiting == false) {

            transform.position = Vector3.Lerp(transform.position, transformObject1.transform.position + offset, CamMoveSpeed * Time.deltaTime);


        }
        if(object2.orbiting == false){
  
            transform.position = Vector3.Lerp(transform.position, transformObject2.transform.position + offset, CamMoveSpeed * Time.deltaTime);

        }  

    }
}
