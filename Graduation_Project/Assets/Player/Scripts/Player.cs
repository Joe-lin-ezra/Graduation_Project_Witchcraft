using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Mirror;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    public GameObject vrCamera;
    public GameObject RightController;
    public GameObject LeftController;

    public GameObject playerRightHandModle;
    public GameObject playerLeftHandModle;
    public GameObject playerLeftHandLaserPoint;
    public GameObject pointer;
    public float thickness = 0.02f;
    public GameObject leftHandLaserHitObject = null;

    public GameObject teleport;
    public GameObject terrain;

    public GameObject[] MagicsOBJ;
    public GameObject bullet;

    [Header("怪物")]
    public GameObject[] monster_prefabs;
    public GameObject my_monster;
    GameObject monster_clone;

    [SyncVar(hook = nameof(OnHpChange))]
    public float hp;
    public float max_hp = 100.0f;
    public float invincibleTime = 0;

    [SerializeField] public GameObject hp_bar;
    [SerializeField] public GameObject hp_vr_bar;

    public float hands_current_distance = 0.0f;
    public float hands_first_point = 0;

    [Header("地形控制")]
    public GameObject tc;
    Terrain this_terrain;
    int hm_width;
    int hm_height;
    int correct_HM_array_X;
    int correct_HW_array_Y;
    public GameObject wall_prefab;

    private void Awake()
    {
        vrCamera = GameObject.Find("Player/SteamVRObjects/VRCamera");
        RightController = GameObject.Find("Player/SteamVRObjects/RightHand/Controller (right)");
        LeftController = GameObject.Find("Player/SteamVRObjects/LeftHand/Controller (left)");
        hp_vr_bar = GameObject.Find("Player/Canvas/Panel/HPBAR/Image");
    }

    // Start is called before the first frame update
    public override void OnStartLocalPlayer()
    {
        transform.position = vrCamera.transform.position;
        //transform.rotation = vrCamera.transform.rotation;
        transform.rotation = new Quaternion(transform.rotation.x, vrCamera.transform.rotation.y, transform.rotation.z, transform.rotation.w);

        if (isServer)
            CmdCreatTerrain();

        pointer.transform.localScale = new Vector3(thickness, thickness, 100f);//設定雷射預設大小

        hp = 100.0f;
        CmdSetUpPlayer(hp);//在連線上初始化玩家血量

        //獲取地形相關資訊
        this_terrain = Terrain.activeTerrain;
        hm_width = this_terrain.terrainData.heightmapResolution;
        hm_height = this_terrain.terrainData.heightmapResolution;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleTime > 0) invincibleTime -= Time.deltaTime;

        if (!isLocalPlayer)
            return;
        transform.position = vrCamera.transform.position;
       // transform.rotation = Quaternion.Euler(0, vrCamera.transform.rotation.y, 0);
        transform.rotation = new Quaternion(transform.rotation.x, vrCamera.transform.rotation.y, transform.rotation.z, transform.rotation.w);

        // player___HandModle: networking gameobject
        // ___Controller: local gameobject
        playerRightHandModle.transform.position = RightController.transform.position;
        playerRightHandModle.transform.rotation = RightController.transform.rotation;
        playerLeftHandModle.transform.position = LeftController.transform.position;
        playerLeftHandModle.transform.rotation = LeftController.transform.rotation;

        float hand_distant = TwoPointDistance3D(playerRightHandModle.transform.position, playerLeftHandModle.transform.position);

        if (hand_distant < 0.1)
        {
            hands_current_distance = playerRightHandModle.transform.position.y;
            if (hands_first_point == 0.0f)
            {
                hands_first_point = playerRightHandModle.transform.position.y;
            }
            if(hands_first_point - hands_current_distance > 0.6f)
            {
                CmdWall();
                Debug.Log("生成牆面");
                hands_first_point = 0.0f;
            }
        }
        else
        {
            hands_first_point = 0.0f;
        }

        // shoot bullet, when right-hand-controller-grab-pinch is grabbed 
        if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            if (bullet != null)
            {
                //bullet.GetComponent<SphereCollider>().enabled = true;
                // notify server to run fly function
                CmdFly();
            }
        }

        // select enemy by grabbing right-hand-controller-grab-pinch
        if (SteamVR_Actions.default_GrabPinch.GetState(SteamVR_Input_Sources.LeftHand))
        {
            CmdLeftHanderLaserSwitch();
        }
        else
        {
            pointer.transform.localScale = new Vector3(0, 0, 0);
        }

        
    }

    private float TwoPointDistance3D(Vector3 p1, Vector3 p2)
    {

        float i = Mathf.Sqrt((p1.x - p2.x) * (p1.x - p2.x)
                            + (p1.y - p2.y) * (p1.y - p2.y)
                            + (p1.z - p2.z) * (p1.z - p2.z));

        return i;
    }

    void OnHpChange(float _Old, float _New)
    {
        hp = _New;
        Debug.Log("hp changed!");
        /*if (isLocalPlayer){
            hp_vr_text.GetComponent<Text>().text = hp.ToString();             // change vr-head UI hp text
            Debug.Log("is local player");
        }*/
        Debug.Log("is not local player");
             
    }
    
    public void selectMonster(int monster_num)
    {
        if (monster_prefabs[monster_num] != null && monster_num != 0)
            CmdCreatMonster(monster_num);
    }

    // find the primary magic ball
    public void sellectMagicBall(string text)
    {
        int minIndex = text.Length;
        int count = 0;
        int ans = 0;
        foreach (GameObject m in MagicsOBJ)
        {
            int i = text.IndexOf(m.GetComponent<MagicBall>().magicName);
            if (i < minIndex && i != -1)
            {
                minIndex = i;
                ans = count;
            }
            count++;
        }
        CmdCreatMagicBall(ans);
    }

    public void TakeDamage(GameObject g)
    {
        int damage;
        if (g.tag == "MagicBall" && invincibleTime <= 0)
        {
            damage = g.GetComponent<MagicBall>().atk;
            invincibleTime = 5.0f;
        }
        else if (g.tag == "Monster" && invincibleTime <= 0)
        {
            damage = g.GetComponent<Monster>().atk;
            invincibleTime = 5.0f;
        }
        else
        {
            damage = 0;
        }
        CMDchangeHp(damage);
    }

    // ==================================          Client RPC         ==========================================

    // change the player hp_bar
    [ClientRpc]
    void RpcChangeHp(int damage)
    {
        hp -= damage;
        print(hp);
        hp_bar.transform.localScale = new Vector3((hp / max_hp), 1, 1);
        if (isLocalPlayer)
            hp_vr_bar.transform.localScale = hp_bar.transform.localScale;
    }
    
    [ClientRpc]
    void RpcLeftHanderLaserSwitch()
    {
        float dist = 100f;

        Ray raycast = new Ray(playerLeftHandLaserPoint.transform.position, playerLeftHandLaserPoint.transform.forward);
        RaycastHit hit;
        bool bHit = Physics.Raycast(raycast, out hit);

        if (!bHit)
        {
            leftHandLaserHitObject = null;
        }
        if (bHit && leftHandLaserHitObject != hit.transform.gameObject)
        {
            leftHandLaserHitObject = hit.transform.gameObject;

            if (leftHandLaserHitObject.tag.Equals("Player")) //是 Player
            {
                this.GetComponent<MonsterManager>().SetEnemy(leftHandLaserHitObject);
            }
        }
        if (bHit && hit.distance < 100f)
        {
            dist = hit.distance;
        }
        pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, 20.0f * dist);
        pointer.transform.localPosition = new Vector3(0f, 0f, dist * 20.0f / 2f);
    }



    [ClientRpc]
    void RpcCreatMagicBall(int ans)
    {
        bullet = Instantiate(MagicsOBJ[ans],
                    playerRightHandModle.transform.position + 0.25f * playerRightHandModle.transform.forward,
                    playerRightHandModle.transform.rotation,
                    playerRightHandModle.transform);

    }

    [ClientRpc]
    void RpcFly()
    {
        bullet.GetComponent<SphereCollider>().enabled = true;
        bullet.GetComponent<Rigidbody>().velocity = playerRightHandModle.transform.forward * bullet.GetComponent<MagicBall>().speed * Time.deltaTime;
        bullet.GetComponent<MagicBall>().magicBallDestory();
        bullet.transform.SetParent(null);
        bullet = null;
    }

    [ClientRpc]
    void RpcSetEnemy(GameObject monster_clone)
    {
        monster_clone.GetComponent<Monster>().SetEnemy(this.gameObject.GetComponent<MonsterManager>().enemyPlayer);
        monster_clone.GetComponent<Monster>().playerModle = this.gameObject;
        //monster_clone.GetComponent<BeetleAnimationScript>().workable = this.gameObject.GetComponent<Monster>().playerModle == NetworkClient.localPlayer.gameObject;
    }

    [ClientRpc]
    public void RpcRiseTerrain(Vector3 target_position)
    {
        ConverPosition(target_position);

        float[,] heights = this_terrain.terrainData.GetHeights(0, 0, hm_width, hm_height);

        heights = makeRectangleHeights(heights, correct_HW_array_Y, correct_HM_array_X, 10, 10, 10.0f / 600);
        this_terrain.terrainData.SetHeights(0, 0, heights);
        print("END");
    }



    /* [ClientRpc] void RpcCreatMonster(int monster_num)
     {
         monster_clone = Instantiate(monster_prefabs[monster_num]);
         monster_clone.GetComponent<Monster>().SetEnemy(this.gameObject.GetComponent<MonsterManager>().enemyPlayer);
         monster_clone.GetComponent<Monster>().playerModle = this.gameObject;
         GameObject owner = this.gameObject;
     }*/


    //  =================================       Command                ===================================


    [Command]
    void CmdWall()
    {
        // need to modify position, the position depends on the user VRhead direction, not x+4 or z+2
        Vector3 pos = new Vector3 (this.transform.position.x + transform.forward.x * 5 , this.transform.position.y-0.5f , this.transform.position.z + transform.forward.z * 5);
       
        GameObject wall_clone = Instantiate(wall_prefab , pos, new Quaternion(wall_prefab.transform.rotation.x, this.transform.rotation.y*-1 , wall_prefab.transform.rotation.z, wall_prefab.transform.rotation.w));
        GameObject owner = this.gameObject;
        NetworkServer.Spawn(wall_clone, owner);
    }

    // change the relative player-model vr-head hp_bar
    [Command]
    void CMDchangeHp(int damage)
    {
        RpcChangeHp(damage);
        hp_bar.transform.localScale = new Vector3((hp / max_hp), 1, 1);
        if (isLocalPlayer)
            hp_vr_bar.transform.localScale = hp_bar.transform.localScale;
    }

    // this is called on the server
    [Command]
    public void CmdCreatMagicBall(int ans)
    {
        if (bullet == null && MagicsOBJ[ans] != null)
        {
            RpcCreatMagicBall(ans);
        }
    }

    [Command]
    void CmdCreatMonster(int monster_num)
    {
        Vector3 pos = new Vector3(this.transform.position.x + transform.forward.x * 4, this.transform.position.y, this.transform.position.z + transform.forward.z * 2);
        monster_clone = Instantiate(monster_prefabs[monster_num] , pos, this.transform.rotation);    
        GameObject owner = this.gameObject;
        NetworkServer.Spawn(monster_clone, owner);
        RpcSetEnemy(monster_clone);
    }
    

    [Command]
    public void CmdSetUpPlayer(float _hp)
    {
        hp = _hp;
    }

    [Command]
    void CmdCreatTerrain()
    {
        GameObject t = Instantiate(teleport, new Vector3(3, 0, 0), new Quaternion(0, 0, 0, 0));
        RightController.GetComponent<VRRightHand>().setTeleporting(t);
        NetworkServer.Spawn(t);

        GameObject terrain_clone = Instantiate(terrain, transform.position - new Vector3(0, 1, 0), new Quaternion(0, 0, 0, 0));
        NetworkServer.Spawn(terrain_clone);
    }

    [Command]
    public void CmdFly()
    {
        RpcFly();
    }

    [Command]
    void CmdLeftHanderLaserSwitch()
    {
        // to notify all relative gameobject to do the laser
        RpcLeftHanderLaserSwitch();
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
    public void CmdRiseTerrain(Vector3 target_position)
    {
        //RpcRiseTerrain(target_position);
        ConverPosition(target_position);

        float[,] heights = this_terrain.terrainData.GetHeights(0, 0, hm_width, hm_height);

        heights = makeRectangleHeights(heights, correct_HW_array_Y, correct_HM_array_X, 10, 10, 10.0f / 600);
        this_terrain.terrainData.SetHeights(0, 0, heights);
        print("Commamd END");

    }

}
