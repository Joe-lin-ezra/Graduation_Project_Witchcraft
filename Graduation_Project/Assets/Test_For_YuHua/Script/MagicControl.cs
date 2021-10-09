using UnityEngine;
using UnityEngine.UI;


public class MagicControl : MonoBehaviour {
    [Header("Magic Component")]
    public GameObject[] MagicOBJ;
    public string[] MagicStr;

    public Object streamReconizer;


    private void Awake() {
        
    }

    public void SetText(string transcription) {
        string[] words = transcription.Split(' ');
        string lastWord = words[words.Length - 1];

    }
}
