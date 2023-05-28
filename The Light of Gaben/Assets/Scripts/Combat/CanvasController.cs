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
    public Image enemyHPBar;
    public Text gabenHealth;
    public Text enemyHealth;
    public Text gameStateAnnouncer;
    public GameObject playerActions;
    public Text combatActions;

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
        playerActions.SetActive(true);
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
        gabenHealth.text = player.health.ToString() + "/" + player.maxHealth.ToString();

        enemyHPBar.fillAmount = Mathf.Clamp(enemy.health / enemy.maxHealth, 0, 1f);
        enemyHealth.text = enemy.health.ToString() + "/" + enemy.maxHealth.ToString();

        gameStateAnnouncer.text = stateController.state.ToString();
        combatActions.text = stateController.actionDesc.ToString();

    }
}
