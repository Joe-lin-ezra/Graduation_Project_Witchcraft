using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;

public class VRRightHand: MonoBehaviour
{
    // Start is called before the first frame update
    SteamVR_LaserPointer slp;   //射线对象
    private GameObject PointerSomething = null;//被指物
    
    public GameObject speechRecognizer;

    [HideInInspector]
    public GameObject bullet = null;//被發射物

    private GameObject teleporting = null;
    //UI Componenet
    public GameObject MagicUI;
    public Image magicActive;
    public 


    void Start()
    {
        slp = GetComponent<SteamVR_LaserPointer>();
        slp.PointerIn += OnpointerIn;
        slp.PointerOut += OnpointerOut;    //響應設線離開事件

        speechRecognizer = GameObject.Find("SpeechRecognizer");


        magicActive = MagicUI.GetComponentInChildren<Image>();
        //UI
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

        // shoot out magic ball
        if(SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand) && bullet != null)
        {
            try
            {
                // give velocity
                bullet.GetComponent<Rigidbody>().velocity =
                    gameObject.transform.forward * bullet.GetComponent<MagicBall>().speed;
                bullet.GetComponent<MagicBall>().BulletDestory();
                bullet.transform.SetParent(null);
                bullet = null;
            } 
            catch
            {
                Debug.LogWarning(">> you have no magic");
            }
        }
        if (SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand) && bullet == null) // start listening
        {
            magicActive.color = Color.red;
            speechRecognizer.GetComponent<SpeechRecognizer>().startListening();
        }
        else if (SteamVR_Actions.default_GrabGrip.GetStateUp(SteamVR_Input_Sources.RightHand)) // stop listening
        {
            magicActive.color = Color.white;
            speechRecognizer.GetComponent<SpeechRecognizer>().stopListening();
        }
    }

    void OnpointerIn(object sender, PointerEventArgs e) //射线进入事件
    {
        GameObject obj = e.target.gameObject;//得到指向的物体
        if (obj.tag.Equals("CanPointer")) //如果我们选择的物体他的标签是CanPointer
        {
            PointerSomething = obj;  //用全局变量记录这个物体
        }
    }
    void OnpointerOut(object sender, PointerEventArgs e)//射线离开事件
    {
        if (PointerSomething != null)  //如果是在能拾取的物体上离开
        {
            PointerSomething = null;  //不再记录这个物体了
        }
    }
    
    public void setTeleporting(GameObject t)
    {
        this.teleporting = t;
    }
}
