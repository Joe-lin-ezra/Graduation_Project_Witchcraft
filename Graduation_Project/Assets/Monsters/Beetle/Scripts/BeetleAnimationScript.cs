using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BeetleAnimationScript : NetworkBehaviour
{
    private Vector3 origin;//儲存導航網格代理的初始位置
    private UnityEngine.AI.NavMeshAgent nma;//儲存導航網格代理元件

    private GameObject target = null;
    private Quaternion rotationRecord;

    private bool workable = false;

    // Start is called before the first frame update
    void Start()
    {
        nma = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();   //取得導航網格代理元件
        origin = transform.position;     // 儲存一下這個指令碼所掛載遊戲物體的初始位置
        rotationRecord = transform.rotation;

        if ( this.gameObject.GetComponent<Monster>().playerModle == NetworkClient.localPlayer.gameObject) //如果此怪物的傭有者是本地玩家則可以移動
            workable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!workable)
            return;

        if (gameObject.GetComponent<Monster>().hp <= 0) 
        {
            GetComponent<Animation>().Play("Die");
            Destroy(gameObject, 35 * Time.deltaTime);
            return; 
        }
        // get enemy or idle in place
        target = GetComponent<Monster>().enemy;
        if (target == null) 
        {
            GetComponent<Animation>().Play("Idle");
            return;   
        }

        // calculate distance between enemy and object, to decide
        // walk toward enemy, or attack
        double distance = Vector2.Distance(
            new Vector2(transform.position.x, transform.position.z),
            new Vector2(target.transform.position.x, target.transform.position.z)
        );
        if(target != null)
        {
            nma.SetDestination(target.transform.position);
        }

        if (distance > 1.8f)
        {
            GetComponent<Animation>().Play("Run Forward In Place");
            return;
        }

        // check gameobject faces target
        Vector3 targetDirection = target.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        // spawn object to surpport
        GetComponent<Animation>().Play("Stab Attack");
        nma.SetDestination(transform.position);
        gameObject.GetComponent<Monster>().Attack();

        cmdSyncPlanePosition(transform.position, transform.rotation);
    }

    [Command] private void cmdSyncPlanePosition(Vector3 currentPosition, Quaternion currentRotation)
    {
        ServerSyncPlayer(currentPosition, currentRotation);
    }

    [ClientRpc] private void ServerSyncPlayer(Vector3 currentPosition, Quaternion currentRotation)
    {
        transform.position = currentPosition;
        transform.rotation = currentRotation;
    }

}
