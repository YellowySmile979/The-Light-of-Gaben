using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitStats : MonoBehaviour
{

    [Header("Unit Stats")]
    public float attack = 10;
    public float defense = 0;
    public float WV = 0;
    public float speed = 10;
    public float crit = 0;
    public float nextTurnIn = 0;
    public float maxHealth = 50;
    public float health = 50;
    public float level = 1;

    [Header ("Smolour Buff Stats")]
    float redMultiplier = 1.0f, redBonus = 0.0f;
    float yellowMultiplier = 1.0f, yellowBonus = 0.0f;
    float blueMultiplier = 1.0f, blueBonus = 0.0f;
    float speedMultiplier = 1.0f, speedBonus = 0.0f;
    float attackMulitplier = 1.0f, attackBonus = 0.0f;
    float defenseMultiplier = 1.0f, defenseBonus = 0.0f;
    float shield = 0.0f;
    int critBonus = 0;

    public bool isDead = false;

    public CombatStateController stateController;

    public enum LightTypes { White, Red, Yellow, Blue };
    public LightTypes lightType = LightTypes.White;
    
    void Start()
    {
        health = maxHealth;
        attack = attack + attackBonus * attackMulitplier;
        defense = defense + defenseBonus * defenseMultiplier;
        speed = speed + speedBonus * speedMultiplier;
        crit += critBonus;
    }
    void Awake()
    {
        stateController = FindObjectOfType<CombatStateController>();
    }
    // Calculates nextTurnIn value   
    public void CalculateNextTurn(int currentTurn)
    {
        nextTurnIn = currentTurn + (Random.Range(1, 50) - speed);
    }

    // Self Actions: Methods called by other CombatControllers to affect their targetting unit
    public void TakeDamage(float dmg, UnitStats attacker, UnitStats attackee)
    {
        print("Took Damage");

        // Light Type If Else statements! There's probably a more efficient way to do this but it's 28 May and our critique is in 2 days.
        // Red > Blue > Yellow > Red
        // - noelle
        float WeakBuff = 2.5f;
        dmg = (((2 * attacker.level * (attacker.crit) / 5) + 2) * attacker.WV * (attacker.attack / attackee.defense) / 2) +2 ;
        
        // Switch Case for Light Weakness
        switch (attackee.lightType)
        {
            case LightTypes.Red:
                if (attacker.lightType == LightTypes.Yellow) dmg *= WeakBuff; 
                break;
            case LightTypes.Yellow:
                if (attacker.lightType == LightTypes.Blue) dmg *= WeakBuff;
                break;
            case LightTypes.Blue:
                if (attacker.lightType == LightTypes.Red) dmg *= WeakBuff;
                break;
            default:
                break;
        }
        // Switch Case for Smolour Buffs
        switch (attacker.lightType)
        {
            case LightTypes.Red:
                dmg += attacker.redBonus;
                dmg *= attacker.redMultiplier;
                break;
            case LightTypes.Blue:
                dmg += attacker.blueBonus;
                dmg *= attacker.blueMultiplier;
                break;
            case LightTypes.Yellow:
                dmg += attacker.yellowBonus;
                dmg *= attacker.yellowMultiplier;
                break;
            default:
                break;
        }

        health -= dmg;

        stateController.actionDesc = "Player attacks " + attackee.name + " for " + dmg + " damage!";

    }

    public void HealDamage(int heal)
    {
        if (lightType == LightTypes.Blue) health += health * 1.25f;
        else health += heal;
        if (health > maxHealth) health = maxHealth;
    }

    // WaitUnitStatsVer() is a temporary numerable called in place of actual animation and gives players
    // enough time to read the action desc to understand what the hell is happening
    // - noelle
    protected IEnumerator WaitUnitStatsVer()
    {   
        yield return new WaitForSeconds(0.5f);
        stateController.NextTurn();
        print("WaitUnitStatsVer() called");
    }
}
