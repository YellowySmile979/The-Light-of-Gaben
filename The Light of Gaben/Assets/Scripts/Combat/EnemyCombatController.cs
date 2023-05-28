using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : UnitStats
{
    UnitStats player;

    private void Update()
    {
        
    }
    public void Attack()
    {
        print("Enemy Attack");
        player = FindObjectOfType<PlayerCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        player.TakeDamage(attack, this, player);
        stateController.actionDesc = stateController.actionDesc + " Enemy attacks the player for 10 damage!";
        StartCoroutine(WaitUnitStatsVer());
    }
}