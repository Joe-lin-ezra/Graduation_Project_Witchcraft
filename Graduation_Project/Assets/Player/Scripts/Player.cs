using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{ 
    public GameObject vrCamera;
    public GameObject RightController;
    public GameObject teleport;
    public GameObject terrain;
    //public GameObject magicBallObj;
    public GameObject[] MagicsOBJ;


    public int hp = 100;


    private void Awake()
    {
        vrCamera = GameObject.Find("Player/SteamVRObjects/VRCamera");
        RightController = GameObject.Find("Player/SteamVRObjects/RightHand/Controller (right)");
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
    }

    public void TakeDamage(GameObject g)
    {
        
    }

    public void sellectMagicBall(string text)
    {
        int minIndex = text.Length;
        GameObject magic = null;
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
        if (RightController.GetComponent<VRRightHand>().bullet == null && MagicsOBJ[ans] != null){
            RpcFire(ans);
        }
    }

    [ClientRpc]
    void RpcFire(int ans)
    {
        RightController.GetComponent<VRRightHand>().bullet = Instantiate(MagicsOBJ[ans],
                    RightController.transform.position - 0.2f * Vector3.down + 0.2f * RightController.transform.forward,
                    RightController.transform.rotation,
                    RightController.transform);

    }

    [Command]
    public void CmdFly(){
        RpcFly();
    }

    [ClientRpc]
    void RpcFly(){
        RightController.GetComponent<VRRightHand>().bullet.GetComponent<Rigidbody>().velocity = RightController.transform.forward * RightController.GetComponent<VRRightHand>().bullet.GetComponent<MagicBall>().speed;
        RightController.GetComponent<VRRightHand>().bullet.GetComponent<MagicBall>().magicBallDestory();
        RightController.GetComponent<VRRightHand>().bullet.transform.SetParent(null);
        RightController.GetComponent<VRRightHand>().bullet = null;
    }
    

}
