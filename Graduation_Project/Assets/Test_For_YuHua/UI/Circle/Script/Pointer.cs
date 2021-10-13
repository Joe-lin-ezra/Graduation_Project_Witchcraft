using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    int[] select;
    Image img;
    Color tmp;

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
        img = other.GetComponentInChildren<Image>();
        Debug.Log(a);
        setSelect(a, 0);
        tmp = img.color;
        img.color = Color.red;
    }
    public void OnTriggerExit(Collider other)
    {
        img.color = Color.blue;
    }
    public void setSelect(int selection,int s)
    {
        select[s] = selection;

    }
}
