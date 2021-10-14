using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Mirror;

public class Player : NetworkBehaviour
{ 
    public GameObject vrCamera;
    public GameObject RightController;
    public GameObject playerRightHandModle;
    public GameObject teleport;
    public GameObject terrain;
    //public GameObject magicBallObj;
    public GameObject[] MagicsOBJ;
    public GameObject bullet;


    public int hp = 100;


    private void Awake()
    {
        vrCamera = GameObject.Find("Player/SteamVRObjects/VRCamera");
        RightController = GameObject.Find("Player/SteamVRObjects/RightHand/Controller (right)");
        //playerRightHandModle = Instantiate(playerRightHandModle);
    }
    // Start is called before the first frame update
    public override void OnStartLocalPlayer()
    {
        transform.position = vrCamera.transform.position;
        transform.rotation = vrCamera.transform.rotation;
        GameObject t = Instantiate(teleport, new Vector3(3, 0, 0), new Quaternion(0, 0, 0, 0));
        GameObject RightController = GameObject.Find("Player/SteamVRObjects/RightHand/Controller (right)");
        RightController.GetComponent<VRRightHand>().setTeleporting(t);
        Instantiate(terrain, transform.position - new Vector3(0, 1, 0), new Quaternion(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        transform.position = vrCamera.transform.position;
        transform.rotation = vrCamera.transform.rotation;

        if ( Input.GetKeyDown(KeyCode.Space))//SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand)) 右手手把開搶鍵有bug ,會導致遊戲崩潰，請有空的隊友幫忙debug
        {
            if (bullet != null)
            {
                CmdFly();
            }

        }
    }

    public void TakeDamage(GameObject g)
    {
        
    }

    public void sellectMagicBall(string text)
    {
        int minIndex = text.Length;
        int count = 0;
        int ans = 0;
        foreach (GameObject m in MagicsOBJ)
        {
            int i = text.IndexOf(m.GetComponent<MagicBall>().magicName);
            if (i < minIndex && i != -1)
            {
                minIndex = i;
                //magic = m;
                ans = count;
            }
            count++;
        }
        CmdFire(ans);
    }

            // this is called on the server
    [Command]
    public void CmdFire(int ans)
    {
        if (bullet == null && MagicsOBJ[ans] != null){
            RpcFire(ans);
        }
    }

    [ClientRpc]
    void RpcFire(int ans)
    {
        bullet = Instantiate(MagicsOBJ[ans],
                    playerRightHandModle.transform.position - 0.1f * Vector3.down + 0.1f * playerRightHandModle.transform.forward,
                    playerRightHandModle.transform.rotation,
                    playerRightHandModle.transform);

    }

    [Command]
    public void CmdFly(){
        RpcFly();
    }

    [ClientRpc]
    void RpcFly(){
       bullet.GetComponent<Rigidbody>().velocity = playerRightHandModle.transform.forward *bullet.GetComponent<MagicBall>().speed;
       bullet.GetComponent<MagicBall>().magicBallDestory();
       bullet.transform.SetParent(null);
       bullet = null;
    }
    

}
