using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicControl: MonoBehaviour
{
    // Start is called before the first frame update
    public  GameObject[] MagicsOBJ ;

    [Header("VR Controller")]
    public GameObject RightController;

    [Header("For Test")]
    public bool debug = false;
    public string testString;

    public void Start()
    {
        RightController = GameObject.Find("Player/SteamVRObjects/RightHand/Controller (right)");
        Debug.Log(RightController);
    }

    private void magicInstantiate(GameObject magic)
    {
        if (!debug)
        {
            RightController.GetComponent<VRRightHand>().bullet = Instantiate(magic, RightController.transform.position, RightController.transform.rotation, RightController.transform);
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
        GameObject g = null;
        foreach(GameObject m in MagicsOBJ)
        {
            int i = text.IndexOf(m.GetComponent<MagicBall>().magicName);
            if (i < minIndex && i != -1)
            {
                minIndex = i;
                g = m;
            }
        }

        if (minIndex == -1)
        {
            Debug.LogWarning("Magic name not found!!");
            return text;
        }
        else
        {
            magicInstantiate(g);
            return "";
        }
    }
}
