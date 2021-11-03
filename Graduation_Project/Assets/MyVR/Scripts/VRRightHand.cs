using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;
using Mirror;

public class VRRightHand: MonoBehaviour
{
    public GameObject speechRecognizer;

    [HideInInspector]
    public GameObject bullet = null;

    private GameObject teleporting = null;

    [Header("Mirror")]
    public GameObject playerModle;
    public GameObject summon_control;

    [Header("Monster")]
    public GameObject monster_manager;

    void Start()
    {
        speechRecognizer = GameObject.Find("SpeechRecognizer");
    }

    // Update is called once per frame
    void Update()
    {
        ClickTrigger();
    }

    void ClickTrigger(){
         // teleport
        if(teleporting != null && teleporting.GetComponent<TeleportArc>().GetArc())
        {
            // hide the raser line
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else if(teleporting != null)
        {
            // show the raser line
            transform.GetChild(1).gameObject.SetActive(true);
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
}
