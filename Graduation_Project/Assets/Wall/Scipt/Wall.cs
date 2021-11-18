using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Wall : NetworkBehaviour
{
    [Command]
    public void CmdTakeDamage(GameObject rock)
    {
        //rock.SetActive(false);
        
    }

}
