using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerRightHandModle : MonoBehaviour
{
    public GameObject bullet = null;//³Qµo®gª«
    public GameObject vrRightHand;
    // Start is called before the first frame update
    void Start()
    {
        vrRightHand = GameObject.Find("Player/SteamVRObjects/RightHand/Controller (right)");// RightRenderModel Slim(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = vrRightHand.transform.position;
        this.transform.rotation = vrRightHand.transform.rotation;
    }
}
