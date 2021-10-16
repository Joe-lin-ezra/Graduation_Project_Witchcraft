using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    int[] select;
    int selection;
    Image img;
    Color tmp;

    // Start is called before the first frame update
    void Start()
    {
        select = new int[2];
        selection = 0;
        for (int i = 0; i < select.Length; i++)
        {
            select[i] = -1;
        }
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
        setSelect(a, selection);
        tmp = img.color;
        img.color = Color.red;
    }
    public void OnTriggerExit(Collider other)
    {
        img.color = Color.blue;
    }
    public void setSelect(int selection, int s)
    {
        
        select[s] = selection;

    }
    public int[] getSelect()
    {
        return this.select;
    }
    public int setSelection(int a)
    {
        selection = a % 2;
        return selection;
    }
    public int getSelection()
    {
        return selection;
    }
}
