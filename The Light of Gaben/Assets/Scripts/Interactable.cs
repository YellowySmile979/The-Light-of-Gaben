using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    SpriteRenderer sr;
    LightController lightController;
    public Color originalColour;
    public int colourNumber;

    void Awake()
    {
        lightController = FindObjectOfType<LightController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //checks to see if the flashlight passes over the object, and the colour of the light is the same as the intended
        //object
        if(collision.gameObject.tag == "Light Source" && lightController.colourChangeCounter == colourNumber)
        {
            sr.color = originalColour;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        //checks to see if the flashlight is no longer covering the object
        if (collision.gameObject.tag == "Light Source")
        {
            sr.color = Color.black;
        }
    }
}
