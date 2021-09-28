using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;

public class VRRightHand: MonoBehaviour
{
    // Start is called before the first frame update
    SteamVR_LaserPointer slp;   //射线对象
    private GameObject PointerSomething = null;//被指物
    public GameObject bullet = null;//被發射物
    public string something = null;

    void Start()
    {
        slp = GetComponent<SteamVR_LaserPointer>();
        slp.PointerIn += OnpointerIn;
        slp.PointerOut += OnpointerOut;    //響應設線離開事件
        GetComponent<SteamVR_LaserPointer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        ClickTrigger();
    }

    void ClickTrigger(){
         // teleport
        if(SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand) && something == null)
        {
            // teleport effect start
            
        }
        else if(SteamVR_Actions.default_GrabPinch.GetStateUp(SteamVR_Input_Sources.RightHand) && something == null)
        {
            // do teleport
            
        }

        // shoot out magic ball
        if(SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand) && something != null && bullet != null)
        {
            try
            {
                // give velocity
                //bullet.GetComponent<Rigidbody>().AddForce(
                //    gameObject.transform.forward * bullet.GetComponent<MagicBall>().speed * Time.deltaTime);
                bullet.GetComponent<Rigidbody>().velocity =
                    gameObject.transform.forward * 1;//bullet.GetComponent<MagicBall>().speed;
                bullet = null;
                something = null;

            } catch
            {
                Debug.LogWarning(">> you have no magic");
            }
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

    private void OnTriggerEnter(Collider other)
    {
        bullet = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        bullet = null;
    }
}
