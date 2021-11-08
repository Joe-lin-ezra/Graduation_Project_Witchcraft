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
    [SerializeField] public GameObject hp_vr_text;

    public float hands_current_distance = 0.0f;
    public float hands_first_point = 0;

    private void Awake()
    {
        vrCamera = GameObject.Find("Player/SteamVRObjects/VRCamera");
        RightController = GameObject.Find("Player/SteamVRObjects/RightHand/Controller (right)");
        LeftController = GameObject.Find("Player/SteamVRObjects/LeftHand/Controller (left)");
        hp_vr_text = GameObject.Find("Player/Canvas/Panel/Text");
    }

    // Start is called before the first frame update
    public override void OnStartLocalPlayer()
    {
        transform.position = vrCamera.transform.position;
        transform.rotation = vrCamera.transform.rotation;

        if (isServer)
            CmdCreatTerrain();

        pointer.transform.localScale = new Vector3(thickness, thickness, 100f);//設定雷射預設大小

        hp = 100.0f;
        CmdSetUpPlayer(hp);//在連線上初始化玩家血量
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        transform.position = vrCamera.transform.position;
        transform.rotation = Quaternion.Euler(0, vrCamera.transform.rotation.y, 0);

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
            if(hands_first_point - hands_current_distance > 0.9f)
            {
                print("AAAAA");
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

        if (invincibleTime < 0) invincibleTime -= Time.deltaTime;
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

        if (isLocalPlayer)
            hp_vr_text.GetComponent<Text>().text = hp.ToString();             // change vr-head UI hp text 
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
                    playerRightHandModle.transform.position - 0.2f * Vector3.down + 0.2f * playerRightHandModle.transform.forward,
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



    /* [ClientRpc] void RpcCreatMonster(int monster_num)
     {
         monster_clone = Instantiate(monster_prefabs[monster_num]);
         monster_clone.GetComponent<Monster>().SetEnemy(this.gameObject.GetComponent<MonsterManager>().enemyPlayer);
         monster_clone.GetComponent<Monster>().playerModle = this.gameObject;
         GameObject owner = this.gameObject;
     }*/


    //  =================================       Command                ===================================


    // change the relative player-model vr-head hp_bar
    [Command]
    void CMDchangeHp(int damage)
    {
        RpcChangeHp(damage);
        hp_bar.transform.localScale = new Vector3((hp / max_hp), 1, 1);
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
        monster_clone = Instantiate(monster_prefabs[monster_num]);    
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
}
