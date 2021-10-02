using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleCloudStreamingSpeechToText;

public class MagicControl: MonoBehaviour
{
    // Start is called before the first frame update
    public  GameObject[] MagicsOBJ ;
    public  string[] MagicStr;
    private Dictionary<string, GameObject> MagicDict;

    [Header("VR Controller")]
    public GameObject RightController;

    [Header("For Test")]
    public bool debug = false;
    public string testString;

    public void Start()
    {
        MagicDict = new Dictionary<string, GameObject>();
        for (int i = 0; i< MagicsOBJ.Length; i++)
        {
            MagicDict.Add(MagicStr[i], MagicsOBJ[i]);
        }
    }

    private void magicInstantiate(string magicName)
    {
        if (!debug)
        {
            if (string.IsNullOrEmpty(magicName))
            {
                Debug.LogWarning("Magic name not found!!");
                return;
            }
            magicName = magicName.ToLower();

            if (string.IsNullOrEmpty(RightController.GetComponent<VRRightHand>().magicName))
            {
                RightController.GetComponent<VRRightHand>().bullet = Instantiate(MagicDict[magicName], RightController.transform.position, RightController.transform.rotation, RightController.transform);
                RightController.GetComponent<VRRightHand>().magicName = magicName;
            }
        }
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("get Z");

                try
                {
                    GameObject magicBall = Instantiate(MagicDict[testString], RightController.transform.position, RightController.transform.rotation, RightController.transform);
                    RightController.GetComponent<VRRightHand>().magicName = testString;
                    RightController.GetComponent<VRRightHand>().bullet = magicBall;
                }
                catch (KeyNotFoundException e)
                {

                }
            }
        }
    }

    public string keywordExtraction(string text)
    {
        int minIndex =text.Length;
        string magicName = "";
        foreach(string s in MagicStr)
        {
            int i = text.IndexOf(s);
            if (i < minIndex && i != -1)
            {
                minIndex = i;
                magicName = s;
            }
        }
        
        if (string.IsNullOrEmpty(magicName))
            return text;
        else
        {
            magicInstantiate(magicName);
            return "";
        }
    }
}
