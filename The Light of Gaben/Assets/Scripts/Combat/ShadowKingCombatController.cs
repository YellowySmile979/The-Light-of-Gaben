using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowKingCombatController : UnitStats
{
    UnitStats player;
    UnitStats healingTarget;
    //public GameObject healthBar;
    public Image healthBar;
    public float xpToGive = 10f;
    public int lowestProbabilityInt, highestProbabilityInt, turnOrder;

    [HideInInspector] public Image image;

    void Start()
    {
        maxDefence = defense;

        image = GetComponent<Image>();
        healingTarget = this;
        if (lowestProbabilityInt == 0)
        {
            lowestProbabilityInt = 1;
        }
        if (highestProbabilityInt == 0)
        {
            highestProbabilityInt = 100;
        }
        RandomColour();
        ChangeSprite();
    }
    //change sprites to the corresponding enemy
    void ChangeSprite()
    {
        image.sprite = LevelManager.Instance.spr;
    }
    //randomises which colour the enemy spawns as
    public void RandomColour()
    {
        var randomColour = Random.Range(1, 8);
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
            case 5:
                lightType = LightTypes.Magenta;
                GetComponent<Image>().color = Color.magenta;
                break;
            case 6:
                lightType = LightTypes.Green;
                GetComponent<Image>().color = Color.green;
                break;
            case 7:
                lightType = LightTypes.Orange;
                GetComponent<Image>().color = new Color(1, 0.5607843f, 0, 1);
                break;
            default:
                lightType = LightTypes.White;
                break;
        }
    }
    private void Update()
    {
        healthBar.fillAmount = ((health / maxHealth) * 1);
    }
    //performs the enemy's attack
    public void Attack()
    {
        hasFinishedTheirTurn = false;
        print("Enemy Attack");
        //sets the player and stateController
        player = FindObjectOfType<PlayerCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        //makes player takedamage
        player.TakeDamage(attack, this, player);
        stateController.camAudioSource.PlayOneShot(clawSFX);
        //tells player what happened
        //stateController.actionDesc = stateController.actionDesc + " Enemy attacks the player for 10 damage!";
        hasFinishedTheirTurn = true;
        StartCoroutine(WaitUnitStatsVer());
    }
    //enemy will do nothing
    public void DoNothing()
    {
        hasFinishedTheirTurn = false;
        print("Enemy Does Nothing");
        //everything here does like Attack(), except the part on attacking
        player = FindObjectOfType<PlayerCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        stateController.actionDesc = stateController.actionDesc + " The Shadow King does absolutely nothing!";
        hasFinishedTheirTurn = true;
        StartCoroutine(WaitUnitStatsVer());
    }
    public void HealSelf()
    {
        hasFinishedTheirTurn = false;
        print("Enemy heals self");
        int heal = Random.Range(1, 10) + ((int)attack / 2);
        //everything here does like Attack(), except heals self
        stateController = FindObjectOfType<CombatStateController>();
        if (health < maxHealth) healingTarget.health += heal;
        else healingTarget.health = maxHealth;
        stateController.camAudioSource.PlayOneShot(healSFX);
        stateController.actionDesc = stateController.actionDesc + "The Shadow King heals itself for " + heal + " health!";
        hasFinishedTheirTurn = true;
        StartCoroutine(WaitUnitStatsVer());
    }

    public void BuffSelf()
    {
        hasFinishedTheirTurn = false;
        attackBonus = attack / 2;
        stateController.camAudioSource.PlayOneShot(healSFX);
        stateController.actionDesc = stateController.actionDesc + "The Shadow King buffs its attack!";
        hasFinishedTheirTurn = true;
        StartCoroutine(WaitUnitStatsVer());
    }

    public PassiveAttacksController passive;
    public void DamageOverTime()
    {
        passive.dmgToGive = attack;
        passive.turnsToDmg = 2;
        passive.passsivelightTypes = lightType;
    }

    public void AttackHarder()
    {
        hasFinishedTheirTurn = false;
        print("Enemy Attack Harder");
        player = FindObjectOfType<PlayerCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        player.TakeDamage(attack + 5 , this, player);
        stateController.camAudioSource.PlayOneShot(clawSFX);
        hasFinishedTheirTurn = true;
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
public enum ShadowKingPhase
{
    Phase1,
    Phase2,
    Phase3,
    Win,
    Lose
}
