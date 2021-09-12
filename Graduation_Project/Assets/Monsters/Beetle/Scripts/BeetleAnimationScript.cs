using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleAnimationScript : MonoBehaviour
{
    private Vector3 origin;//儲存導航網格代理的初始位置
    private UnityEngine.AI.NavMeshAgent nma;//儲存導航網格代理元件

    private GameObject target = null;
    public GameObject attackCollider;    
    
    private Quaternion rotationRecord;
    public bool died = false;
    // Start is called before the first frame update
    void Start()
    {
        nma = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();   //取得導航網格代理元件
        origin = transform.position;     // 儲存一下這個指令碼所掛載遊戲物體的初始位置
        rotationRecord = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (died) 
        {
            GetComponent<Animation>().Play("Die");
            Destroy(gameObject, 35 * Time.deltaTime);
            return; 
        }
        // get enemy or idle in place
        target = GameObject.Find("Cube");
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
        nma.SetDestination(target.transform.position);
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
        spawnAttackObject();
    }

    void spawnAttackObject()
    {
        Vector3 ribcageGizmoPos = gameObject.transform.GetChild(9).transform.position;
        Vector3 pelvisGizmoPos = gameObject.transform.GetChild(8).transform.position;
        Vector3 vec = ribcageGizmoPos - pelvisGizmoPos;
        vec[1] = 0;
        Instantiate(attackCollider, 
        transform.position + 3.3f * vec,
         Quaternion.identity);
    }

    public void getHit()
    {
        died = true;
    }
}
