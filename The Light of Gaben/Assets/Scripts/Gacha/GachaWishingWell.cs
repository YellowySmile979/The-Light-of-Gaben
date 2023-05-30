using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaWishingWell : MonoBehaviour
{
    public GameObject gachaScreen;

    //when player collides, open the gacha screen
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            Time.timeScale = 0;
            gachaScreen.SetActive(true);
        }
    }
}
