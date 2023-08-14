using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsController : MonoBehaviour
{
    public List<string> InstructionsText;
    public Text text;
    int instructionsCount = 0;
    
    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstLoad") == 1) { Close(); }
        // starts out the instructions text with the 1st text
        text.text = InstructionsText[0];
    }

    public void NextInstruction()
    {
        //advances instrcutions count
        instructionsCount++;
        if (instructionsCount >= InstructionsText.Count)
        {
            Close();
        }
        text.text = InstructionsText[instructionsCount];
    }

    void Close()
    {
        gameObject.SetActive(false);
    }

}
