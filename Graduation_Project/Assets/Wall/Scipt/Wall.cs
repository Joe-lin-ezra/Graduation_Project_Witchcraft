using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Wall : NetworkBehaviour
{
    public void TakeDamage(GameObject rock)
    {
        NetworkBehaviour.Destroy(rock);
    }
}
