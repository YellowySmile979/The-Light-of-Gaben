using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    int colourChangeCounter = 0;
    public SpriteRenderer sr;
    public GameObject flashlight;
    bool onOrOff = true;

    [Header("Colour Defs")]
    public Vector4 white, red, blue, green;

    // Start is called before the first frame update
    void Start()
    {
        //sets the possible colour of the flashlight
        white = new Color(1, 1, 1, 1);
        red = new Color(1, 0, 0, 1);
        green = new Color(0, 1, 0, 1);
        blue = new Color(0, 0, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //whenever i press E, change the colour of the flashlight
        if (Input.GetKeyDown(KeyCode.E) && onOrOff)
        {
            colourChangeCounter++;
            switch (colourChangeCounter)
            {
                default:
                    sr.color = white;
                    break;
                case 1:
                    sr.color = red;
                    break;
                case 2:
                    sr.color = green;
                    break;
                case 3:
                    sr.color = blue;
                    break;
            }
        }
        //resets the counter back to zero so that the colour is white
        if (colourChangeCounter > 3) colourChangeCounter = 0;
        //when i press Q, flip flop between off or on
        if(Input.GetKeyDown(KeyCode.Q))
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
    }
}
