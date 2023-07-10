using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndGoal : MonoBehaviour
{
    public static LevelEndGoal Instance;
    public int floorNumber;
    
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        PlayerPrefs.SetInt("Floor Number", floorNumber);
    }
    //detects when player enters
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            Win();
        }
    }
    //if player has met the win conditions, player can win
    public void Win(int randomNumber = 0)
    {
        if (LoadSceneManager.Instance.canWin)
        {
            //player can win
            if(PlayerPrefs.GetInt("Floor Number") <= 5)
            {
                SceneManager.LoadScene(LoadSceneManager.Instance.easySceneNames[randomNumber]);
            }
            else if(PlayerPrefs.GetInt("Floor Number") > 5 && PlayerPrefs.GetInt("Floor Number") <= 10)
            {
                SceneManager.LoadScene(LoadSceneManager.Instance.mediumSceneNames[randomNumber]);
            }
            else if(PlayerPrefs.GetInt("Floor Number") > 10 && PlayerPrefs.GetInt("Floor Number") <= 15)
            {
                SceneManager.LoadScene(LoadSceneManager.Instance.hardSceneNames[randomNumber]);
            }
        }
    }
}
