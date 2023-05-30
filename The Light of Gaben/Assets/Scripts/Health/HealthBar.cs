using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Header("Health Bar")]
    public GameObject fullHP;
    public GameObject threeQuartersHP, halfHP, quarterHP, almostZeroHP;
    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    public static HealthBar Instance;

    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        UpdateHealth();
    }
    //updates the health according to the remaining battlehealth
    public void UpdateHealth()
    {
        if (currentHealth == maxHealth)
        {
            fullHP.SetActive(true);
            threeQuartersHP.SetActive(false);
            halfHP.SetActive(false);
            quarterHP.SetActive(false);
            almostZeroHP.SetActive(false);
        }
        else if(currentHealth >= maxHealth * 0.75f)
        {
            fullHP.SetActive(false);
            threeQuartersHP.SetActive(true);
            halfHP.SetActive(false);
            quarterHP.SetActive(false);
            almostZeroHP.SetActive(false);
        }
        else if (currentHealth >= maxHealth * 0.5f)
        {
            fullHP.SetActive(false);
            threeQuartersHP.SetActive(false);
            halfHP.SetActive(true);
            quarterHP.SetActive(false);
            almostZeroHP.SetActive(false);
        }
        else if (currentHealth >= maxHealth * 0.25f)
        {
            fullHP.SetActive(false);
            threeQuartersHP.SetActive(false);
            halfHP.SetActive(false);
            quarterHP.SetActive(true);
            almostZeroHP.SetActive(false);
        }
        else if(currentHealth >= 0)
        {
            fullHP.SetActive(false);
            threeQuartersHP.SetActive(false);
            halfHP.SetActive(false);
            quarterHP.SetActive(false);
            almostZeroHP.SetActive(true);
        }
    }
}
