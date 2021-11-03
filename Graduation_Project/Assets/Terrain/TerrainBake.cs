using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TerrainBake : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
