using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;

public class VRLeftHand : MonoBehaviour
{
    // Start is called before the first frame update
    SteamVR_LaserPointer slp;   //射线对象
    private GameObject PointerSomething = null;//被指物
    public GameObject CircleUI;

    [Header("debug")]
    public bool debug;
    void Start()
    {
        //slp = getcomponent<steamvr_laserpointer>();
        //slp.pointerin += onpointerin;
        //slp.pointerout += onpointerout;    //響應設線離開事件
        //getcomponent<steamvr_laserpointer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        MenuTrigger();


    }

    void ClickTrigger()
    {

        
    }
    void MenuTrigger()
    {
        if (SteamVR_Actions.default_PadOnTouch.state)
        {
            CircleUI.SetActive(true);
            //get ui posi
            if(debug)
            {
                Debug.Log(string.Format("Touchpad : X {0:0.00} ,Y {0:0.00}", SteamVR_Actions.default_PadPosition.axis.x, SteamVR_Actions.default_PadPosition.axis.y));
            }
            
        }
        if (!SteamVR_Actions.default_PadOnTouch.state)
        {
            CircleUI.SetActive(false);
        }
    }


    /*void OnpointerIn(object sender, PointerEventArgs e) //射线进入事件
    {
        //GameObject obj = e.target.gameObject;//得到指向的物体
        //if (obj.tag.Equals("CanPointer")) //如果我们选择的物体他的标签是CanPointer
        //{
        //    PointerSomething = obj;  //用全局变量记录这个物体
        //}
    }
    void OnpointerOut(object sender, PointerEventArgs e)//射线离开事件
    {
        if (PointerSomething != null)  //如果是在能拾取的物体上离开
        {
            PointerSomething = null;  //不再记录这个物体了
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
