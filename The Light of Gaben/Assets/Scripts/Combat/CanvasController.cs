using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasController : MonoBehaviour
{
    PlayerCombatController player;
    EnemyCombatController enemy;
    CombatStateController stateController;
    public static CanvasController Instance;

    public Image gabenHPBar;
    public Text combatActions;
    public GameObject lightChanger;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerCombatController>();
        enemy = FindObjectOfType<EnemyCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        lightChanger.SetActive(false);
    }
    private void Update()
    {
        /*if (stateController.state == CombatStateController.GameStates.Player)
        {
            playerActions.SetActive(true);
        }
        else
        {
            playerActions.SetActive(false);
        }*/
        gabenHPBar.fillAmount = Mathf.Clamp(player.health / player.maxHealth, 0, 1f);

        combatActions.text = stateController.actionDesc.ToString();

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
