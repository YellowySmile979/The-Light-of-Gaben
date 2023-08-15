using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndGoal : MonoBehaviour
{
    public static LevelEndGoal Instance;
    public int floorNumber;
    int randomNo, count;
    bool isInGoal;
    
    void Awake()
    {
        Instance = this;
        isInGoal = false;
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
            isInGoal = true;
            Win();

            if (SceneManager.GetActiveScene().name == "Tutorial") { PlayerPrefs.SetInt("FirstLoad", 1); }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        isInGoal = false;
    }
    //if player has met the win conditions, player can win
    public void Win(int randomNumber = 0)
    {
        print("Win is firing");
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
        if (LoadSceneManager.Instance.canWin && isInGoal)
        {
            //player can win
            if(PlayerPrefs.GetInt("Floor Number") <= 2) //Original 5
            {
                SceneManager.LoadScene(LoadSceneManager.Instance.easySceneNames[randomNo]);
            }
            else if(PlayerPrefs.GetInt("Floor Number") > 2 && PlayerPrefs.GetInt("Floor Number") <= 5)
            {
                SceneManager.LoadScene(LoadSceneManager.Instance.mediumSceneNames[randomNo]);
            }
            else if(PlayerPrefs.GetInt("Floor Number") > 5 && PlayerPrefs.GetInt("Floor Number") <= 6)
            {                
                SceneManager.LoadScene(LoadSceneManager.Instance.hardSceneNames[randomNo]);
            }
            else if(PlayerPrefs.GetInt("Floor Number") > 6)
            {
                SceneManager.LoadScene("Hard16");
            }
            if(PlayerPrefs.GetInt("Floor Number") <= 7)
            {
                floorNumber++;
                PlayerPrefs.SetInt("Floor Number", floorNumber);
            }
        }
    }
}
