using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    void Update_cooldown(float persentage)
    {

    }

    void Update_Tpcharge(int nowCharge)
    {

    }
    
}
