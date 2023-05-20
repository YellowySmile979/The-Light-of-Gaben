using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : UnitStats
{
    PlayerCombatController player;

    void Start()
    {
        player = FindObjectOfType<PlayerCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
    }
    public void Attack()
    {
        player.TakeDamage(attack);
        stateController.actionDesc = stateController.actionDesc + " Enemy attacks the player for 10 damage!";
        StartCoroutine(Wait());
    }
}