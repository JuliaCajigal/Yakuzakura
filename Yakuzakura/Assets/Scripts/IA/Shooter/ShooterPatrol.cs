using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPatrol : MonoBehaviour
{

    float targetAngle = 0;
    float turnSpeed = 5;

    void Start()
    {
        ChangeAngle();
    }


    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), turnSpeed * Time.deltaTime);

    }


    public void ChangeAngle()
    {
        targetAngle += 90;
        Invoke("ChangeAngle", 4);

    }


}
