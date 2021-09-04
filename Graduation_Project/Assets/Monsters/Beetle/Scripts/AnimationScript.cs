using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Vector3 origin;//儲存導航網格代理的初始位置
    private UnityEngine.AI.NavMeshAgent nma;//儲存導航網格代理元件

    private GameObject target = null;

    // Start is called before the first frame update
    void Start()
    {
        nma = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();   //取得導航網格代理元件
        origin = transform.position;     // 儲存一下這個指令碼所掛載遊戲物體的初始位置
    }

    // Update is called once per frame
    void Update()
    {
        // get enemy object
        target = GameObject.Find("Cube");

        // if get target object, then go forward and attack
        if (target != null) 
        {
            // get the distance between self and target object
            double distance = Vector3.Distance(transform.position, target.transform.position);
            nma.SetDestination(target.transform.position);
            // 
            if (distance < 2.5)
            {
                nma.SetDestination(transform.position);
                GetComponent<Animation>().Play("Stab Attack");
            }
            else 
            {
                // run to close target object
                GetComponent<Animation>().Play("Run Forward In Place");
            }
            Debug.Log("target position: " + target.transform.position + "\ndistance: " + distance);
        }
        else
        {
            GetComponent<Animation>().Play("Idle");
        }
        
        
    }
}
