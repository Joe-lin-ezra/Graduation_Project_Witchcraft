using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    private GameObject vrPlayer;
    // Start is called before the first frame update
    public override void OnStartLocalPlayer()
    {
        vrPlayer = GameObject.Find("Player");
        if (!isLocalPlayer)
            vrPlayer.SetActive(false);
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
    }

    public void TakeDamage(GameObject g)
    {
        
    }
}
