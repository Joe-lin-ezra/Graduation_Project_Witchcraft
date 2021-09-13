using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManAttactedDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider c)
    {
        if (c.tag == "magic ball")
        {
            transform.parent.GetComponent<RockManAnimationController>().getHit();
        }
    }
}
