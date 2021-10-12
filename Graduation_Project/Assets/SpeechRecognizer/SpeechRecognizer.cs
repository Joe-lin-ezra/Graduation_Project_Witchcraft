using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;

public class SpeechRecognizer : MonoBehaviour
{
    DictationRecognizer dictationRecognizer;
    public GameObject magicController;

    public void startListening()
    {
        Debug.Log("Speech recognizer starts.");
        dictationRecognizer.Start();
    }
    public void stopListening()
    {
        Debug.Log("Speech recognizer stops.");
        dictationRecognizer.Stop();
    }

    // Use this for initialization
    void Start()
    {
        dictationRecognizer = new DictationRecognizer();

        dictationRecognizer.DictationResult += onDictationResult;
        dictationRecognizer.DictationHypothesis += onDictationHypothesis;
        dictationRecognizer.DictationComplete += onDictationComplete;
        dictationRecognizer.DictationError += onDictationError;

        magicController = GameObject.Find("/Player/MagicController");
    }

    void onDictationResult(string text, ConfidenceLevel confidence)
    {
        // write your logic here
        Debug.LogFormat("Dictation result: " + text);
        magicController.GetComponent<MagicControl>().keywordExtractionAndInstantiate(text);
        stopListening();
    }

    void onDictationHypothesis(string text)
    {
        // write your logic here
        Debug.LogFormat("Dictation hypothesis: {0}", text);
        text = magicController.GetComponent<MagicControl>().keywordExtractionAndInstantiate(text);
    }

    void onDictationComplete(DictationCompletionCause cause)
    {
        // write your logic here
        if (cause != DictationCompletionCause.Complete)
            Debug.LogWarningFormat("Dictation completed unsuccessfully: {0}.", cause);
    }

    void onDictationError(string error, int hresult)
    {
        // write your logic here
        Debug.LogWarningFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        dictationRecognizer.Start();
    }
}