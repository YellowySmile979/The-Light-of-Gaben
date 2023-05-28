using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject creditScreen;
    public string lvlToStart;
    public void StartLevel()
    {
        SceneManager.LoadScene(lvlToStart);
    }

    public void Credits()
    {
        creditScreen.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ReturnFromCredits()
    {
        creditScreen.SetActive(false);
    }

}
