using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RockManAnimationController : NetworkBehaviour
{
    private Vector3 origin;//儲存導航網格代理的初始位置
    private UnityEngine.AI.NavMeshAgent nma;//儲存導航網格代理元件

    private GameObject target = null;
    
    private Quaternion rotationRecord;

    private bool workable = false;

    // finite state machine
    RockManStateEnum state = RockManStateEnum.idle;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        nma = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();   //取得導航網格代理元件
        origin = transform.position;     // 儲存一下這個指令碼所掛載遊戲物體的初始位置
        rotationRecord = transform.rotation;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Monster>().playerModle != null)
        {
            workable = true;
        }

        animator.SetInteger("state", (int)state);

        // die animation and destroy
        if (gameObject.GetComponent<Monster>().hp <= 0)
        {
            state = RockManStateEnum.die;
            nma.SetDestination(transform.position);  // need 40 frame to play die-animation
            Destroy(gameObject, 5);
            return; 
        }

        // get enemy or idle in place
        target = GetComponent<Monster>().enemy;
        if (target == null) 
        {
            state = RockManStateEnum.idle;
            return;   
        }

        // get enemy or idle in place
        target = GetComponent<Monster>().enemy;
        if (target == null)
        {
            GetComponent<Animation>().Play("Idle");
            return;
        }
        if (target != null && workable)
        {
            nma.SetDestination(target.transform.position);
        }

        // calculate distance between enemy and object, to decide
        // walk toward enemy, or attack
        double distance = Vector2.Distance(
            new Vector2(transform.position.x, transform.position.z),
            new Vector2(target.transform.position.x, target.transform.position.z)
        );  
        if (distance > 1.8f)
        {
            nma.SetDestination(target.transform.position);
            state = RockManStateEnum.walk;
            cmdSyncPlanePosition(transform.position, transform.rotation);
            return;
        }

        // check gameobject faces target
        Vector3 targetDirection = target.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        // spawn object to surpport
        state = RockManStateEnum.attack1;
        nma.SetDestination(transform.position);
        GetComponent<Monster>().Attack();
    }

    [Command]
    private void cmdSyncPlanePosition(Vector3 currentPosition, Quaternion currentRotation)
    {
        ServerSyncPlayer(currentPosition, currentRotation);
    }

    [ClientRpc]
    private void ServerSyncPlayer(Vector3 currentPosition, Quaternion currentRotation)
    {
        transform.position = currentPosition;
        transform.rotation = currentRotation;
    }
}
