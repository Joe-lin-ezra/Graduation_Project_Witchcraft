using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class NetworkingInitializer : MonoBehaviour
{
    public GameObject speechRecognizer;
    public GameObject rightController;
    public GameObject magicController;
    public GameObject mainCanvas;
    public GameObject summonController;
    public GameObject teleportCold;
    public GameObject MagicUI;
    public GameObject monsterSelector;
    public bool uiSwitch;

    // Start is called before the first frame update
    void Start()
    {
        uiSwitch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length > 0) {
            uiSwitch = true;
            
        }
        if (uiSwitch)
        {
            rightController.GetComponent<VRRightHand>().enabled = true;
            rightController.GetComponent<SteamVR_LaserPointer>().enabled = true;
            speechRecognizer.SetActive(true);
            magicController.SetActive(true);
            mainCanvas.SetActive(true);
            summonController.SetActive(true);
            teleportCold.SetActive(true);
            MagicUI.SetActive(true);
            monsterSelector.SetActive(true);
        }
    }
}
