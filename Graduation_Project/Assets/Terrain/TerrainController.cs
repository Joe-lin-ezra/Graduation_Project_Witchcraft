using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TerrainController : NetworkBehaviour
{
    Terrain this_terrain;
    int hm_width;
    int hm_height;
    int correct_HM_array_X;
    int correct_HW_array_Y;

    // Start is called before the first frame update
    void Start()
    {
        this_terrain = Terrain.activeTerrain;
        hm_width = this_terrain.terrainData.heightmapResolution;
        hm_height = this_terrain.terrainData.heightmapResolution;
    }

    [Command]
    void ConverPosition(Vector3 target_position)
    {
        Vector3 temp_position = (target_position - this.gameObject.transform.position);

        Vector3 pos;

        pos.x = temp_position.x / this_terrain.terrainData.size.x; // 除地圖大小寬
        pos.y = temp_position.y / this_terrain.terrainData.size.y; // 除地圖大小高
        pos.z = temp_position.z / this_terrain.terrainData.size.z; // 除地圖大小長

        correct_HM_array_X = (int)(pos.x * hm_width);
        correct_HW_array_Y = (int)(pos.z * hm_height);
    }

    float[,] makeRectangleHeights(float[,] heights, int startX, int startZ, int theLong, int width, float setHeight)
    {
        for (int z = startZ; z < startZ + width; z++)
        {
            for (int x = startX; x < startX + theLong; x++)
            {
                heights[x, z] = setHeight;
            }
        }
        return heights;
    }

    [Command]
    public void RiseTerrain(Vector3 target_position)
    {
        ConverPosition(target_position);

        float[,] heights = this_terrain.terrainData.GetHeights(0, 0, hm_width, hm_height);

        heights = makeRectangleHeights(heights, correct_HW_array_Y, correct_HM_array_X, 10, 10, 10.0f / 600);
        this_terrain.terrainData.SetHeights(0, 0, heights);
        print("END");
    }
}
