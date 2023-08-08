using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatController : UnitStats
{
    UnitStats attackTarget;
    UnitStats healTarget;
    PlayerSmolourController smolourController;
    int playerPrefsDMG;
    [Header("Player Stats")]
    public float playerXP = 0;
    public GameObject w;

    [Header("Animator")]
    public GameObject flash;

    public RuntimeAnimatorController heal;
    public Animator animator;

    public static PlayerCombatController Instance;

    public AudioSource audioSource;
    public AudioClip whiteSFX, blueSFX, redSFX, yellowSFX, colourSFX;

    UnitStats enemy;

    void Awake()
    {
        Instance = this;
        smolourController = FindObjectOfType<PlayerSmolourController>();
    }

    void Start()
    {
        HealthBar.Instance.maxHealth = this.maxHealth;
        HealthBar.Instance.currentHealth = PlayerPrefs.GetFloat("Current Health");

        maxDefence = defense;

        hpBonus = smolourController.hpPlus;
        attackBonus = smolourController.atkPlus;
        defenseBonus = smolourController.defPlus;
        critBonus = smolourController.critPlus;
        speedBonus = smolourController.spPlus;

        redMultiplier = smolourController.redPlus;
        blueMultiplier = smolourController.bluePlus;
        yellowMultiplier = smolourController.yellowPlus;
        orangeMultiplier = smolourController.orangePlus;
        greenMultiplier = smolourController.greenPlus;
        magentaMultiplier = smolourController.magentaPlus;
    }
    public void SelectTarget()
    {
        //Temp
        if(FindObjectOfType<EnemyCombatController>())
        {
            attackTarget = FindObjectOfType<EnemyCombatController>();
        }
        else if (FindObjectOfType<ShadowKingCombatController>())
        {
            attackTarget = FindObjectOfType<ShadowKingCombatController>();
        }
        stateController = FindObjectOfType<CombatStateController>();
        healTarget = this;
    }
    public void Attack()
    {
        //selects the target
        SelectTarget();
        //sets the hp bar for the main HUD
        //HealthBar.Instance.currentHealth = this.health;
        PlayerPrefs.SetFloat("Current Health", this.health);
        AnimationAttack();
        if(CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.orange.colour
           || CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.magenta.colour
           || CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.green.colour
           )
        {
            ColourEffects();
        }
        int damage = (int)attack;
        playerPrefsDMG += damage;
        //sets the total damage for the end card
        PlayerPrefs.SetInt("Total Attack", playerPrefsDMG);
        //makes enemy take dmg
        attackTarget.TakeDamage(damage, this, attackTarget);
        StartCoroutine(WaitUnitStatsVer());
    }

    public void Heal()
    {
        //selects the target
        SelectTarget();
        //randomises health to heal
        int heal = Random.Range(1, 20) + (int)attack;
        //heals self
        healTarget.HealDamage(heal);
        stateController.camAudioSource.PlayOneShot(healSFX);
        //shows what happened
        stateController.actionDesc = "Gaben heals himself for " + heal + " health!";
        //updates the hp bar for the main HUD
        //HealthBar.Instance.currentHealth = this.health;
        PlayerPrefs.SetFloat("Current Health", this.health);
        StartCoroutine(WaitUnitStatsVer());
    }
    //handles the different colour effects
    public void ColourEffects(float damage = 0)
    {
        //resets player's defence
        defense = maxDefence;
        if(FindObjectOfType<EnemyCombatController>())
        {
            enemy = FindObjectOfType<EnemyCombatController>();
        }
        else if (FindObjectOfType<ShadowKingCombatController>())
        {
            enemy = FindObjectOfType<ShadowKingCombatController>();
        }
        //resets enemy's defence
        if (enemy != null) enemy.defense = enemy.maxDefence;

        if (CanvasController.Instance.result.GetComponent<Image>().color == Color.white)
        {
            print("White Colour Effect");
            return;
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.red.colour)
        {
            print("Red Colour Effect");
            //heal 10% of dmg dealt
            if (hasAttacked)
            {
                health += damage * 0.1f;
                if (health > maxHealth) health = maxHealth;

                hasAttacked = false;
            }
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.blue.colour)
        {
            print("Blue Colour Effect");
            //increases defence by 40% of player's max hp
            defense = maxHealth * 0.4f;
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.yellow.colour)
        {
            print("Yellow Colour Effect");
            //ignore enemy defence
            enemy.defense = 1;
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.orange.colour)
        {
            print("Orange Colour Effect");
            //increases dmg dealt by 20%
            attack += damage * 0.2f;
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.magenta.colour)
        {
            print("Magenta Colour Effect");
            //gain 5% max hp, and each attack consumes 5% hp to deal extra 5% damage to opponents
            float maxHp = this.maxHealth;
            this.maxHealth = maxHp + maxHp * 0.05f;

            this.health -= maxHp * 0.05f;
            attack += maxHp * 0.05f;
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.green.colour)
        {
            print("Green Colour Effect");
            //player defence increases by 20% and heal 15% of max hp each attack
            defense = maxDefence + maxDefence * 0.2f;

            health += maxHealth * 0.15f;
            if (health > maxHealth) health = maxHealth;
        }
    }
    //animations for attacks
    void AnimationAttack()
    {
        print("This animation attack");
        //decides which anim to play
        if (CanvasController.Instance.result.GetComponent<Image>().sprite == CanvasController.Instance.white)
        {
            //animator.runtimeAnimatorController = whiteAnim;
            animator.SetTrigger("White");
            stateController.camAudioSource.PlayOneShot(whiteSFX);
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().sprite == CanvasController.Instance.red.colourIcon)
        {
            //animator.runtimeAnimatorController = redAnim;
            animator.SetTrigger("Red");
            stateController.camAudioSource.PlayOneShot(redSFX);

        }
        else if (CanvasController.Instance.result.GetComponent<Image>().sprite == CanvasController.Instance.blue.colourIcon)
        {
            //animator.runtimeAnimatorController = blueAnim;
            animator.SetTrigger("Blue");
            stateController.camAudioSource.PlayOneShot(blueSFX);

        }
        else if (CanvasController.Instance.result.GetComponent<Image>().sprite == CanvasController.Instance.yellow.colourIcon)
        {
            //animator.runtimeAnimatorController = yellowAnim;
            animator.SetTrigger("Yellow");
            stateController.camAudioSource.PlayOneShot(yellowSFX);
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().sprite == CanvasController.Instance.orange.colourIcon)
        {
            //animator.runtimeAnimatorController = orangeAnim;
            animator.SetTrigger("Orange");
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().sprite == CanvasController.Instance.green.colourIcon)
        {
            //animator.runtimeAnimatorController = greenAnim;
            animator.SetTrigger("Green");
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().sprite == CanvasController.Instance.magenta.colourIcon)
        {
            //animator.runtimeAnimatorController = purpleAnim;
            animator.SetTrigger("Magenta");
        }
    }
    public void LightChangerBlue()
    {
        lightType = LightTypes.Blue;
        stateController.actionDesc = "Gaben changes his light to Blue!";
        audioSource.PlayOneShot(colourSFX);
    }

    public void LightChangerRed()
    {
        lightType = LightTypes.Red;
        stateController.actionDesc = "Gaben changes his light to Red!";
        audioSource.PlayOneShot(colourSFX);
    }

    public void LightChangerYellow()
    {
        lightType = LightTypes.Yellow;
        audioSource.PlayOneShot(colourSFX);
        stateController.actionDesc = "Gaben changes his light to Yellow!";
    }

    public void LightChangerOrange()
    {
        lightType = LightTypes.Orange;
        audioSource.PlayOneShot(colourSFX);
        stateController.actionDesc = "Gaben changes his light to Orange!";
    }
    public void LightChangerGreen()
    {
        lightType = LightTypes.Green;
        audioSource.PlayOneShot(colourSFX);
        stateController.actionDesc = "Gaben changes his light to Green!";
    }
    public void LightChangerMagenta()
    {
        lightType = LightTypes.Magenta;
        audioSource.PlayOneShot(colourSFX);
        stateController.actionDesc = "Gaben changes his light to Magenta!";
    }
    public void LightChangerWhite()
    {
        lightType = LightTypes.White;
        audioSource.PlayOneShot(colourSFX);
        stateController.actionDesc = "Gaben changes his light to White!";
    }
    public void UpdatePlayerLevel(float xpGiven)
    {
        playerXP += xpGiven;
        print("playerXP: " + playerXP);
        if (playerXP >= 0 && playerXP < 200)
        {
            level = 1;
        }
        else if (playerXP >= 200 && playerXP < 330)
        {
            level = 2;
        }
        else if (playerXP >= 330 && playerXP < 460)
        {
            level = 3;
        }
        else if (playerXP >= 460 && playerXP < 590)
        {
            level = 4;
        }
        else if (playerXP >= 590 && playerXP < 620)
        {
            level = 5;
        }
        else if (playerXP >= 620 && playerXP < 770)
        {
            level = 6;
        }
        else if (playerXP >= 770 && playerXP < 920)
        {
            level = 7;
        }
        else if (playerXP >= 920 && playerXP < 1100)
        {
            level = 8;
        }
        else if (playerXP >= 1100 && playerXP < 1280)
        {
            level = 9;
        }
        else if (playerXP >= 1280 && playerXP < 1500)
        {
            level = 10;
        }
        else if (playerXP >= 1500 && playerXP < 1720)
        {
            level = 11;
        }
        else if (playerXP >= 1720 && playerXP < 1940)
        {
            level = 12;
        }
        else if (playerXP >= 1940 && playerXP < 2210)
        {
            level = 13;
        }
        else if (playerXP >= 2210 && playerXP < 2480)
        {
            level = 14;
        }
        else if (playerXP >= 2480 && playerXP < 2750)
        {
            level = 15;
        }
        else if (playerXP >= 2750 && playerXP < 3050)
        {
            level = 16;
        }
        else if (playerXP >= 3050 && playerXP < 3350)
        {
            level = 17;
        }
        else if (playerXP >= 3350 && playerXP < 3950)
        {
            level = 18;
        }
        else if (playerXP >= 3950 && playerXP < 4550)
        {
            level = 19;
        }
        else if (playerXP >= 4550)
        {
            level = 20;
        }
        GeneralCanvasStuff.Instance.UpdateLevelText(level);
    }
}
