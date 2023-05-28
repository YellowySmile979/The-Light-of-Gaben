using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : UnitStats
{
    UnitStats player;
    public void Attack()
    {
        print("Enemy Attack");
        player = FindObjectOfType<PlayerCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        player.TakeDamage(attack);
        stateController.actionDesc = stateController.actionDesc + " Enemy attacks the player for 10 damage!";
        StartCoroutine(WaitUnitStatsVer());
    }
}