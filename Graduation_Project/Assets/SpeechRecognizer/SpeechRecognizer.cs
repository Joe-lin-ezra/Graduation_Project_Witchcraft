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

    // initialization
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