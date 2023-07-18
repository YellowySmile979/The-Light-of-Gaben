using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmolourGalleryController : MonoBehaviour
{
    //Controller for indivicual Smolours that are instantiated for Gallery.
    // I'm too lazy to imput them manually - noelle

    public Image displayedSprite;
    public Text description;
    public SmoloursData smoloursData;
    public bool collected;

    void Update()
    {
        if (collected)
        {
            displayedSprite.sprite = smoloursData.known;
            description.text = smoloursData.description;
        }
    }

}
