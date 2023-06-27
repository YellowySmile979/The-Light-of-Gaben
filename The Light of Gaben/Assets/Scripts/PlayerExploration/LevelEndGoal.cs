using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndGoal : MonoBehaviour
{
    public static LevelEndGoal Instance;
    
    void Awake()
    {
        Instance = this;
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
            SceneManager.LoadScene(LoadSceneManager.Instance.sceneNames[randomNumber]);
        }
    }
}
