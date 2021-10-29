using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    int[] select;
    int selection;
    Image img;


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
 
    public void OnTriggerStay(Collider other)
    {
            //element§ó´«ÃC¦âª¬ºA
            img = other.GetComponentInChildren<Image>();
            img.color = Color.red; 
            
    }
    public void OnTriggerExit(Collider other)
    {
            if(selection < 2)
            {
                int a = other.GetComponent<Element>().selection;
                setSelect(a, selection);
                img.color = Color.blue;
            }


    }
    public void setSelect(int selection, int s)
    {
        select[s] = selection;
        Debug.Log(string.Format("Selection: {0} {1}", select[0], select[1]));

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
