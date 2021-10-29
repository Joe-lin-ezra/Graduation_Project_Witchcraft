using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MonsterManager : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnEnmyChange))]
    public GameObject enemyPlayer = null;


    void OnEnmyChange(GameObject _Old, GameObject _New)
    {
        enemyPlayer = _New;
    }

    public void SetEnemy(GameObject _enemyPlayer)
    {
        if (_enemyPlayer.tag == "Player" && this.enemyPlayer != this.gameObject)
        {
            this.enemyPlayer = _enemyPlayer;
        }
    }

    GameObject GetEnemyPlayer() 
    {
        return this.enemyPlayer;
    }
}
