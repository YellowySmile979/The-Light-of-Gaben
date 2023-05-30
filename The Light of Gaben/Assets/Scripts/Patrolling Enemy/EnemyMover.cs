using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 movementVector;
    Rigidbody2D rb;

    [Header("Acceleration")]
    public float currentSpeed = 0f;
    public float currentForwardDirection = 1f;

    [Header("EnemyData")]
    public EnemyData enemyData;
    void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }
    //handles the enemy's movement
    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
        CalculateSpeed(movementVector);
        if (movementVector.y > 0)
        {
            currentForwardDirection = 1f;
        }
        else if (movementVector.y < 0)
        {
            currentForwardDirection = 0f;
        }
    }
    //calculates the speed at which the enemy moves
    void CalculateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.y) > 0)
        {
            currentSpeed += enemyData.acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= enemyData.deceleration * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, enemyData.moveSpeed);
    }
    void FixedUpdate()
    {
        //makes the enemy move
        rb.velocity = currentForwardDirection * currentSpeed * Time.deltaTime * (Vector2)transform.up;
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * enemyData.turnSpeed
            * Time.fixedDeltaTime));
    }
}
