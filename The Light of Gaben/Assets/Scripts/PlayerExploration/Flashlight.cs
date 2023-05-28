using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    bool onOrOff = false;
    LightController lightController;

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
        }
        else
        {
            flashlight.SetActive(false);
        }
    }
    //switches the colour of the light by calling the colour switcher func
    public void CallColourSwitcher()
    {
        lightController.ColourSwitcher();
    }
}
