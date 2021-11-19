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

    //UI component 有點懶惰想 先拉
    public TMP_Text teleportWord;
    public Image cooldown_UI;

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
                // hide the raser line
                transform.GetChild(1).gameObject.SetActive(false);
            }
            else if (teleporting != null)
            {
                // show the raser line
                transform.GetChild(1).gameObject.SetActive(true);
               
            }
            
        }
        // 0 ~ 1 會有bug

        if (teleportCharge <= 0)
        {
            teleporting.SetActive(false);
        }
        else
        {
            if(teleporting!=null)
                teleporting.SetActive(true);
        }

        if (SteamVR_Actions.default_Teleport.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            if (teleportCharge != 0) teleportCharge--;
        }

        if (SteamVR_Actions.default_Teleport.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            if(teleportCharge != 0 )teleportCharge--;
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
    public void UpdateColdownUI()
    {
        if (teleportWord != null) teleportWord.text = teleportCharge.ToString();
        if (cooldown_UI != null) cooldown_UI.fillAmount = timer / cooldonTime;
    }
    public void TeleportInit()
    {
        timer = 0;
        teleportCharge = teleportMaxCharge;
        //teleportWord = teleportUI.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<TMP_Text>();
        //cooldown_UI =  transform.GetChild(0).transform.GetChild(0).GetComponentsInChildren<Image>()[1];

    }
}
