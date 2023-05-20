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
        health = maxHealth;stateController = FindObjectOfType<CombatStateController>();
    }

    // Calculates nextTurnIn value
    
    public void calculateNextTurn(int currentTurn)
    {
        nextTurnIn = currentTurn + (Random.Range(1, 50) - speed);
    }

    // Self Actions: Methods called by other CombatControllers to affect their targetting unit
    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }

    public void HealDamage(int heal)
    {
        health += heal;
    }

    // Wait() is a temporary numerable called in place of actual animation and give player
    // enough time to read the action desc to undersatdn what the hell is happening
    // - noelle
    public IEnumerator Wait()
    {
        print("Wait() called");
        yield return new WaitForSeconds(2f);
        stateController.nextTurn();
    }
}
