using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralCanvasStuff : MonoBehaviour
{
    public Text levelText, floorText, winConditionText;

    public static GeneralCanvasStuff Instance;

    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        UpdateFloorNoText();
    }
    //updates the level text to display the player's level
    public void UpdateLevelText(float level)
    {
        //updates the playerprefs so that we can store info
        PlayerPrefs.SetInt("Player Level", (int)level);
        levelText.text = "Level: " + PlayerPrefs.GetInt("Player Level");
    }
    //udpates the floor text to display which floor their on
    public void UpdateFloorNoText()
    {
        //updates the playerprefs so that we can store info
        floorText.text = "Floor: " + PlayerPrefs.GetInt("Floor Number");
    }
    //updates the win condition text to display which win condition the player must make
    public void UpdateWinConditionText(string winCondition)
    {
        winConditionText.text = "Win Condition: " + winCondition;
    }
}
