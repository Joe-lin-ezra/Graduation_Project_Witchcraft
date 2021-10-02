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
    [Header("Speech Recognizer")]
    public GameObject SpeechRecognizer;
    public string testString;

    public bool debug = false;

    public void Start()
    {
        MagicDict = new Dictionary<string, GameObject>();
        for (int i = 0; i< MagicsOBJ.Length; i++)
        {
            MagicDict.Add(MagicStr[i], MagicsOBJ[i]);
        }
    }

    void Update()
    {

        if (!debug)
        {
            string magicName = keywordExtraction(SpeechRecognizer.GetComponent<SpeechRecognizer>().getText());
            if (string.IsNullOrEmpty(magicName))
            {
                Debug.LogWarning("Magic name not found!!");
                return;
            }
            magicName = magicName.ToLower();

            if (string.IsNullOrEmpty(RightController.GetComponent<VRRightHand>().something))
            {
                RightController.GetComponent<VRRightHand>().bullet = Instantiate(MagicDict[magicName], RightController.transform.position, RightController.transform.rotation, RightController.transform);
                RightController.GetComponent<VRRightHand>().something = magicName;
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
                    RightController.GetComponent<VRRightHand>().something = testString;
                    RightController.GetComponent<VRRightHand>().bullet = magicBall;
                }
                catch (KeyNotFoundException e)
                {

                }
            }
        }
    }

    private string keywordExtraction(string text)
    {
        int minIndex = 0;
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
        return magicName;
    }
}
