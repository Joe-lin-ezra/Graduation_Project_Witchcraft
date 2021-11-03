using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerRightHandModle : MonoBehaviour
{
    public GameObject bullet = null;
    public GameObject vrRightHand;

    void Start()
    {
        vrRightHand = GameObject.Find("Player/SteamVRObjects/RightHand/Controller (right)");
    }

    void Update()
    {
        this.transform.position = vrRightHand.transform.position;
        this.transform.rotation = vrRightHand.transform.rotation;
    }
}
