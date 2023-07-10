using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Health Bar")]
    public Sprite fullHP;
    public Sprite threeQuartersHP, halfHP, quarterHP, almostZeroHP;
    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    public Image image;
    public static HealthBar Instance;

    void Awake()
    {
        Instance = this;
        //if we havent given the image component a reference, check for it
        if(image == null) image = GetComponentInChildren<Image>();
    }
    void Update()
    {
        UpdateHealth();
    }
    //updates the health UI according to the remaining battlehealth
    public void UpdateHealth()
    {
        PlayerPrefs.GetFloat("Current Health", currentHealth);
        if (currentHealth == maxHealth)
        {
            image.sprite = fullHP;
        }
        else if(currentHealth >= maxHealth * 0.75f)
        {
            image.sprite = threeQuartersHP;
        }
        else if (currentHealth >= maxHealth * 0.5f)
        {
            image.sprite = halfHP;
        }
        else if (currentHealth >= maxHealth * 0.25f)
        {
            image.sprite = quarterHP;
        }
        else if(currentHealth >= 0)
        {
            image.sprite = almostZeroHP;
        }
    }
}
