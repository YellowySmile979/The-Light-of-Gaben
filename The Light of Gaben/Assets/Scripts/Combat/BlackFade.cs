using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFade : MonoBehaviour
{
    public float duration = 1f;

    private void Start()
    {
        FadeIn();
    }
    public void FadeOut()
    {
        print("FadeOut");
        Image i = GetComponent<Image>();
        i.CrossFadeAlpha(1, duration, true);
    }

    public void FadeIn()
    {
        Image i = GetComponent<Image>();
        i.CrossFadeAlpha(0, duration, true);
    }
}
