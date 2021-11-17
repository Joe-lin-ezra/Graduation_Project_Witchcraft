using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Wall : NetworkBehaviour
{
    public void TakeDamage(GameObject rock)
    {
        rock.SetActive(false);
        /*if(rock != null) {
            print("AAA");
            NetworkBehaviour.Destroy(rock);
            
        }*/
    }
}
