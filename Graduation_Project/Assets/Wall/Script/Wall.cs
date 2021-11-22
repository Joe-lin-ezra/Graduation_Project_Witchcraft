using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Wall : NetworkBehaviour
{
    public void TakeDamage(GameObject rock)
    {
        // destroy the specific component to avoid
        // -> MissingReferenceException: The object of type 'Transform' has been destroyed but you are still trying to access it.
        NetworkTransformChild[] components = GetComponents<NetworkTransformChild>();
        foreach(NetworkTransformChild c in components)
        {
            if (c.target.name == rock.name)
            {
                Destroy(c, 0);
            }
        }
        Destroy(rock, 0);
        // rock.SetActive(false);
        //if(rock != null) {
        //    print("AAA");
        //    NetworkServer.Destroy(rock);
        //}
    }
}
