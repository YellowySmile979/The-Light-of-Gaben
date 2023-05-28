using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject creditScreen;
    public string lvlToStart;
    public AudioClip confirmSFX, hoverSFX;
    public AudioSource audioSource;

    public void DetectHover()
    {
        audioSource.PlayOneShot(hoverSFX);
    }
    public void StartLevel()
    {
        audioSource.PlayOneShot(confirmSFX);
        SceneManager.LoadScene(lvlToStart);
    }

    public void Credits()
    {
        audioSource.PlayOneShot(confirmSFX);
        creditScreen.SetActive(true);
    }

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
