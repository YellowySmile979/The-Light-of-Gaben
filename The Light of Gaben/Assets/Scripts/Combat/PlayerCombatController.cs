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
        //sets the max health of the exploration health bar to the combat max health
        HealthBar.Instance.maxHealth = this.maxHealth;
        //checks if the current health of the exploration hp bar is the same as the combat hp bar,
        //or if the exploration hp bar is <=0
        //otherwise sets the combat hp to exploration hp to perceive hp being carried over
        if (HealthBar.Instance.currentHealth == this.health || HealthBar.Instance.currentHealth <= 0)
        {
            HealthBar.Instance.currentHealth = this.health;
        }
        else
        {
            this.health = HealthBar.Instance.currentHealth;
        }
    }
    //selects the target to attack
    public void SelectTarget()
    {
        //Temp
        attackTarget = FindObjectOfType<EnemyCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        healTarget = this;
    }
    //performs the player's attack
    public void Attack()
    {
        SelectTarget();
        HealthBar.Instance.currentHealth = this.health;
        int damage = attack;
        attackTarget.TakeDamage(damage, this, attackTarget);
        StartCoroutine(WaitUnitStatsVer());
    }
    //performs the player's heal
    public void Heal()
    {
        SelectTarget();
        int heal = Random.Range(1, 20) + attack;
        healTarget.HealDamage(heal);
        stateController.actionDesc = "Player heals themself for " + heal + " health!";
        HealthBar.Instance.currentHealth = this.health;
        StartCoroutine(WaitUnitStatsVer());
    }
    //changes the colour of the attack to be blue
    public void LightChangerBlue()
    {
        lightType = LightTypes.Blue;
        stateController.actionDesc = "Player changes their light to Blue!";
    }
    //changes the colour of the attack to be red
    public void LightChangerRed()
    {
        lightType = LightTypes.Red;
        stateController.actionDesc = "Player changes their light to Red!";
    }
    //changes the colour of the attack to be yellow
    public void LightChangerYellow()
    {
        lightType = LightTypes.Yellow;
        stateController.actionDesc = "Player changes their light to Yellow!";
    }
    //changes the colour of the attack to be white
    public void LightChangerWhite()
    {
        lightType = LightTypes.White;
        stateController.actionDesc = "Player changes their light to White!";
    }
}
