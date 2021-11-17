using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Wall : NetworkBehaviour
{
    public void TakeDamage(GameObject rock)
    {
        if(rock != null) {
            Destroy(rock);
        }
    }
}
