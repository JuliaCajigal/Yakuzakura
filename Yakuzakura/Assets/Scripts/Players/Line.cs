using UnityEngine;
using System.Collections;


public class Line : MonoBehaviour
{

    public GameObject gameObject1;          // Reference to the first GameObject
    public GameObject gameObject2;          // Reference to the second GameObject

    private LineRenderer line;                           // Line Renderer

    // Use this for initialization
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, new Vector3(gameObject1.transform.position.x, gameObject1.transform.position.y, gameObject1.transform.position.z));
        line.SetPosition(1, new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y, gameObject2.transform.position.z));
    }
}