using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitStats : MonoBehaviour
{
    [Header("Unit Stats")]
    public int attack = 10;
    public int speed = 10;
    public int nextTurnIn = 0;
    public float maxHealth = 50;
    public float health = 50;

    public bool isDead = false;

    public CombatStateController stateController;

    public enum LightTypes { White, Red, Yellow, Blue };
    public LightTypes lightType = LightTypes.White;
    
    void Start()
    {
        health = maxHealth;
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
        // Light Type If Else statements! There's probably a more efficient way to do this but it's 28 May and our critique is in 2 days.
        // Red > Blue > Yellow > Red
        // - noelle
        float multiplier = 2.5f;
        switch (attackee.lightType)
        {
            case LightTypes.Red:
                if (attacker.lightType == LightTypes.Yellow) dmg = dmg * multiplier; 
                break;
            case LightTypes.Yellow:
                if (attacker.lightType == LightTypes.Blue) dmg = dmg * multiplier;
                break;
            case LightTypes.Blue:
                if (attacker.lightType == LightTypes.Red) dmg = dmg * multiplier;
                break;
            default:
                break;
        }
        health -= dmg;
        stateController.actionDesc = "Player attacks " + attackee.name + " for " + dmg + " damage!";

    }
    //healing part
    public void HealDamage(int heal)
    {
        if (lightType == LightTypes.Blue) health += health * 1.25f;
        else health += heal;
        if (health > maxHealth) health = maxHealth;
    }

    // WaitUnitStatsVer() is a temporary numerable called in place of actual animation and gives players
    // enough time to read the action desc to undersatdn what the hell is happening
    // - noelle
    protected IEnumerator WaitUnitStatsVer()
    {   
        yield return new WaitForSeconds(1f);
        stateController.NextTurn();
    }
}
