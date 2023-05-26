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

    void Start()
    {
        health = maxHealth;
        stateController = FindObjectOfType<CombatStateController>();
    }

    // Calculates nextTurnIn value
    
    public void CalculateNextTurn(int currentTurn)
    {
        nextTurnIn = currentTurn + (Random.Range(1, 50) - speed);
    }

    // Self Actions: Methods called by other CombatControllers to affect their targetting unit
    public void TakeDamage(int dmg)
    {
        print("Took Damage");
        health -= dmg;
    }

    public void HealDamage(int heal)
    {
        health += heal;
    }

    // WaitUnitStatsVer() is a temporary numerable called in place of actual animation and give player
    // enough time to read the action desc to undersatdn what the hell is happening
    // - noelle
    public IEnumerator WaitUnitStatsVer()
    {        
        yield return new WaitForSeconds(1f);
        stateController.NextTurn();
        print("WaitUnitStatsVer() called");
    }
}
