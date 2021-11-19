using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;
using Mirror;
using TMPro;

public class VRRightHand: MonoBehaviour
{
    public GameObject speechRecognizer;

    
    public GameObject bullet = null;
    public GameObject teleporting = null;
    public Teleport tpobj = null;

    [Header("Mirror")]
    public GameObject playerModle;
    public GameObject summon_control;

    [Header("Monster")]
    public GameObject monster_manager;

    [Header("Teleport")]
    public int teleportCharge;
    public int teleportMaxCharge;
    public float cooldonTime = 0.0f;
    public float timer = 0.0f;
    public GameObject teleportUI;

    //UI component ���I�i�k�Q ����

    void Start()
    {
        speechRecognizer = GameObject.Find("SpeechRecognizer");
        TeleportInit();
    }

    // Update is called once per frame
    void Update()
    {
        ClickTrigger();
        if( teleportCharge != teleportMaxCharge)
        {
            ChargeTeleport();
        }
    }

    void ClickTrigger(){
        // teleport
        if (teleportCharge > 0)
        {
            
            if (teleporting != null && teleporting.GetComponent<TeleportArc>().GetArc())
            {
                tpobj = teleporting.GetComponent<Teleport>();
                // hide the raser line
                transform.GetChild(1).gameObject.SetActive(false);
            }
            else if (teleporting != null)
            {

                // show the raser line
                transform.GetChild(1).gameObject.SetActive(true);
                tpobj = teleporting.GetComponent<Teleport>();
            }
            
        }
        // 0 ~ 1 �|��bug

        if (teleportCharge <= 0)
        {
            teleporting.SetActive(false);
        }
        else
        {
            if(teleporting!=null)
                teleporting.SetActive(true);
        }
        if (SteamVR_Actions.default_Teleport.GetStateUp(SteamVR_Input_Sources.RightHand))
        {
            if (teleportCharge != 0 && tpobj.Teleported) teleportCharge--;
        }
            // Speech Recognizer listening setting
            if (SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand) && bullet == null) 
        {
            speechRecognizer.GetComponent<SpeechRecognizer>().startListening();
        }
        else if (SteamVR_Actions.default_GrabGrip.GetStateUp(SteamVR_Input_Sources.RightHand))
        {
            // stop listening when sub-system get the return string
        }
    }

    public void setTeleporting(GameObject t)
    {
        this.teleporting = t;
    }
    public void TimeRun()
    {
        timer += Time.deltaTime;
    }
    public void ChargeTeleport()
    { 
        if(teleportCharge != teleportMaxCharge)//when charge != not charge
        {
            TimeRun();//timer add
            if(timer >= cooldonTime)
            {
                timer = 0;
                teleportCharge += 1;
                UpdateColdownUI();
            }
        }
    }

    public void TeleportInit()
    {
        timer = 0;
        teleportCharge = teleportMaxCharge;
        
        
    }
}
