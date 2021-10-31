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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
