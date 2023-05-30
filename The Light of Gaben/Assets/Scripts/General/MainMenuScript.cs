using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject creditScreen;
    public string lvlToStart;
    public AudioClip confirmSFX, hoverSFX, startSFX;
    public AudioSource audioSource;

    //detects if the player is hovering over the button and plays the hoverSFX
    public void DetectHover()
    {
        audioSource.PlayOneShot(hoverSFX);
    }
    //starts the game
    public void StartLevel()
    {
        audioSource.PlayOneShot(startSFX);
        SceneManager.LoadScene(lvlToStart);
    }
    //opens the credits overlay
    public void Credits()
    {
        audioSource.PlayOneShot(confirmSFX);
        creditScreen.SetActive(true);
    }
    //quits the game
    public void Quit()
    {
        audioSource.PlayOneShot(confirmSFX);
        Application.Quit();
    }

    public void ReturnFromCredits()
    {
        audioSource.PlayOneShot(confirmSFX);
        creditScreen.SetActive(false);
    }

}
