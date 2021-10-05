using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public GameObject playerModel;
    public GameObject vrPlayer;
    public GameObject vrCame;
    public GameObject teleprot;
    public GameObject terrain;

    public int hp = 100;


    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            vrPlayer.SetActive(false);
        }
        else
        {
            Instantiate(teleprot, new Vector3(3, 0, 0), new Quaternion(0, 0, 0, 0));
            Instantiate(terrain, this.gameObject.transform.position, new Quaternion(0, 0, 0, 0));
            //playerModel.GetComponent<MeshRenderer>().enabled = false;
            playerModel.gameObject.transform.position = vrCame.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        var sqrlen = (gameObject.transform.position - vrPlayer.transform.position).sqrMagnitude; //計算玩家模型與VR玩家的距離，如果差距瞬移模型過去
        if(sqrlen > 0.1f){
            CmdPlayerMove();
        }
    }

    [Command]
    void CmdPlayerMove()
    {
        playerModel.gameObject.transform.position = vrCame.transform.position;
    }


    public void TakeDamage(GameObject g)
    {
        
    }

}
