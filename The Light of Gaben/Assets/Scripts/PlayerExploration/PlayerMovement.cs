using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;

public class PlayerMovement : MonoBehaviour
{
    public JoystickMovement joystickMovement;
    public float playerSpeed;

    [Header("Animation")]
    public float angle;
    public RuntimeAnimatorController frontAnim, leftAnim, rightAnim, backAnim;
    public Animator animator;

    public static PlayerMovement Instance;
    Rigidbody2D rb;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        CalculateRotation();
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
    //calculates the angle at which the player is turning and then changes the walking animation accordingly
    void CalculateRotation()
    {
        angle = joystickMovement.separateTransform.transform.eulerAngles.z;
        if(angle >= 45 && angle < 135)
        {
            animator.runtimeAnimatorController = backAnim;
        }
        else if (angle >= 135 && angle < 225)
        {
            animator.runtimeAnimatorController = leftAnim;
        }
        else if (angle >= 225 && angle < 315)
        {
            animator.runtimeAnimatorController = frontAnim;
        }
        else if (angle >= 315 && angle < 45 || angle == 0)
        {
            animator.runtimeAnimatorController = rightAnim;
        }
    }
}
