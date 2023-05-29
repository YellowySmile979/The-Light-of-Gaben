using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : UnitStats
{
    UnitStats player;
    public GameObject healthBar;

    private void Update()
    {
        healthBar.transform.localScale = new Vector3((health/maxHealth) * 2f, -0.1f, 1);
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