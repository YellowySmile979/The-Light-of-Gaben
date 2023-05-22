using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData/ScriptableEnemy")]
public class EnemyData : ScriptableObject
{
    public float acceleration = 70f;
    public float deceleration = 50f;
    public float moveSpeed = 10f;
    public float turnSpeed = 100f;
}
