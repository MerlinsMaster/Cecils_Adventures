using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene_01 : MonoBehaviour
{
    
    private int index;
    public float typingSpeed;
    //public float delayBetweenLines;
    public float duration;

    public GameObject[] bubbles;
    public Text[] textObjects;
    public string[] lines;

    public GameObject typingSoundPrefab;
    public GameObject windowsSoundPrefab;

    private void Start()
    {
        foreach (GameObject bubble in bubbles)
        {
            bubble.SetActive(false);
        }
    }

    public void PlayLine(string line, Text textDisplay)                           // THIS PLAYS A LINE OF DIALOGUE FROM CECIL'S COMPUTER
    {
        textDisplay.text = string.Empty;
        StartCoroutine(TypeCo(line, textDisplay));
    }

    private IEnumerator TypeCo(string sentence, Text textDisplay)                 // THIS TYPES OUT THE LINE OF DIALOGUE
    {
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void DisplayDialogueBubble(string line, Text textDisplay, GameObject bubble)              // THIS DISPLAY'S SOMETHING THAT CECIL IS SAYING OUT LOUD
    {
        DisplayBubbleCo(line, textDisplay, bubble);
    }

    public IEnumerator DisplayBubbleCo(string line, Text textDisplay, GameObject bubble)
    {
        bubble.SetActive(true);

        yield return new WaitForSeconds(duration);

        bubble.SetActive(false);
    }

    public void PlayTypingSound()
    {
        Instantiate(typingSoundPrefab, transform.position, transform.rotation);
    }

    public void PlayWindowsSound()
    {
        Instantiate(windowsSoundPrefab, transform.position, transform.rotation);
    }

    public void Line_00()
    {
        PlayLine(lines[0], textObjects[0]);
    }

    public void Line_01()
    {
        PlayLine(lines[1], textObjects[1]);
    }

    public void Line_02()
    {
        PlayLine(lines[2], textObjects[2]);
    }

    public void Line_03()
    {
        PlayLine(lines[3], textObjects[3]);
    }

    public void Line_04()
    {
        PlayLine(lines[4], textObjects[4]);
    }

    public void Line_05()
    {
        PlayLine(lines[5], textObjects[5]);
    }

    public void Line_06()
    {
        PlayLine(lines[6], textObjects[6]);
    }

    public void Line_07()
    {
        PlayLine(lines[7], textObjects[7]);
    }

    public void Line_08()
    {
        PlayLine(lines[8], textObjects[8]);
    }

    public void Line_09()
    {
        PlayLine(lines[9], textObjects[9]);
    }

    public void Line_10()
    {
        PlayLine(lines[10], textObjects[10]);
    }

    public void Line_11()
    {
        PlayLine(lines[11], textObjects[11]);
    }

    public void Line_12()
    {
        PlayLine(lines[12], textObjects[12]);
    }

    public void Line_13()
    {
        PlayLine(lines[13], textObjects[13]);
    }

    public void Line_14()
    {
        PlayLine(lines[14], textObjects[14]);
    }

    public void Line_15()
    {
        PlayLine(lines[15], textObjects[15]);
    }

    public void Line_16()
    {
        PlayLine(lines[16], textObjects[16]);
    }

    public void Line_17()
    {
        PlayLine(lines[17], textObjects[17]);
    }

    public void Line_18()
    {
        PlayLine(lines[18], textObjects[18]);
    }

    public void Line_19()
    {
        PlayLine(lines[19], textObjects[19]);
    }

    public void Line_20()
    {
        PlayLine(lines[20], textObjects[20]);
    }

    public void Line_21()
    {
        PlayLine(lines[21], textObjects[21]);
    }

    public void Line_22()
    {
        PlayLine(lines[22], textObjects[22]);
    }

    public void Line_23()
    {
        PlayLine(lines[23], textObjects[23]);
    }

    public void Line_24()
    {
        PlayLine(lines[24], textObjects[24]);
    }

    public void Line_25()
    {
        PlayLine(lines[25], textObjects[25]);
    }

    public void Line_26()
    {
        PlayLine(lines[26], textObjects[26]);
    }

    public void Line_27()
    {
        PlayLine(lines[27], textObjects[27]);
    }

    
}
