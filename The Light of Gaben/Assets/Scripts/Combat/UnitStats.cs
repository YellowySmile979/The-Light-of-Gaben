using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitStats : MonoBehaviour
{
    [Header("Unit Stats")]
    public float attack = 10;
    public float defense = 0;
    public float WV = 2;
    public float speed = 10;
    public float crit = 0;
    public float nextTurnIn = 0;
    public float maxHealth = 50;
    public float health = 50;
    public float level = 1;
    float WeakBuff = 2.5f;

    public bool hasFinishedTheirTurn;

    [Header("Smolour Buff Stats")]
    public float redMultiplier = 1.0f;
    public float yellowMultiplier = 1.0f;
    public float blueMultiplier = 1.0f;
    public float orangeMultiplier = 1.0f;
    public float greenMultiplier = 1.0f;
    public float magentaMultiplier = 1.0f;
    public float hpBonus = 0.0f;
    public float speedBonus = 0.0f;
    public float attackBonus = 0.0f;
    public float defenseBonus = 0.0f;
    public float shield = 0.0f;
    public float critBonus = 0;
    public List<SmoloursData> smolourBuffs;

    public bool isDead = false;

    [Header("SFX")]
    public AudioClip clawSFX, healSFX;

    /*[Header("Wiggle")]
    public RuntimeAnimatorController bodyguardHurtAnim, archerHurtAnim, summonerHurtAnim;
    public Animator enemy;
    public Sprite bodyguard, archer, summoner;*/

    public CombatStateController stateController;

    public enum LightTypes { White, Red, Yellow, Blue, Orange, Magenta, Green };
    public LightTypes lightType = LightTypes.White;

    void Awake()
    {
        stateController = FindObjectOfType<CombatStateController>();
        maxHealth += hpBonus;
        UpdateComb();
    }
    /*void Start()
    {
        enemy = FindObjectOfType<EnemyCombatController>().GetComponent<Animator>();
        enemy.speed = 0;
    }
    void Update()
    {
        //ensures that the object is hidden when the animator end
        if (enemy.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            enemy.speed = 0;
        }
    }*/
    // Calculates nextTurnIn value   
    public void CalculateNextTurn(int currentTurn)
    {
        nextTurnIn = currentTurn + (Random.Range(1, 50) - (speed+speedBonus));
    }
    /*void Wiggle(UnitStats attackee)
    {
        enemy.runtimeAnimatorController = null;
        print("This animation attack");
        //starts the anim
        enemy.speed = 1;
        if (FindObjectOfType<EnemyCombatController>().image == bodyguard)
        {
            enemy.runtimeAnimatorController = bodyguardHurtAnim;
        }
        else if (FindObjectOfType<EnemyCombatController>().image == archer)
        {
            enemy.runtimeAnimatorController = archerHurtAnim;
        }
        else if(FindObjectOfType<EnemyCombatController>().image == summoner)
        {
            enemy.runtimeAnimatorController = summonerHurtAnim;
        }
    }*/
    // Self Actions: Methods called by other CombatControllers to affect their targetting unit
    public void TakeDamage(float dmg, UnitStats attacker, UnitStats attackee)
    {
        print("Took Damage");

        // Red > Blue > Yellow > Red
        // Magenta > Green > Orange > Magenta

        
        dmg = (((2 * attacker.level * 
            (attacker.crit + attacker.critBonus) / 5) + 2) 
            * attacker.WV 
            * (((attacker.attack + attacker.attackBonus)) / (attackee.defense+ attackee.defenseBonus)) / 2) +2;

        /*if (attackee.GetComponent<EnemyCombatController>())
        {
            print("WIGGLE");
            Wiggle(attackee);
        }*/
        // Switch Case for Light Weakness
        switch (attackee.lightType)
        {
            case LightTypes.Red:
                if (attacker.lightType == LightTypes.Yellow)
                {
                    dmg *= WeakBuff;
                }
                else if(attacker.lightType == LightTypes.Magenta || attacker.lightType == LightTypes.Orange)
                {
                    dmg *= WeakBuff / 2;
                }
                break;
            case LightTypes.Yellow:
                if (attacker.lightType == LightTypes.Blue)
                {
                    dmg *= WeakBuff;
                }
                else if (attacker.lightType == LightTypes.Green || attacker.lightType == LightTypes.Orange)
                {
                    dmg *= WeakBuff / 2;
                }
                break;
            case LightTypes.Blue:
                if (attacker.lightType == LightTypes.Red)
                {
                    dmg *= WeakBuff;
                }
                else if (attacker.lightType == LightTypes.Magenta || attacker.lightType == LightTypes.Green)
                {
                    dmg *= WeakBuff / 2;
                }
                break;
            case LightTypes.Orange:
                if(attacker.lightType == LightTypes.Green)
                {
                    dmg *= WeakBuff;
                }
                else if (attacker.lightType == LightTypes.Yellow || attacker.lightType == LightTypes.Blue)
                {
                    dmg *= WeakBuff / 2;
                }
                break;
            case LightTypes.Magenta:
                if (attacker.lightType == LightTypes.Orange)
                {
                    dmg *= WeakBuff;
                }
                else if (attacker.lightType == LightTypes.Yellow || attacker.lightType == LightTypes.Red)
                {
                    dmg *= WeakBuff / 2;
                }
                break;
            case LightTypes.Green:
                if (attacker.lightType == LightTypes.Magenta)
                {
                    dmg *= WeakBuff;
                }
                else if (attacker.lightType == LightTypes.Red || attacker.lightType == LightTypes.Blue)
                {
                    dmg *= WeakBuff / 2;
                }
                break;
            default:
                print("Gaben is White");
                break;
        }
        
        // Switch Case for Smolour Buffs
        switch (attacker.lightType)
        {
            case LightTypes.Red:
                dmg *= attacker.redMultiplier;
                break;
            case LightTypes.Blue:
                dmg *= attacker.blueMultiplier;
                break;
            case LightTypes.Yellow:
                dmg *= attacker.yellowMultiplier;
                break;
            case LightTypes.Orange:
                dmg *= attacker.orangeMultiplier;
                break;
            case LightTypes.Green:
                dmg *= attacker.greenMultiplier;
                break;
            case LightTypes.Magenta:
                dmg *= attacker.magentaMultiplier;
                break;
            default:
                print("No Smolours");                
                break;
        }
        print("The dmg is: " + dmg);
        health -= dmg;

        stateController.actionDesc = "Player attacks " + attackee.name + " for " + dmg + " damage!";
    }

    public void HealDamage(float heal)
    {
        if (lightType == LightTypes.Blue) heal = heal * 1.25f;
        else if (lightType == LightTypes.Magenta) heal = heal * 1.5f;
        else if (lightType == LightTypes.Green) heal = heal * 1.5f;

        health += heal / 2;
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

    public void UpdateComb()
    {
        hpBonus = 0;
        attackBonus = 0;
        defenseBonus = 0;
        speedBonus = 0;
        critBonus = 0;
        shield = 0;
        redMultiplier = 0;
        blueMultiplier = 0;
        yellowMultiplier = 0;
        orangeMultiplier = 0;
        greenMultiplier = 0;
        magentaMultiplier = 0;

        foreach (SmoloursData smoloursData in smolourBuffs)
        {
            hpBonus += smoloursData.hpBonus;
            attackBonus += smoloursData.attackBonus;
            defenseBonus += smoloursData.defenseBonus;
            speedBonus += smoloursData.speedBonus;
            critBonus += smoloursData.critBonus;
            shield += smoloursData.shield;
            redMultiplier += smoloursData.redMultiplier;
            blueMultiplier += smoloursData.blueMultiplier;
            yellowMultiplier += smoloursData.yellowMultiplier;
            orangeMultiplier += smoloursData.orangeMultiplier;
            greenMultiplier += smoloursData.greenMultiplier;
            magentaMultiplier += smoloursData.magentaMultiplier;
        }
    }
}