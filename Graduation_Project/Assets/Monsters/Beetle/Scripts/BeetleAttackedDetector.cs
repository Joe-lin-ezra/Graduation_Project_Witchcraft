using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleAttackedDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Monster";
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void TakeDamage(GameObject other)
    {
        if (other.tag == ("MagicBall")) 
        {
            transform.parent.gameObject.GetComponent<Monster>().TakeDamage(other);
        }
    }
}
