using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickMovement : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    [SerializeField] float joystickRadiusLimit;
    Vector2 joystickTouchPos;
    Vector2 joystickOriginalPos;
    float joystickRadius;

    public Transform playerTransform;
    public Transform flashlightTransform;
    public Transform separateTransform;

    // Start is called before the first frame update
    void Start()
    {
        //stores the joysticks original pos so that it can return when not pressed 
        joystickOriginalPos = joystickBG.transform.position;
        //stores the BG radius
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.x / joystickRadiusLimit;
    }
    void Update()
    {
        //rotate the character towards the joystick direction
        if (joystickVec != Vector2.zero)
        {
            //calculates the angle at which the joystick is facing
            //aka where im wanting the player to move to
            float angle = Mathf.Atan2(joystickVec.y, joystickVec.x) * Mathf.Rad2Deg;
            //calculates the rotation that the player has to move to
            separateTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            flashlightTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    //when i press down, sets the joystick and it's BG to the mouseposition
    public void PointerDown()
    {
        joystick.transform.position = Input.mousePosition;
        joystickBG.transform.position = Input.mousePosition;
        //stores the pos of where pressed
        joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        //gets the pointer position and sets the variable on the left as the general event data on
        //the right which also takes the pointer data
        //as is a cast like () but not really
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        //gets the position on where ur pointer is
        Vector2 dragPos = pointerEventData.position;
        //calculates the direction from the joystick to where ur dragging
        joystickVec = (dragPos - joystickTouchPos).normalized;

        //calculates the distance between where u r clicking and where u r dragging
        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        //clamps the max dist the joystick can move
        if (joystickDist < joystickRadius)
        {
            //when drag, move it by this amount
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        }
        else
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }
    }
    //stops the joystick and resets it when i stop pressing
    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
        joystickBG.transform.position = joystickOriginalPos;
    }
}
