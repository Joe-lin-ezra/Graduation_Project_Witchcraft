using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MonsterManager : MonoBehaviour
{
    public GameObject enemyPlayer;

    public void SetEnemy(GameObject _enemyPlayer)
    {
        if (_enemyPlayer.tag == "Player" && this.enemyPlayer == null)
        {
            this.enemyPlayer = _enemyPlayer;
        }
    }

    GameObject GetEnemyPlayer() 
    {
        return this.enemyPlayer;
    }
}
