using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public JoystickMovement joystickMovement;
    public float playerSpeed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //determines if the player should move or not
        if (joystickMovement.joystickVec.y != 0)
        {
            //moves the player in the direction of the joystick movement
            rb.velocity = new Vector2(joystickMovement.joystickVec.x * playerSpeed,
                joystickMovement.joystickVec.y * playerSpeed);
        }
        else
        {
            //simply stops the player
            rb.velocity = Vector2.zero;
        }
    }
}
