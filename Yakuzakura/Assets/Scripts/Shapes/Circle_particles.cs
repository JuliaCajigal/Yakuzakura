using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_particles : MonoBehaviour
{
    public ParticleSystem ps;
    private float radius;
    private bool bigger;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        radius = 0.05f;
        bigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (bigger)
        {
            Bigger();
        }
        else
        {
            Smaller();
        }
        
        
    }

    void Bigger()
    {
        var sh = ps.shape;
        sh.radius += radius;

        if (sh.radius >= 10)
        {
            bigger = false;
        }
    }

    void Smaller()
    {
        var sh = ps.shape;
        sh.radius -= radius;

        if (sh.radius <= 2)
        {
            bigger = true;
        }
    }
}
