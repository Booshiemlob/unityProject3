using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteText : MonoBehaviour
{
    //DANGER ZONE: You should not have to modify anything on this script

    [Range(0,0.1f)]
    [Header("Controls how fast the text is written to the screen")]
    public float scrollSpeed;

    [Range(0, 1)]
    [Header("Controls how long the text box waits before dissapearing")]
    public float fadeSpeed;

    public Image textBackground;
    public Text textBox;
    int characters, currentChar;
    string fullOutputText, outputText;
    bool isWriting = false;

    private void Awake()
    {
        textBackground = GetComponentInChildren<Image>();
        textBox = GetComponentInChildren<Text>();
        textBox.text = null;
    }

    /// <summary>
    /// Accepts a string, and writes it to the output log on screen.
    /// </summary>
    /// <param name="text"></param>
    public void OutputText(string text)
    {
        if (isWriting)
            return;

        ResetValues();

        textBackground.enabled = true;

        characters = text.Length;
        fullOutputText = text;
        
        InvokeRepeating("ScrollingText", 0, scrollSpeed);
    }

    /// <summary>
    /// Writes each individual character to the screen, decreasing/increasing the scrollSpeed float will make this function write faster/slower respectively.
    /// </summary>
    void ScrollingText()
    {
        outputText += fullOutputText[currentChar];
        textBox.text = outputText;
        currentChar++;

        if (currentChar >= fullOutputText.Length)
        {
            Invoke("Delay", fadeSpeed);
            CancelInvoke("ScrollingText");
        }
    }

    /// <summary>
    /// Turns off the text box & resets critical parameters
    /// </summary>
    void Delay()
    {
        ResetValues();
        isWriting = false;
        textBackground.enabled = false;
    }

    void ResetValues()
    {
        currentChar = 0;
        outputText = null;
        textBox.text = null;
    }
}
