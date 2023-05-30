using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasController : MonoBehaviour
{
    PlayerCombatController player;
    CombatStateController stateController;
    public static CanvasController Instance;

    public Image gabenHPBar;
    public Text combatActions;
    public GameObject lightChanger;
    public Image lightBG;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        lightBG = GameObject.Find("LightBackGround").GetComponent<Image>();
        lightChanger.SetActive(false);
    }
    private void Update()
    {
        //corresponds the hp to the visual element
        gabenHPBar.fillAmount = Mathf.Clamp(player.health / player.maxHealth, 0, 1f);
        //sets the text
        combatActions.text = stateController.actionDesc.ToString();
        //changes the colour of the attack accordingly (depending on what button the player chooses
        if (player.lightType == UnitStats.LightTypes.Red) lightBG.color = Color.red;
        else if (player.lightType == UnitStats.LightTypes.Blue) lightBG.color = Color.blue;
        else if (player.lightType == UnitStats.LightTypes.Yellow) lightBG.color = Color.yellow;
        else lightBG.color = Color.white;

        //deactivates/activates the UI for the battle menu
        if (stateController.state == CombatStateController.GameStates.Player) LightChangeMenu();
        else LightChangeExit();
    }
    public void LightChangeMenu()
    {
        lightChanger.SetActive(true);
    }

    public void LightChangeExit()
    {
        lightChanger.SetActive(false);
    }
}
