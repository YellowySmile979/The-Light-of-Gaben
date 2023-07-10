using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaWishingWell : MonoBehaviour
{
    public GameObject gachaScreen;
    bool hasCollided;

    //when player collides, open the gacha screen
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() && !hasCollided)
        {
            Time.timeScale = 0;
            gachaScreen.SetActive(true);
            hasCollided = true;
        }
    }

    public void Close()
    {
        Time.timeScale = 1;
        gachaScreen.SetActive(false);
        hasCollided = false;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            hasCollided = false;
        }
    }
}
