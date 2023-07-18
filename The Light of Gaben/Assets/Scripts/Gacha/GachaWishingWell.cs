using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaWishingWell : MonoBehaviour
{
    public PlayerSmolourController smolourController;
    bool hasCollided;

    private void Start()
    {
        smolourController = FindObjectOfType<PlayerSmolourController>();
    }

    //when player collides, open the gacha screen
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() && !hasCollided)
        {
            Time.timeScale = 0;
            smolourController.OpenGacha();
            hasCollided = true;
        }
    }

    public void Close()
    {
        Time.timeScale = 1;
        smolourController.CloseGacha();
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
