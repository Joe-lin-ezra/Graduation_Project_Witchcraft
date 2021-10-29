using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MonsterManager : MonoBehaviour
{
    public GameObject enemyPlayer = null;

    public void SetEnemy(GameObject _enemyPlayer)
    {
        print(_enemyPlayer.tag);
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
