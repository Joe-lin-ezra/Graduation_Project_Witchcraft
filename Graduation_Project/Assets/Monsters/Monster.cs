using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Monster: MonoBehaviour
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

    void Update() 
    {
        if (enemy == null)
        {
            SetEnemy();
        }
    }

    void SetEnemy()
    {
        if (playerModle == null)
            playerModle = NetworkClient.localPlayer.gameObject;
        this.enemy = playerModle.GetComponent<MonsterManager>().enemyPlayer;
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
        1.75f * transform.TransformDirection(Vector3.forward),
        Quaternion.identity,
        transform);
    }
}
