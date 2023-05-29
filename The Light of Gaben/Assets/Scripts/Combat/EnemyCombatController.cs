using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : UnitStats
{
    UnitStats player;
    public GameObject healthBar;

    private void Start()
    {
        RandomColour();
    }
    void RandomColour()
    {
        var randomColour = Random.Range(1, 5);
        switch (randomColour)
        {
            case 1:
                lightType = LightTypes.White;
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case 2:
                lightType = LightTypes.Red;
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 3:
                lightType = LightTypes.Blue;
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case 4:
                lightType = LightTypes.Yellow;
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            default:
                lightType = LightTypes.White;
                break;
        }
    }
    private void Update()
    {
        healthBar.transform.localScale = new Vector3((health/maxHealth) * 2f, -0.1f, 1);
    }
    public void Attack()
    {
        print("Enemy Attack");
        player = FindObjectOfType<PlayerCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        player.TakeDamage(attack, this, player);
        stateController.actionDesc = stateController.actionDesc + " Enemy attacks the player for 10 damage!";
        StartCoroutine(WaitUnitStatsVer());
    }
}