using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseScreenOverlay;
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("Start");
    }
    // Update is called once per frame

    public void Pause()
    {
        pauseScreenOverlay.SetActive(true);
    }

    public void Unpause()
    {
        pauseScreenOverlay.SetActive(false);
    }
}
