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
    public GameObject magicBall = null;


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

            // this is called on the server
    [Command]
    public void CmdFire(GameObject magic)
    {
        if(magicBall != null){
            GameObject projectile = Instantiate(magic,
                    RightController.transform.position - 0.2f * Vector3.down + 0.2f * RightController.transform.forward,
                    RightController.transform.rotation,
                    RightController.transform);
            NetworkServer.Spawn(magicBall);
        }  
    }

}
