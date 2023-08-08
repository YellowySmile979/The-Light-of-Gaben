using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndGoal : MonoBehaviour
{
    public static LevelEndGoal Instance;
    public int floorNumber;
    int randomNo, count;
    
    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        count++;
        if(count >= 100)
        {
            int floorNumber = PlayerPrefs.GetInt("Floor Number");
            print("Floor Number " + floorNumber);
            count = 0;
        }
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
        //gets the corresponding random number depending on the floor number
        if (PlayerPrefs.GetInt("Floor Number") <= 5)
        {
            randomNo = LoadSceneManager.Instance.easyRandomNumber;
        }
        else if (PlayerPrefs.GetInt("Floor Number") > 5 && PlayerPrefs.GetInt("Floor Number") <= 10)
        {
            randomNo = LoadSceneManager.Instance.mediumRandomNumber;
        }
        else if (PlayerPrefs.GetInt("Floor Number") > 10 && PlayerPrefs.GetInt("Floor Number") <= 15)
        {
            randomNo = LoadSceneManager.Instance.hardRandomNumber;
        }
        if (LoadSceneManager.Instance.canWin)
        {
            //player can win
            if(PlayerPrefs.GetInt("Floor Number") <= 5)
            {
                SceneManager.LoadScene(LoadSceneManager.Instance.easySceneNames[randomNo]);
            }
            else if(PlayerPrefs.GetInt("Floor Number") > 5 && PlayerPrefs.GetInt("Floor Number") <= 10)
            {
                SceneManager.LoadScene(LoadSceneManager.Instance.mediumSceneNames[randomNo]);
            }
            else if(PlayerPrefs.GetInt("Floor Number") > 10 && PlayerPrefs.GetInt("Floor Number") <= 15)
            {                
                SceneManager.LoadScene(LoadSceneManager.Instance.hardSceneNames[randomNo]);
            }
            else if(PlayerPrefs.GetInt("Floor Number") > 15)
            {
                SceneManager.LoadScene("Hard16");
            }
            if(PlayerPrefs.GetInt("Floor Number") <= 15)
            {
                floorNumber++;
                PlayerPrefs.SetInt("Floor Number", floorNumber);
            }
        }
    }
}
