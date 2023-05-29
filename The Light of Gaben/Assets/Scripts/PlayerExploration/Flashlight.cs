using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    bool onOrOff = false;
    LightController lightController;
    public AudioSource audioSource;
    public AudioClip activateSFX, switchSFX;

    // Start is called before the first frame update
    void Start()
    {
        lightController = FindObjectOfType<LightController>();
    }
    //turns on or off the flashlight, this is button inputs
    public void TurnOnOrOffLight()
    {
        onOrOff = !onOrOff;
        if(onOrOff)
        {
            flashlight.SetActive(true);
            audioSource.PlayOneShot(activateSFX);
        }
        else
        {
            flashlight.SetActive(false);
            audioSource.PlayOneShot(activateSFX);
        }
    }
    //switches the colour of the light by calling the colour switcher func
    public void CallColourSwitcher()
    {
        lightController.ColourSwitcher();
        audioSource.PlayOneShot(switchSFX);
    }
}
