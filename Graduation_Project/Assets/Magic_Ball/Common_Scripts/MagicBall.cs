using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MagicBall : MonoBehaviour
{
    public GameObject explodeEffect;
    public GameObject bulletEffect;

    [Header("Magic ball setting")]
    public int destoryTime;
    public float speed;
    public int atk;
    private int hitDestoryTime = 2;

    private Transform thetr;
    // Start is called before the first frame update
    void Start()
    {
        thetr = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, destoryTime);
    }

    void OnTriggerEnter(Collider other) // triger to destory
    {
        if(other.tag == "Player"){
           // other.gameObject.GetComponent<Player>().TakeDamage(other.gameObject);
        }
        else if(other.tag == "Monster"){
           // other.gameObject.GetComponent<Monster>().TakeDamage(other.gameObject);
        }
        if(other.tag == "Player" || other.tag == "Monster"){
            GameObject mge = Instantiate(explodeEffect, this.transform.position , this.transform.rotation);
            bulletEffect.SetActive(false);
            mge.transform.SetParent(this.gameObject.transform);
            Destroy(this.gameObject, hitDestoryTime);
        }
    }
}
