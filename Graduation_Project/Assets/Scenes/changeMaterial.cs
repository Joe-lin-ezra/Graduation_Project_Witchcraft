using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMaterial : MonoBehaviour
{
    public Material smile;
    public Material hit;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Renderer>().material = smile;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            this.gameObject.GetComponent<Renderer>().material = hit;
        }
        if(Input.GetKeyDown(KeyCode.CapsLock)){
            this.gameObject.GetComponent<Renderer>().material = smile;
        }
    }
}
