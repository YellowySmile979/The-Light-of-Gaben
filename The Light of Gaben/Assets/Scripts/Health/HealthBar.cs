using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpBar;
    public float maxHealth;
    public float currentHealth;
    Vector2 currentRectTransform;

    public static HealthBar Instance;

    void Awake()
    {
        Instance = this;
        currentRectTransform = hpBar.GetComponent<RectTransform>().anchoredPosition;
    }
    void Update()
    {
        UpdateHealth();
    }
    public void UpdateHealth()
    {
        if (currentHealth == maxHealth)
        {
            hpBar.GetComponent<RectTransform>().anchoredPosition = currentRectTransform;
        }
        else if(currentHealth >= maxHealth - 10)
        {
            hpBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(105.9f, currentRectTransform.y);
        }
        else if (currentHealth >= maxHealth - 20)
        {
            hpBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(49.1f, currentRectTransform.y);
        }
        else if (currentHealth >= maxHealth - 30)
        {
            hpBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-37.1f, currentRectTransform.y);
        }
        else if (currentHealth >= maxHealth - 40)
        {
            hpBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-96.8f, currentRectTransform.y);
        }
        else if(currentHealth >= 0)
        {
            hpBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-133f, currentRectTransform.y);
        }
    }
}
