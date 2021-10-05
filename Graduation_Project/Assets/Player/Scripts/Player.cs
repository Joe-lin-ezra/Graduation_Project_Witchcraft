using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{

    public GameObject vrCamera;
    public GameObject teleprot;
    public GameObject terrain;

    public int hp = 100;


    private void Awake()
    {
        vrCamera = GameObject.Find("Player/SteamVRObjects/VRCamera");
    }
    // Start is called before the first frame update
    public override void OnStartLocalPlayer()
    {
        this.gameObject.transform.position = vrCamera.transform.position;
        this.gameObject.transform.rotation = vrCamera.transform.rotation;
        Instantiate(teleprot, new Vector3(3, 0, 0), new Quaternion(0, 0, 0, 0));
        Instantiate(terrain, this.gameObject.transform.position, new Quaternion(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        this.gameObject.transform.position = vrCamera.transform.position;
        this.gameObject.transform.rotation = vrCamera.transform.rotation;
    }

    public void TakeDamage(GameObject g)
    {
        
    }

}
