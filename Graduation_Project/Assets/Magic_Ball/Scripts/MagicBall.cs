﻿using System.Collections;
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
    public int destoryTime;


    private int hitDestoryTime = 2;


    // Start is called before the first frame update
    void Awake()
    {
        gameObject.tag = "MagicBall";
        bulletEffect = transform.GetChild(0).gameObject;
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
        GameObject ex = Instantiate(explodeEffect, transform.position, transform.rotation);
        //Debug.Log("enter");
        if (other.tag == "Player"){
            other.gameObject.GetComponent<Player>().TakeDamage(other.gameObject);
        }
        else if(other.tag == "Monster"){
            other.gameObject.GetComponent<Monster>().TakeDamage(other.gameObject);
        }
        

        bulletEffect.SetActive(false);
        Destroy(ex, hitDestoryTime);
    }
}
