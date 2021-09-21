using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster: MonoBehaviour
{
    public MonsterTypeEnum type;
    public int hp;
    public int atk;

    public GameObject enemy;
    public GameObject attackCollider; 

    void Start() 
    {
         this.hp = 1;
         this.atk = 1;
    }

    void Update() 
    {
        if (enemy == null)
        {
            SetEnemy();
        }
    }

    void SetEnemy()
    {
        this.enemy = GameObject.Find("Monster Manager").GetComponent<MonsterManager>().enemyPlayer;
    }

    public void TakeDamage(GameObject g)
    {
        if (g.tag == "MagicBall")
        {
            this.hp -= g.GetComponent<MagicBall>().atk;
        }
    }

    public void Attack() 
    {
        Instantiate(attackCollider, 
        1.75f * transform.TransformDirection(Vector3.forward) + new Vector3(0, 0.2f, 0),
        Quaternion.identity,
        transform);
    }
}
