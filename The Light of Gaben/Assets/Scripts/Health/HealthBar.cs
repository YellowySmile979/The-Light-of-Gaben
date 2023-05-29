using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpBar;
    public float currentHealth;
    RectTransform currentRectTransform;

    public static HealthBar Instance;

    void Awake()
    {
        Instance = this;
        currentRectTransform = hpBar.GetComponent<RectTransform>();
    }
    public void UpdateHealth()
    {

    }
}
