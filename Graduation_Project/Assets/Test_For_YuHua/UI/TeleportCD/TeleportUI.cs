using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TeleportUI : MonoBehaviour
{
    public GameObject circle;
    public GameObject text;
    public Image cooldown_UI;
    public TMP_Text Tpcharge;
    void Awake()
    {
        
        cooldown_UI = circle.GetComponent<Image>();
        Tpcharge = text.GetComponent<TMP_Text>();
    }

    public void Update_cooldown(float persentage)
    {
       cooldown_UI.fillAmount = persentage;
    }

    public void Update_Tpcharge(int nowCharge)
    {
        Tpcharge.text = String.Format("{0}",nowCharge);
    }
    
}
