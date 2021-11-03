using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;



public class VRLeftHand : MonoBehaviour
{
    private GameObject PointerSomething = null;
    public GameObject CircleMenu;
    public GameObject SummonControler;

    public int selected;
    Summon sumo;

    public GameObject Pointer;
    private Pointer pt;  
    public bool debug;

    
    void Start()
    {
        selected = 0;
        pt = Pointer.GetComponent<Pointer>();
        sumo = SummonControler.GetComponent<Summon>();
    }

    // Update is called once per frame
    void Update()
    {
        MenuTrigger();
    }

    void MenuTrigger()
    {

        if (SteamVR_Actions.default_PadOnTouch_Left.state)
        {
            //If touched, get ui position
            if(debug)
            {
                Debug.Log(string.Format("Touchpad : X {0:0.00} ,Y {0:0.00}", SteamVR_Actions.default_PadPosition_Left.axis.x, SteamVR_Actions.default_PadPosition_Left.axis.y));
            }
            CircleMenu.GetComponent<CircularControl>().pointPos = SteamVR_Actions.default_PadPosition_Left.axis;

        }

        // another summon monster
        sumo.CheckMonster(pt);
        if (SteamVR_Actions.default_PadOnTouch_Left.stateUp)
        {
            if(pt.isTrigered)
            {
                sumo.Chnage();
                pt.reCenter();
            }
            
        }
    }
}
