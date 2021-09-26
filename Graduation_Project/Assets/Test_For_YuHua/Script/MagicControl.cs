using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicControl : MonoBehaviour
{
    // Start is called before the first frame update
    public  GameObject[] MagicsOBJ ;
    public  string[] MagicStr;
    private Dictionary<string, GameObject> MagicDict;

    [Header("VR Controller")]
    public GameObject RightController;
    [Header("Google Speech Recognizer")]
    public GameObject GoogleSpeechRecognizer;


    public void Start()
    {
        MagicDict = new Dictionary<string, GameObject>();
        for (int i = 0; i<2; i++)
        {
            MagicDict.Add(MagicStr[i], MagicsOBJ[i]);
        }
    }

    public void MagicInstantiate()
    {
        //string[] words = GoogleSpeechRecognizer.GetComponent<StreamingReconizer>().pubStr;
        //string lastWord = words[words.Length - 1].ToLower();
        //Debug.Log("text get");
        //Instantiate(MagicDict[lastWord], this.transform.position, this.transform.rotation);
    }
}
