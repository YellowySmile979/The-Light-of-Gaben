using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UnitStats : MonoBehaviour
{
    [Header("Unit Stats")]
    public float attack = 10;
    public float defense = 10;
    public float maxDefence;
    public float WV = 2;
    public float speed = 10;
    public float crit = 0;
    public float nextTurnIn = 0;
    public float maxHealth = 50;
    public float health = 50;
    public float level = 1;
    float WeakBuff = 2.5f;

    public bool hasFinishedTheirTurn;

    protected bool hasAttacked;

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

    [Header("Wiggle")]
    public float wiggleOffset;
    public float wiggleSpeed;
    Vector3 origin;
    bool direction;

    public CombatStateController stateController;

    public enum LightTypes { White, Red, Yellow, Blue, Orange, Magenta, Green };
    public LightTypes lightType = LightTypes.White;

    void Awake()
    {
        stateController = FindObjectOfType<CombatStateController>();
        maxHealth += hpBonus;
        if (defense <= 0) defense = 1;
        UpdateComb();
    }
    // Calculates nextTurnIn value   
    public void CalculateNextTurn(int currentTurn)
    {
        nextTurnIn = currentTurn + (Random.Range(1, 50) - (speed+speedBonus));
    }
    //handles the wiggling of the enemy
    IEnumerator Wiggle(UnitStats attackee)
    {
        bool doWiggle = true;
        origin = attackee.transform.position;
        int count = 0;

        while (doWiggle)
        {
            //changes direction depending on which direction dude moves
            if(attackee.transform.position.x >= origin.x + wiggleOffset - 0.01f)
            {
                direction = true;
            }
            else if(attackee.transform.position.x <= origin.x - wiggleOffset + 0.01f)
            {
                direction = false;
            }
            //dpending on the bool, move accordingly
            if (direction)
            {
                attackee.transform.position -= new Vector3(wiggleSpeed, 0) * Time.deltaTime; 
            }
            else
            {
                attackee.transform.position += new Vector3(wiggleSpeed, 0) * Time.deltaTime;
            }
            //falls outta the loop when count reaches more than 100
            if(count > 100)
            {
                //print("Count: " + count);
                break;
            }
            count++;
            yield return new WaitForSeconds(0.0001f);
        }
    }
    // Self Actions: Methods called by other CombatControllers to affect their targetting unit
    public void TakeDamage(float dmg, UnitStats attacker, UnitStats attackee)
    {
        print(attackee + " took damage.");

        // Red > Blue > Yellow > Red
        // Magenta > Green > Orange > Magenta
        
        dmg = (((2 * attacker.level * 
            (attacker.crit + attacker.critBonus) / 5) + 2) 
            * attacker.WV 
            * (((attacker.attack + attacker.attackBonus)) / (attackee.defense+ attackee.defenseBonus)) / 2) +2;

        if (attackee.GetComponent<EnemyCombatController>() || attackee.GetComponent<ShadowKingCombatController>())
        {
            print("WIGGLE");
            StopCoroutine(Wiggle(attackee));
            StartCoroutine(Wiggle(attackee));
        }
        // Switch Case for Light Weaknes

        /*Debug.Log(
            "level: " + attacker.level +
            "crit " + attacker.crit +
            "critbonus " + attacker.critBonus +
            "wv " + attacker.WV +
            "attack " + attacker.attack +
            "attackbonus " + attacker.attackBonus +
            "defensee " + attackee.defense +
            "defensebonuss " + attackee.defenseBonus
            );*/

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
                print("Gaben is White"); // ,':/
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
        
        print("The dmg dealt by " + attacker.name + " is: " + dmg);
        dmg = Mathf.Round(dmg);
        print("The dmg dealt by " + attacker.name + " is: " + dmg);
        health -= dmg;

        hasAttacked = true;
        if (attacker.GetComponent<PlayerCombatController>()
            &&
            (CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.yellow.colour
            || CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.red.colour
            || CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.blue.colour)
            )
        {
            attacker.GetComponent<PlayerCombatController>().ColourEffects(dmg);
        }

        stateController.actionDesc = attacker.name + " attacks " + attackee.name + " for " + dmg + " damage!";
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