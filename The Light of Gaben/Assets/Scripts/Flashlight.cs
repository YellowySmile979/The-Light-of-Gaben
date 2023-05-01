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

    // Update is called once per frame
    void Update()
    {
        
    }
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
    public void CallColourSwitcher()
    {
        lightController.ColourSwitcher();
    }
}
