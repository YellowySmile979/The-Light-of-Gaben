using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaResultsAnimationController : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        Debug.Log("Start");
        anim = GetComponent<Animator>();
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0f);
    }

    public void Pulled()
    {
        Debug.Log("Pulled");
        anim.SetTrigger("Pulled");
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
    }
}
