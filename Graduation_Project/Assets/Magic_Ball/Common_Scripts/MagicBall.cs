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

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.tag = "MagicBall";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BulletDestory()
    {
        Destroy(gameObject, destoryTime);
    }
    void OnTriggerEnter(Collider other) // triger to destory
    {
        if(other.tag == "Player"){
            other.gameObject.GetComponent<Player>().TakeDamage(other.gameObject);
        }
        else if(other.tag == "Monster"){
            other.gameObject.GetComponent<Monster>().TakeDamage(other.gameObject);
        }
        if(other.tag == "Player" || other.tag == "Monster"){
            Instantiate(explodeEffect, transform.position , transform.rotation, transform);
            bulletEffect.SetActive(false);
            Destroy(gameObject, hitDestoryTime);
        }
    }
}
