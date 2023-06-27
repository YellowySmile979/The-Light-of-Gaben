using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralCanvasStuff : MonoBehaviour
{
    [Header("HUD")]
    public Text levelText, floorText, winConditionText;
    [Header("Game Over Screen")]
    public GameObject gameOverScreen;
    public static bool fade = false;
    public Text finalLevelText, finalPPText, finalLSText, finalTotalDMG, finalSmouloursCount;    
    public Image backgroundGameOverScreen;

    public static GeneralCanvasStuff Instance;

    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        UpdateFloorNoText();
        //handles fade in
        if (fade)
        {
            //sets the var colour to bg colour
            var colour = backgroundGameOverScreen.color;
            //changes alpha overtime
            colour.a += Time.unscaledDeltaTime;
            //clamps alpha to 1
            if (colour.a > 1) colour.a = 1;
            //sets the bg colour to colour
            backgroundGameOverScreen.color = colour;
            //backgroundGameOverScreen.CrossFadeAlpha(1, crossFadeInTime, true);
        }
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
    //handles the entire game over section of the game
    public void GameOver()
    {
        Time.timeScale = 0;
        //activates the gameoverscreen
        gameOverScreen.SetActive(true);
        //updates all the texts of the player's final stats
        finalLevelText.text = "Level: " + PlayerPrefs.GetInt("Player Level");
        finalLSText.text = "LS Count: " + PlayerPrefs.GetInt("LS Count");
        finalPPText.text = "PP Count: " + PlayerPrefs.GetInt("PP Count");
        //fades in the BG
        fade = true;
    }
    //returns back to main menu
    public void BackToMainMenu()
    {
        //SceneManager.LoadScene();
    }
}
