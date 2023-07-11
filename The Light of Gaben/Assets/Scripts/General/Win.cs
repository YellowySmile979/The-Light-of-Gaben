using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public string mainMenu;
    public Text finalLevelText, finalLSText, finalPPText, finalSmoloursCount, finalTotalDMG;

    // Start is called before the first frame update
    void Start()
    {
        ShowWinText();
    }
    //shows that the player has won
    public void ShowWinText()
    {
        //updates all the texts of the player's final stats
        finalLevelText.text = "Level: " + PlayerPrefs.GetInt("Player Level");
        finalLSText.text = "LS Count: " + PlayerPrefs.GetInt("LS Count");
        finalPPText.text = "PP Count: " + PlayerPrefs.GetInt("PP Count");
        finalSmoloursCount.text = "Total Smolour Count:" + PlayerPrefs.GetInt("Smolours Collected");
        finalTotalDMG.text = "Total Damage Dealt: " + PlayerPrefs.GetInt("Total Attack");
    }
    //returns back to main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
