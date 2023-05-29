using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : UnitStats
{
    UnitStats attackTarget;
    UnitStats healTarget;
    [Header("Player Stats")]
    public float playerLevel = 1;
    public float playerXP = 0;
    [Header("Audio Clips")]
    public AudioClip buttonSFX;

    void Update()
    {
        // Controls XP levelling
        //switch (playerXP)
        //{
        //    case 10:
        //        playerLevel = 2;
        //        print("Player levelled up!");
        //        break;
        //    case 20:
        //        playerLevel = 3;
        //        print("Player levelled up!");
        //        break;
        //    case 40:
        //        playerLevel = 4;
        //        print("Player levelled up!");
        //        break;
        //    case 80:
        //        playerLevel = 5;
        //        print("Player levelled up!");
        //        break;
        //}

        //Testing Health
        //if (Input.GetMouseButtonDown(1))
        //{
        //    print("Health minused");
        //    TakeDamage(10);
        //}
        //if (Input.GetMouseButtonDown(0))
        //{
        //    print("Health addeded");
        //    HealDamage(10);
        //}
    }
    void UpdateHealthBar()
    {
        HealthBar.Instance.currentHealth = this.health;
    }
    public void SelectTarget()
    {
        //Temp, since it's just 1v1
        attackTarget = FindObjectOfType<EnemyCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        healTarget = this;
    }
    public void Attack()
    {
        stateController.camAudioSource.PlayOneShot(buttonSFX, 1f);
        SelectTarget();
        int damage = Random.Range(1, 20) + attack;
        attackTarget.TakeDamage(damage);
        stateController.actionDesc = "Player attacks " + attackTarget.name + " for " + damage + " damage!";
        UpdateHealthBar();
        StartCoroutine(WaitUnitStatsVer());
    }

    public void Heal()
    {
        stateController.camAudioSource.PlayOneShot(buttonSFX, 1f);
        SelectTarget();
        int heal = Random.Range(1, 20) + attack;
        healTarget.HealDamage(heal);
        stateController.actionDesc = "Player heals themself for " + heal + " health!";
        UpdateHealthBar();
        StartCoroutine(WaitUnitStatsVer());
    }
}
