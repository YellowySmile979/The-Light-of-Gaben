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
        if (hasWaited)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0) SceneManager.LoadScene("Start");
        }
    }
    public void QuitToMainMenu()
    {
        audioSource.PlayOneShot(LevelManager.Instance.confirmSFX, 0.5f);
        hasWaited = true;       
    }
    // Update is called once per frame

    public void Pause()
    {
        audioSource.PlayOneShot(LevelManager.Instance.confirmSFX, 0.5f);
        pauseScreenOverlay.SetActive(true);
    }

    public void Unpause()
    {
        audioSource.PlayOneShot(LevelManager.Instance.confirmSFX, 0.5f);
        pauseScreenOverlay.SetActive(false);
    }
}
