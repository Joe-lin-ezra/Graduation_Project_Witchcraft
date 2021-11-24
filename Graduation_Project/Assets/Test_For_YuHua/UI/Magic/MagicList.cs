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

       
        for(int i = 0; i < mgList.Length;i+=3)
        {
            text += string.Format("¡P{0} ¡P{1} ¡P{2} \n", mgList[i].GetComponent<MagicBall>().magicName, mgList[i+1].GetComponent<MagicBall>().magicName,mgList[i+2].GetComponent<MagicBall>().magicName);
        }
        mgicText.text = text;
    }
    // Start is called before the first frame update
    
}
