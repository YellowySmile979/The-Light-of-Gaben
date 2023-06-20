using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : UnitStats
{
    UnitStats attackTarget;
    UnitStats healTarget;
    [Header("Player Stats")]
    public static float playerXP = 0;

    public static PlayerCombatController Instance;
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
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
    public void UpdatePlayerLevel(float xpGiven)
    {
        playerXP += xpGiven;
        print("playerXP: " + playerXP);
        if (playerXP >= 0 && playerXP < 200)
        {
            level = 1;
        }
        else if (playerXP >= 200 && playerXP < 330)
        {
            level = 2;
        }
        else if (playerXP >= 330 && playerXP < 460)
        {
            level = 3;
        }
        else if (playerXP >= 460 && playerXP < 590)
        {
            level = 4;
        }
        else if (playerXP >= 590 && playerXP < 620)
        {
            level = 5;
        }
        else if (playerXP >= 620 && playerXP < 770)
        {
            level = 6;
        }
        else if (playerXP >= 770 && playerXP < 920)
        {
            level = 7;
        }
        else if (playerXP >= 920 && playerXP < 1100)
        {
            level = 8;
        }
        else if (playerXP >= 1100 && playerXP < 1280)
        {
            level = 9;
        }
        else if (playerXP >= 1280 && playerXP < 1500)
        {
            level = 10;
        }
        else if (playerXP >= 1500 && playerXP < 1720)
        {
            level = 11;
        }
        else if (playerXP >= 1720 && playerXP < 1940)
        {
            level = 12;
        }
        else if (playerXP >= 1940 && playerXP < 2210)
        {
            level = 13;
        }
        else if (playerXP >= 2210 && playerXP < 2480)
        {
            level = 14;
        }
        else if (playerXP >= 2480 && playerXP < 2750)
        {
            level = 15;
        }
        else if (playerXP >= 2750 && playerXP < 3050)
        {
            level = 16;
        }
        else if (playerXP >= 3050 && playerXP < 3350)
        {
            level = 17;
        }
        else if (playerXP >= 3350 && playerXP < 3950)
        {
            level = 18;
        }
        else if (playerXP >= 3950 && playerXP < 4550)
        {
            level = 19;
        }
        else if (playerXP >= 4550)
        {
            level = 20;
        }
        GeneralCanvasStuff.Instance.UpdateLevelText(level);
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
