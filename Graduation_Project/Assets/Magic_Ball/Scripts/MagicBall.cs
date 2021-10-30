using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public GameObject explodeEffect;
    public GameObject bulletEffect;

    [Header("Magic ball setting")]
    public string magicName;
    public int atk;
    public float speed;
    public float destoryTime;


    private int explosionDestoryTime = 2;


    // Start is called before the first frame update
    void Awake()
    {
        gameObject.tag = "MagicBall";
        destoryTime = 10.0f;
        bulletEffect = transform.GetChild(0).gameObject;
        GetComponent<SphereCollider>().enabled = false;
    }

    public void magicBallDestory()
    {
        Destroy(gameObject, destoryTime);
    }

    void OnTriggerEnter(Collider other) // instantiate explosion and set destory time
    {
        print(other.name);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        if (other.tag == "Player"){
            other.gameObject.GetComponent<Player>().TakeDamage(gameObject);
        }
        else if(other.tag == "Monster"){
            print(other.gameObject.name);
            other.gameObject.GetComponent<BeetleAttackedDetector>().TakeDamage(gameObject);
        }
        bulletEffect.SetActive(false);
        GameObject ex = Instantiate(explodeEffect, transform.position, transform.rotation, transform);
        Destroy(gameObject, explosionDestoryTime);
    }
}
