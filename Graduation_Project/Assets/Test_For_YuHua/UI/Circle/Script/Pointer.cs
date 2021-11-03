using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Pointer : MonoBehaviour
{
    int[] select;
    int selection;
    Image img;
    public bool isTrigered;


    // Start is called before the first frame update
    void Start()
    {
        select = new int[2];
        selection = 0;

        //clear select
        for (int i = 0; i < select.Length; i++)
        {
            select[i] = -1;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CircleElements") isTrigered = true;
    }
    public void OnTriggerStay(Collider other)
    {
        try {
            if (other.tag == "CircleElements")
            {
                
                //element§ó´«ÃC¦âª¬ºA
                img = other.GetComponentInChildren<Image>();
                img.color = Color.red;
            }
        }
        catch(Exception e)
        {
            print(string.Format("stay {0}",e));
        }


    }
    public void OnTriggerExit(Collider other)
    {
        try
        {
            if(other.tag == "CircleElements")
            {
                if (selection < 2)
                {
                    int a = other.GetComponent<Element>().selection;
                    setSelect(a, selection);
                    Color tmpcol = Color.white;
                    tmpcol.a = 0.0f;
                    img.color = tmpcol;
                    
                }
            }

        }
        catch
        {

        }
        if (other.tag == "CircleElements") isTrigered = false;
    }
    public void setSelect(int selection, int s)
    {
        select[s] = selection;
        Debug.Log(string.Format("Selection: {0} {1}", select[0], select[1]));

    }
    public void reCenter()
    {
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }
    public int[] getSelect()
    {
        return this.select;
    }  
    public int getSelect(int index)
    {
        return this.select[index];
    }
    public int setSelection(int a)
    {
        selection = a % 3;
        return selection;
    }
    public int getSelection()
    {
        return selection;
    }
}
