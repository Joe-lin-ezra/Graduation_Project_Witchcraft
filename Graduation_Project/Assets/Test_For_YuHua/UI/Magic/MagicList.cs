using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MagicList : MonoBehaviour
{
    public GameObject playerModel;
    public Player playerC;
    public GameObject[] mgList;
    public GameObject wordPrefab;
    public TMP_Text mgicText;
    public string text = "";

    private void Awake()
    {
        playerC = playerModel.GetComponent<Player>();
        mgList = playerC.MagicsOBJ;// array

       
        foreach (GameObject element in mgList)
        {
            text += string.Format("¡P {0} \n",element.name);
        }
        mgicText.text = text;
    }
    // Start is called before the first frame update
    
}
