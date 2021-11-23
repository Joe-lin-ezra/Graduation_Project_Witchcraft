using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class SpeechRecognizer : MonoBehaviour
{
    DictationRecognizer dictationRecognizer;
    public GameObject magicController;

    //this is not good by i lazy to think:)
    public GameObject microhoneUI;

    public void startListening()
    {
        Debug.Log("Speech recognizer starts.");
        microhoneUI.SetActive(true);
        dictationRecognizer.Start();
        
    }
    public void stopListening()
    {
        Debug.Log("Speech recognizer stops.");
        microhoneUI.SetActive(false);
        dictationRecognizer.Stop();
    }

    // initialization
    void Start()
    {
        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.DictationResult += onDictationResult;
        dictationRecognizer.DictationHypothesis += onDictationHypothesis;
        dictationRecognizer.DictationComplete += onDictationComplete;
        dictationRecognizer.DictationError += onDictationError;

        magicController = GameObject.Find("/Player/MagicController");

        //for UI
        microhoneUI.SetActive(false);
    }

    void onDictationResult(string text, ConfidenceLevel confidence)
    {
        Debug.LogFormat("Dictation result: " + text);
        text = magicController.GetComponent<MagicControl>().keywordExtractionAndInstantiate(text);
        stopListening();
    }

    void onDictationHypothesis(string text)
    {
        Debug.LogFormat("Dictation hypothesis: {0}", text);
        text = magicController.GetComponent<MagicControl>().keywordExtractionAndInstantiate(text);
    }

    void onDictationComplete(DictationCompletionCause cause)
    {
        if (cause != DictationCompletionCause.Complete)
            Debug.LogWarningFormat("Dictation completed unsuccessfully: {0}.", cause);
    }

    void onDictationError(string error, int hresult)
    {
        Debug.LogWarningFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        dictationRecognizer.Start();
    }
}