using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsController : MonoBehaviour
{
    public List<string> InstructionsText;
    public Text text;
    public int instructionsCount = 0;
    public InstructionsController Instance;

    private void Start()
    {
        if (Instance == null) { Instance = this; }
        // starts out the instructions text with the 1st text
        text.text = InstructionsText[0];
    }

    public void NextInstruction()
    {
        //advances instrcutions count
        Instance.instructionsCount += 1;
        if (Instance.instructionsCount >= Instance.InstructionsText.Count)
        {
            Instance.Close();
        }
        text.text = InstructionsText[Instance.instructionsCount];
    
    }

    void Close()
    {
        Instance.gameObject.SetActive(false);
    }

}
