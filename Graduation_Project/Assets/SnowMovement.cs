using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMovement : MonoBehaviour
{
    Vector3 orgPosition;

    int offsetLimit = 1;

    // Start is called before the first frame update
    void Start()
    {
        orgPosition = transform.position;
        Invoke("snowMove", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void snowMove()
    {
        float xrand = Random.Range(-0.5f, 0.5f);
        float zrand = Random.Range(-0.5f, 0.5f);

        transform.position = orgPosition + new Vector3(xrand, 0, zrand);
    }
}
