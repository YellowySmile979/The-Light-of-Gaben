using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    int colourChangeCounter = 0;
    SpriteRenderer sr;

    [Header("Colour Defs")]
    public Vector4 white, red, blue, green;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        white = new Color(1, 1, 1, 1);
        red = new Color(1, 0, 0, 1);
        green = new Color(0, 1, 0, 1);
        blue = new Color(0, 0, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
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
                case 4:
                    colourChangeCounter = 0;
                    break;
            }
        }
    }
}
