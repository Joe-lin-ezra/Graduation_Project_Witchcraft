using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    int[] select;
    

    // Start is called before the first frame update
    void Start()
    {
        select = new int[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        int a = other.GetComponent<Element>().selection;
        Debug.Log(a);
        setSelect(a, 0);
    }
    public void setSelect(int selection,int s)
    {
        select[s] = selection;

    }
}
