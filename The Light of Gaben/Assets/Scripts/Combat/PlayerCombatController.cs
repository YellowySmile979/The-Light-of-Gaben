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

    public RuntimeAnimatorController whiteAnim;
    public RuntimeAnimatorController redAnim;
    public RuntimeAnimatorController blueAnim;
    public RuntimeAnimatorController yellowAnim;
    public RuntimeAnimatorController orangeAnim;
    public RuntimeAnimatorController greenAnim;
    public RuntimeAnimatorController purpleAnim;
    public Animator animator;

    public static PlayerCombatController Instance;

    void Awake()
    {
        Instance = this;
        smolourController = FindObjectOfType<PlayerSmolourController>();
    }

    void Start()
    {
        HealthBar.Instance.maxHealth = this.maxHealth;
        HealthBar.Instance.currentHealth = PlayerPrefs.GetFloat("Current Health");


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
        //stops the anim
        animator.speed = 0;
    }
    void Update()
    {
        //ensures that the object is hidden when the animator end
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            flash.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }
    public void SelectTarget()
    {
        //Temp
        attackTarget = FindObjectOfType<EnemyCombatController>();
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
        //shows what happened
        stateController.actionDesc = "Player heals themself for " + heal + " health!";
        //updates the hp bar for the main HUD
        //HealthBar.Instance.currentHealth = this.health;
        PlayerPrefs.SetFloat("Current Health", this.health);
        StartCoroutine(WaitUnitStatsVer());
    }
    //animations for attacks
    void AnimationAttack()
    {
        animator.runtimeAnimatorController = null;
        print("This animation attack");
        //enables the flash
        flash.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        //starts the anim
        animator.speed = 1;
        //decides which anim to play
        if (CanvasController.Instance.result.GetComponent<Image>().color == Color.white)
        {
            animator.runtimeAnimatorController = whiteAnim;
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.red.colour)
        {
            animator.runtimeAnimatorController = redAnim;
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.blue.colour)
        {
            animator.runtimeAnimatorController = blueAnim;
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.yellow.colour)
        {
            animator.runtimeAnimatorController = yellowAnim;
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.orange.colour)
        {
            animator.runtimeAnimatorController = orangeAnim;
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.green.colour)
        {
            animator.runtimeAnimatorController = greenAnim;
        }
        else if (CanvasController.Instance.result.GetComponent<Image>().color == CanvasController.Instance.magenta.colour)
        {
            animator.runtimeAnimatorController = purpleAnim;
        }
    }
    public void LightChangerBlue()
    {
        lightType = LightTypes.Blue;
        stateController.actionDesc = "Player changes their light to Blue!";
    }

    public void LightChangerRed()
    {
        lightType = LightTypes.Red;
        stateController.actionDesc = "Player changes their light to Red!";
    }

    public void LightChangerYellow()
    {
        lightType = LightTypes.Yellow;
        stateController.actionDesc = "Player changes their light to Yellow!";
    }

    public void LightChangerOrange()
    {
        lightType = LightTypes.Orange;
        stateController.actionDesc = "Player changes their light to Orange!";
    }
    public void LightChangerGreen()
    {
        lightType = LightTypes.Green;
        stateController.actionDesc = "Player changes their light to Green!";
    }
    public void LightChangerMagenta()
    {
        lightType = LightTypes.Magenta;
        stateController.actionDesc = "Player changes their light to Magenta!";
    }
    public void LightChangerWhite()
    {
        lightType = LightTypes.White;
        stateController.actionDesc = "Player changes their light to White!";
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
