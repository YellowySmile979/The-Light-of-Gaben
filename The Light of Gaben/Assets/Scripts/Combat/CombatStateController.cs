using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatStateController : MonoBehaviour
{
    public BlackFade fade;
    private List<UnitStats> TurnOrder;
    UnitStats stats;
    public string actionDesc;
    public AudioClip combatMusic1, combatMusic2, victorySFX, lossSFX, clawSFX, whiteSFX, blueSFX, redSFX, yellowSFX;
    public AudioSource camAudioSource;
    public Canvas turnBasedScreen;

    PlayerCombatController player;
    EnemyCombatController enemy;
    CanvasController canvasController;

    // Each turn has different states, Start State, Player Action, Enemy Action, and Passive Actions.
    // Honestly, this is prob gonna be more for debugging and testing. - noelle

    public enum GameStates { Start, Player, Enemy, Passive, End }
    public GameStates state = GameStates.Start;

    void Start()
    {
        TurnOrder = new List<UnitStats>();
        stats = FindObjectOfType<UnitStats>();
        fade = FindObjectOfType<BlackFade>();
        player = FindObjectOfType<PlayerCombatController>();
        enemy = FindObjectOfType<EnemyCombatController>();
        canvasController = FindObjectOfType<CanvasController>();
        camAudioSource = FindObjectOfType<LevelManager>().GetComponent<AudioSource>();
        if (state == GameStates.Start) StartState();
    }
    //randomises the combat music
    public void RandomiseCombatMusic()
    {
        float randomTrack = Mathf.Round(Random.Range(0, 100));
        print("Random music");
        if(randomTrack < 50)
        {
            camAudioSource.PlayOneShot(combatMusic1);
        }
        else
        {
            camAudioSource.PlayOneShot(combatMusic2);
        }
    }
    void StartState()
    {
        RandomiseCombatMusic();
        //Start State
        //Rolls every units Speed to determine turns.
        // Player Units turns (Gaben + Smolours)
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject playerUnit in playerUnits)
        {
            UnitStats currentPlayerUnit = playerUnit.GetComponent<UnitStats>();
            currentPlayerUnit.CalculateNextTurn(0);
            TurnOrder.Add(currentPlayerUnit);
        }
        // Enemy Units turns
        GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach (GameObject enemyUnit in enemyUnits)
        {
            UnitStats currentEnemyUnit = enemyUnit.GetComponent<UnitStats>();
            currentEnemyUnit.CalculateNextTurn(0);
            TurnOrder.Add(currentEnemyUnit);
        }
        //Adding another object here to trigger all passive Damage Over Time effects - noelle

        // All units are sorted in an array (TurnOrder) by their nextTurnIn, in ascending order
        // When a unit acts, their nextTurnIn goes up, and TurnOrder is resorted, with the next
        // lowest nextTurnIn.

        TurnOrder.Sort(SortByTurn);
        print("TurnOrder: " + TurnOrder);
        NextTurn();
    }

    // Comparable called to sort List by nextTurnIn
    static int SortByTurn(UnitStats p1, UnitStats p2)
    {
        return p1.nextTurnIn.CompareTo(p2.nextTurnIn);       
    }

    // Called after Player and Enemy states, calls the next unit in TurnOrder.
    [SerializeField] int currentTurn = -1;
    public void NextTurn()
    {
        //updates the turn count
        if (currentTurn == TurnOrder.Count - 1)
        {
            currentTurn = 0;
        }
        else
        {
            currentTurn++;
        }
        //sets the unit according to the currentTurn
        UnitStats currentUnit = TurnOrder[currentTurn];

        //checks to see if either enemy or player has died
        //if the respective dude has died, perform the respective Coroutine
        //and then return
        if (player.health <= 0)
        {
            StartCoroutine(LostCombat());
            return;
        }
        if (enemy.health <= 0)
        {
            StartCoroutine(WinCombat());
            return;
        }
        //checks to see which unit's turn it should be
        if (currentUnit.tag == "PlayerUnit")
        {
            CanvasController.Instance.lightChanger.SetActive(true);
            actionDesc = "Player is now acting!";
            StartCoroutine(Wait());
            PlayerState();
        }
        else
        {
            CanvasController.Instance.lightChanger.SetActive(false);
            EnemyState();
            actionDesc = "Enemy is now acting!";
            StartCoroutine(Wait());
            if(state != GameStates.End)
            {
                int randomInt = Random.Range(
                    currentUnit.GetComponent<EnemyCombatController>().lowestProbabilityInt,
                    currentUnit.GetComponent<EnemyCombatController>().highestProbabilityInt
                    );

                if(randomInt <= currentUnit.GetComponent<EnemyCombatController>().highestProbabilityInt * 0.7)
                {
                    currentUnit.GetComponent<EnemyCombatController>().Attack();
                }
                else if(randomInt <= currentUnit.GetComponent<EnemyCombatController>().highestProbabilityInt * 0.1)
                {
                    currentUnit.GetComponent<EnemyCombatController>().DoNothing();
                }
                else if(randomInt <= currentUnit.GetComponent<EnemyCombatController>().highestProbabilityInt * 0.2)
                {
                    currentUnit.GetComponent<EnemyCombatController>().HealSelf();
                }
            }
        }
        //}
    }
    //call this to change the state to player
    void PlayerState()
    {
        state = GameStates.Player;
        TurnOrder.Sort(SortByTurn);

    }
    //call this to change the state to player
    void EnemyState()
    {
        state = GameStates.Enemy;
        TurnOrder.Sort(SortByTurn);
    }
    void PassivesState()
    {
        state = GameStates.Passive;
    }
    //when player loses, set the gamestate to end, play the lossSFX, tell player they have lost,
    //fadeout the scene and then reset the variables in the levelManager
    IEnumerator LostCombat()
    {
        state = GameStates.End;
        camAudioSource.PlayOneShot(lossSFX);
        actionDesc = "You Lost!";
        yield return new WaitForSeconds(2);        
        // Uh load the scene before this
        fade.FadeOut();
        yield return new WaitForSeconds(1);
        LevelManager.Instance.hasUnloaded = false;
        LevelManager.Instance.hasWon = false;
        LevelManager.Instance.enemies[LevelManager.Instance.theEnemy].hasLoaded = false;
        LevelManager.Instance.inCombat = false;
    }
    //when player wins, set the gamestate to end, play the victorySFX, tell player they have won,
    //fadeout the scene and then reset the variables in the levelManager
    IEnumerator WinCombat()
    {
        print("WinCombat");
        state = GameStates.End;
        camAudioSource.PlayOneShot(victorySFX);
        actionDesc = "You Won!";
        enemy.ScaleXPWithLevel();
        yield return new WaitForSeconds(2);
        // Uh load the scene before this
        fade.FadeOut();
        yield return new WaitForSeconds(1);
        canvasController.gabenHPBar.enabled = false;
        LevelManager.Instance.hasUnloaded = false;
        LevelManager.Instance.hasWon = true;
        LevelManager.Instance.enemies[LevelManager.Instance.theEnemy].hasLoaded = false;
        LevelManager.Instance.inCombat = false;
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}
