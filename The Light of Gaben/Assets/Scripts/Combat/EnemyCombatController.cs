using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombatController : UnitStats
{
    UnitStats player;
    //public GameObject healthBar;
    public Image healthBar;

    private void Start()
    {
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
        player = FindObjectOfType<PlayerCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        player.TakeDamage(attack, this, player);
        stateController.actionDesc = stateController.actionDesc + " Enemy attacks the player for 10 damage!";
        StartCoroutine(WaitUnitStatsVer());
    }
}