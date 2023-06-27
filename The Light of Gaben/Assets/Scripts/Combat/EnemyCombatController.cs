using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombatController : UnitStats
{
    UnitStats player;
    UnitStats healTarget;
    //public GameObject healthBar;
    public Image healthBar;
    public float xpToGive = 10f;
    public int lowestProbabilityInt, highestProbabilityInt;

    void Start()
    {
        healTarget = this;
        if (lowestProbabilityInt == 0)
        {
            lowestProbabilityInt = 1;
        }
        if (highestProbabilityInt == 0)
        {
            highestProbabilityInt = 100;
        }
        RandomColour();
    }
    //randomises which colour the enemy spawns as
    void RandomColour()
    {
        var randomColour = Random.Range(1, 5);
        switch (randomColour)
        {
            case 1:
                lightType = LightTypes.White;
                GetComponent<Image>().color = Color.white;
                break;
            case 2:
                lightType = LightTypes.Red;
                GetComponent<Image>().color = Color.red;
                break;
            case 3:
                lightType = LightTypes.Blue;
                GetComponent<Image>().color = Color.blue;
                break;
            case 4:
                lightType = LightTypes.Yellow;
                GetComponent<Image>().color = Color.yellow;
                break;
            default:
                lightType = LightTypes.White;
                break;
        }
    }
    private void Update()
    {
        healthBar.fillAmount = ((health/maxHealth) * 1);
    }
    //performs the enemy's attack
    public void Attack()
    {
        print("Enemy Attack");
        //sets the player and stateController
        player = FindObjectOfType<PlayerCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        //makes player takedamage
        player.TakeDamage(attack, this, player);
        //tells player what happened
        stateController.actionDesc = stateController.actionDesc + " Enemy attacks the player for 10 damage!";
        StartCoroutine(WaitUnitStatsVer());
    }
    //enemy will do nothing
    public void DoNothing()
    {
        print("Enemy Does Nothing");
        //everything here does like Attack(), except the part on attacking
        player = FindObjectOfType<PlayerCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        stateController.actionDesc = stateController.actionDesc + " Enemy does absolutely nothing!";
        StartCoroutine(WaitUnitStatsVer());
    }
    public void HealSelf()
    {
        print("Enemy heals self");
        int heal = Random.Range(1, 20) + (int)attack;
        //everything here does like Attack(), except heals self
        stateController = FindObjectOfType<CombatStateController>();
        healTarget.health += heal;
        stateController.actionDesc = "Enemy heals itself for " + heal + " health!";
        StartCoroutine(WaitUnitStatsVer());
    }
    //scale xp given to the level of the enemy
    public void ScaleXPWithLevel()
    {
        switch (level)
        {
            default:
                return;
            case 1:
                xpToGive = 50;
                break;
            case 2:
                xpToGive = 60;
                break;
            case 3:
                xpToGive = 70;
                break;
            case 4:
                xpToGive = 80;
                break;
            case 5:
                xpToGive = 90;
                break;
        }
        PlayerCombatController.Instance.UpdatePlayerLevel(xpToGive);
    }
}