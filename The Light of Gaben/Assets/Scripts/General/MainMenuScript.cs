using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuScript : MonoBehaviour
{
    public GameObject creditScreen;
    public string lvlToStart;
    public AudioClip confirmSFX, hoverSFX, startSFX;
    public AudioSource audioSource;
    public GameObject menuLoop, beginningAnim, galleryButton;
    public bool skipBeginning;

    SmolourGallery gallery;

    // Begins playing the First Load Animation if it is a fresh launch.
    // Else, skips to Menu.
    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstLoad") == 1 || skipBeginning)
        {
            PlayLoop();
            galleryButton.SetActive(true);
        }
        else
        {
            PlayBeginning();
        }

        gallery = FindObjectOfType<SmolourGallery>();
    }

    // For playing the Beginning Anim and the Menu Loop
    void PlayBeginning() { beginningAnim.SetActive(true); }
    void PlayLoop() { menuLoop.SetActive(true); }
    void Update()
    {
        // Check for animation to end.
        // i dont know how this works. im a programmer - noelle
        beginningAnim.GetComponent<VideoPlayer>().loopPointReached += CheckOver;

        // Extra check to skip the video if player clicks on the screen while video is playing
        if (beginningAnim.GetComponent<VideoPlayer>().isPlaying && Input.GetMouseButtonDown(0)) { CheckOver(beginningAnim.GetComponent<VideoPlayer>()); }
    }

    // Happens when gacha pull video is played / skipped.
    public void CheckOver(VideoPlayer vp)
    {
        beginningAnim.SetActive(false);
        PlayLoop();
    }

    //detects if the player is hovering over the button and plays the hoverSFX
    public void DetectHover()
    {
        audioSource.PlayOneShot(hoverSFX);
    }
    //starts the game
    public void StartLevel()
    {
        audioSource.PlayOneShot(startSFX);
        if (PlayerPrefs.GetInt("FirstLoad") == 1 || skipBeginning)
        {
            int randomNo = Random.Range(1, 5);
            SceneManager.LoadScene(LoadSceneManager.Instance.easySceneNames[randomNo]); 
        }
        else { SceneManager.LoadScene(lvlToStart); }
        // resets all player prefs for a new game
        PlayerPrefs.SetInt("Player Level", 1);
        PlayerPrefs.SetInt("LS Count", 0);
        PlayerPrefs.SetInt("PP Count", 0);
        PlayerPrefs.SetInt("Total Attack", 0);
        PlayerPrefs.SetInt("Smolours Collected", 0);
        gallery.collectedSmolours.Clear();
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
