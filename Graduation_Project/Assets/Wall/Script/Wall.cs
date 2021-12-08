using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Wall : NetworkBehaviour
{
    public GameObject[] littel_rock;
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
        CmdBoom(rock.transform.position);
        Destroy(rock, 0);
    }

    [Command]
    void CmdBoom(Vector3 rock_position)
    {
        /*GameObject wall_clone = Instantiate(littel_rock[0], rock_position, this.transform.rotation);
        wall_clone.AddComponent<Rigidbody>();
        wall_clone.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        wall_clone.GetComponent<Rigidbody>().AddForce(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2), ForceMode.Impulse);
        GameObject owner = this.gameObject;
        NetworkServer.Spawn(wall_clone, owner);*/
        foreach (GameObject element in littel_rock)
        { 
            GameObject wall_clone = Instantiate(littel_rock[0], rock_position, this.transform.rotation);
        wall_clone.AddComponent<Rigidbody>();
        wall_clone.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        wall_clone.GetComponent<Rigidbody>().AddForce(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5), ForceMode.Impulse);
        GameObject owner = this.gameObject;
        }
    }
}
