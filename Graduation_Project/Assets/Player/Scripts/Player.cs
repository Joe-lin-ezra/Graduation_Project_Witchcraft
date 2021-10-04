using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public GameObject playerModel;
    public GameObject vrPlayer;
    
    public int hp = 100;


    // Start is called before the first frame update
    void Start()
    {
        //if (!isLocalPlayer)
        //    vrPlayer.SetActive(false);
        //else
        //{
        //    playerModel.GetComponent<MeshRenderer>().enabled = false;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        var sqrlen = (gameObject.transform.position - vrPlayer.transform.position).sqrMagnitude; //計算玩家模型與VR玩家的距離，如果差距瞬移模型過去
        if(sqrlen > 0.1f){
            gameObject.transform.position = vrPlayer.transform.position;
        }
    }

    public void TakeDamage(GameObject g)
    {
        
    }
}
