using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Win : MonoBehaviour
{
    public string mainMenu;
    public Text finalLevelText, finalLSText, finalPPText, finalSmoloursCount, finalTotalDMG;
    public GameObject endAnimation;

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
    void Update()
    {
        // Check for animation to end.
        // i dont know how this works. im a programmer - noelle
        endAnimation.GetComponent<VideoPlayer>().loopPointReached += CheckOver;

        // Extra check to skip the video if player clicks on the screen while video is playing
        if (endAnimation.GetComponent<VideoPlayer>().isPlaying && Input.GetMouseButtonDown(0)) { CheckOver(endAnimation.GetComponent<VideoPlayer>()); }
    }

    // Happens when gacha pull video is played / skipped.
    public void CheckOver(VideoPlayer vp)
    {
        endAnimation.SetActive(false);
    }
}
