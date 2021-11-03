using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Monster: NetworkBehaviour
{
    public MonsterTypeEnum type;
    public int hp;
    public int atk;

    public GameObject enemy;
    public GameObject attackCollider;
    public GameObject playerModle;

    void Start() 
    {
         this.hp = 1;
         this.atk = 1;
    }

    public void SetEnemy(GameObject me)
    {
        this.enemy = me;
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
        .75f * transform.forward,
        Quaternion.identity,
        transform);
    }
}
