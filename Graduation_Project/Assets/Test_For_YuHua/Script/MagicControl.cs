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
    [Header("Google Speech Recognizer")]
    public GameObject GoogleSpeechRecognizer;
    public string testString; 

    public void Start()
    {
        MagicDict = new Dictionary<string, GameObject>();
        for (int i = 0; i<8; i++)
        {
            MagicDict.Add(MagicStr[i], MagicsOBJ[i]);
        }
    }

    void Update()
    {
        //string magicName = GoogleSpeechRecognizer.GetComponent<StreamingRecognizer>().GetMagicName();
        
        /*if (magicName == null || magicName.Length <= 0)
        {
            return;
        }
        magicName = magicName.ToLower();
        if (RightController.GetComponent<VRRightHand>().something != magicName)
        {
            try
            {
                Instantiate(MagicDict[magicName], RightController.transform.position, RightController.transform.rotation, RightController.transform);
                RightController.GetComponent<VRRightHand>().something = magicName;
                RightController.GetComponent<VRRightHand>().bullet = MagicDict[magicName];
            }
            catch (KeyNotFoundException e)
            {

            }
        }*/
        if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("get f");
            
            try
            {
                Instantiate(MagicDict[testString], RightController.transform.position, RightController.transform.rotation, RightController.transform);
                RightController.GetComponent<VRRightHand>().something = testString;
                RightController.GetComponent<VRRightHand>().bullet = MagicDict[testString];
            }
            catch (KeyNotFoundException e)
            {

            }
        }
        
    }
}
