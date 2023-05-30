using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseScreenOverlay;
    public AudioSource audioSource;
    float waitTime;
    bool hasWaited;

    void Start()
    {
        waitTime = LevelManager.Instance.confirmSFX.length;
    }
    void Update()
    {
        //checks to see if the button has been pressed
        if (hasWaited)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0) SceneManager.LoadScene("Start");
        }
    }
    //for button to quit to main menu
    public void QuitToMainMenu()
    {
        audioSource.PlayOneShot(LevelManager.Instance.confirmSFX, 0.5f);
        Time.timeScale = 1;
        hasWaited = true;       
    }
    //for pause button
    public void Pause()
    {
        audioSource.PlayOneShot(LevelManager.Instance.confirmSFX, 0.5f);
        Time.timeScale = 0;
        pauseScreenOverlay.SetActive(true);
    }
    //for unpause button
    public void Unpause()
    {
        audioSource.PlayOneShot(LevelManager.Instance.confirmSFX, 0.5f);
        Time.timeScale = 1;
        pauseScreenOverlay.SetActive(false);
    }
}
