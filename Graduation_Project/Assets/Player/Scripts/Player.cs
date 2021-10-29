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
    public GameObject playerRightHandModle;
    public GameObject teleport;
    public GameObject terrain;
    //public GameObject magicBallObj;
    public GameObject[] MagicsOBJ;
    public GameObject bullet;

    [Header("怪物")]
    public GameObject[] monster_prefabs;
    public GameObject my_monster;

    [SyncVar(hook = nameof(OnHpChange))]
    public float hp;
    public float max_hp = 100.0f;

    [SerializeField] public GameObject hp_bar;
    [SerializeField] public GameObject hp_vr_text;


    private void Awake()
    {
        vrCamera = GameObject.Find("Player/SteamVRObjects/VRCamera");
        RightController = GameObject.Find("Player/SteamVRObjects/RightHand/Controller (right)");
        hp_vr_text = GameObject.Find("Player/Canvas/Panel/Text");
        //playerRightHandModle = Instantiate(playerRightHandModle);
    }
    // Start is called before the first frame update
    public override void OnStartLocalPlayer()
    {
        transform.position = vrCamera.transform.position;
        transform.rotation = vrCamera.transform.rotation;
        GameObject t = Instantiate(teleport, new Vector3(3, 0, 0), new Quaternion(0, 0, 0, 0));
        GameObject RightController = GameObject.Find("Player/SteamVRObjects/RightHand/Controller (right)");
        RightController.GetComponent<VRRightHand>().setTeleporting(t);
        Instantiate(terrain, transform.position - new Vector3(0, 1, 0), new Quaternion(0, 0, 0, 0));
        

        hp = 100.0f;
        CmdSetUpPlayer(hp);//在連線上初始化玩家血量
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        transform.position = vrCamera.transform.position;
        transform.rotation = Quaternion.Euler( 0 , vrCamera.transform.rotation.y , 0);
        
        playerRightHandModle.transform.position = RightController.transform.position;
        playerRightHandModle.transform.rotation = RightController.transform.rotation;


        if ( SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))// 右手手把開搶鍵有bug ,會導致遊戲崩潰，請有空的隊友幫忙debug
        {
            if (bullet != null)
            {
                CmdFly();
            }

        }

    }

    [Command]
    public void CmdSetUpPlayer(float _hp ){
        hp = _hp;
    }

    void OnHpChange(float _Old, float _New)
    {
        hp = _New;
        //hp_bar.transform.localScale = new Vector3((hp/max_hp) , 1, 1); //改頭上UI顯示寫廖

        if(isLocalPlayer)
            hp_vr_text.GetComponent<Text>().text = hp.ToString(); //改頭盔UI顯示寫廖
    } 

    [Command]
    void CMDchangeHp(int damage){
        RpcChangeHp(damage);
        hp_bar.transform.localScale = new Vector3((hp / max_hp), 1, 1); //改頭上UI顯示寫廖
        //hp_vr_text.GetComponent<Text>().text = hp.ToString(); //改頭盔UI顯示寫廖
    }

    public void TakeDamage(GameObject g)
    {
        int damage;
        if (g.tag == "MagicBall")
        {
            damage = g.GetComponent<MagicBall>().atk;
        }
        else if(g.tag == "Monster")
        {
            damage = g.GetComponent<Monster>().atk;
            print("一待");
        }
        else
        {
            damage = 0;
        }
        CMDchangeHp(damage);
        print("Take damage");
    }

    [ClientRpc]
    void RpcChangeHp(int damage){
        hp -= damage;
        hp_bar.transform.localScale = new Vector3((hp / max_hp), 1, 1); //改頭上UI顯示寫廖

        //hp_vr_text.GetComponent<Text>().text = hp.ToString(); //改頭盔UI顯示寫廖
    }

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

            // this is called on the server
    [Command]
    public void CmdCreatMagicBall(int ans)
    {
        if (bullet == null && MagicsOBJ[ans] != null){
            RpcCreatMagicBall(ans);
        }
    }

    [ClientRpc]
    void RpcCreatMagicBall(int ans)
    {
        bullet = Instantiate(MagicsOBJ[ans],
                    playerRightHandModle.transform.position - 0.2f * Vector3.down + 0.2f * playerRightHandModle.transform.forward,
                    playerRightHandModle.transform.rotation,
                    playerRightHandModle.transform);

    }

    [Command]
    public void CmdFly(){
        RpcFly();
    }

    [ClientRpc]
    void RpcFly(){
       bullet.GetComponent<Rigidbody>().velocity = playerRightHandModle.transform.forward * bullet.GetComponent<MagicBall>().speed * Time.deltaTime;
       bullet.GetComponent<MagicBall>().magicBallDestory();
       bullet.transform.SetParent(null);
       bullet = null;
    }

    public void selectMonster(int monster_num)
    {
        if(monster_prefabs[monster_num] != null)
            CmdCreatMonster(monster_num);
    }

    [Command] void CmdCreatMonster(int monster_num)
    {
        RpcCreatMonster(monster_num);
    }

    [ClientRpc]void RpcCreatMonster(int monster_num)
    {
        my_monster = Instantiate(monster_prefabs[monster_num]);
    }

}
