using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    bool offOrOn = false;
    public GameObject lightSource;
    Vector2 moveDirection;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        ToggleFlashlight();
    }
    void FixedUpdate()
    {
        Move();
    }
    //func processes what input is currently being inputed and fills it in accordingly
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY);
    }
    //determines how the player moves, in what direction and how fast
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
    void ToggleFlashlight()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            offOrOn = !offOrOn;
            if (!offOrOn)
            {
                lightSource.SetActive(false);
            }
            else
            {
                lightSource.gameObject.SetActive(true);
            }
        }
    }
}
