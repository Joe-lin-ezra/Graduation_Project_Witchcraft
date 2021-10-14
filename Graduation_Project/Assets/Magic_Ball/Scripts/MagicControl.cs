using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class MagicControl: MonoBehaviour
{
    // Start is called before the first frame update
    public  GameObject[] MagicsOBJ ;

    [Header("VR Controller")]
    public GameObject RightController;

    [Header("For Test")]
    public bool debug = false;
    public string testString;
    
    [Header("Mirror")]
    public GameObject playerModle;

    public void Start()
    {
        RightController = GameObject.Find("Player/SteamVRObjects/RightHand/Controller (right)");
        Debug.Log(RightController);
    }

    private void magicInstantiate(GameObject magic)
    {
        if (!debug)
        {
            if (RightController.GetComponent<VRRightHand>().bullet == null)
                RightController.GetComponent<VRRightHand>().bullet =
                    Instantiate(magic,
                    RightController.transform.position - 0.2f * Vector3.down + 0.2f * RightController.transform.forward,
                    RightController.transform.rotation,
                    RightController.transform);
            else
                Debug.LogWarning("There has be a magic ball on your hand.");
        }
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("get Z");

                try
                {
                    GameObject magicBall = Instantiate(magic, RightController.transform.position, RightController.transform.rotation, RightController.transform);
                    RightController.GetComponent<VRRightHand>().bullet = magicBall;
                }
                catch (KeyNotFoundException e)
                {

                }
            }
        }
    }

    public string keywordExtractionAndInstantiate(string text)
    {
        int minIndex =text.Length;
        GameObject magic = null;
 
        foreach(GameObject m in MagicsOBJ)
        {
            int i = text.IndexOf(m.GetComponent<MagicBall>().magicName);
            if (i < minIndex && i != -1)
            {
                minIndex = i;
                magic = m;
            }
        }

        if (minIndex == -1 || magic == null)
        {
            Debug.LogWarning("Magic name not found!!");
            return text;
        }
        else if( magic != null)
        {
            //我先新增點咚咚喔 By 蛋蛋馬
            if( playerModle == null ){
                playerModle = NetworkClient.localPlayer.gameObject;
                //playerModle.GetComponent<Player>().CmdFire(magic);
                playerModle.GetComponent<Player>().sellectMagicBall(text);
            }
            else{
                //playerModle.GetComponent<Player>().CmdFire(magic);
                playerModle.GetComponent<Player>().sellectMagicBall(text);
            }
            //magicInstantiate(magic);
            return "";
        }
        else
        {
            return "";
        }
    }
}
