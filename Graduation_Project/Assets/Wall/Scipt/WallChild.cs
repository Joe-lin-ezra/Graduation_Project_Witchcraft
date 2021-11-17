using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChild : MonoBehaviour
{
    GameObject parent;

    private void Start()
    {
        parent = this.gameObject.transform.parent.gameObject;    
    }

    public void TakeDamage()
    {
        parent.GetComponent<Wall>().TakeDamage(this.parent);
    }
}
