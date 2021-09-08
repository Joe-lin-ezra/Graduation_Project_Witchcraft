using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackedDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Player";
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerStay(Collider c)
    {
        if (c.tag.Equals("magic ball")) 
        {
            Destroy(gameObject.transform.parent.gameObject, 0);
        }
    }
}
