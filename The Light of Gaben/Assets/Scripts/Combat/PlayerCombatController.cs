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
    void Start()
    {
        HealthBar.Instance.maxHealth = this.maxHealth;
        HealthBar.Instance.currentHealth = this.health;
    }
    public void SelectTarget()
    {
        //Temp
        attackTarget = FindObjectOfType<EnemyCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        healTarget = this;
    }
    public void Attack()
    {
        SelectTarget();
        HealthBar.Instance.currentHealth = this.health;
        int damage = attack;
        attackTarget.TakeDamage(damage, this, attackTarget);
        StartCoroutine(WaitUnitStatsVer());
    }

    public void Heal()
    {
        SelectTarget();
        int heal = Random.Range(1, 20) + attack;
        healTarget.HealDamage(heal);
        stateController.actionDesc = "Player heals themself for " + heal + " health!";
        HealthBar.Instance.currentHealth = this.health;
        StartCoroutine(WaitUnitStatsVer());
    }

    public void LightChangerBlue()
    {
        lightType = LightTypes.Blue;
        stateController.actionDesc = "Player changes their light to Blue!";
    }

    public void LightChangerRed()
    {
        lightType = LightTypes.Red;
        stateController.actionDesc = "Player changes their light to Red!";
    }

    public void LightChangerYellow()
    {
        lightType = LightTypes.Yellow;
        stateController.actionDesc = "Player changes their light to Yellow!";
    }

    public void LightChangerWhite()
    {
        lightType = LightTypes.White;
        stateController.actionDesc = "Player changes their light to White!";
    }
}
