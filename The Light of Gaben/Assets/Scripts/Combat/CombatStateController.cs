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
    public AudioClip combatMusic1, combatMusic2;
    public AudioSource camAudioSource;
    public Canvas turnBasedScreen;

    PlayerCombatController player;
    EnemyCombatController enemy;

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
        camAudioSource = FindObjectOfType<LevelManager>().GetComponent<AudioSource>();
        if (state == GameStates.Start) StartState();
    }
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
        print ("Player Units: " + playerUnits);
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
        print("NextTurn() called");
        if (currentTurn == TurnOrder.Count - 1)
        {
            print("currentTurn has been set to 0");
            currentTurn = 0;
        }
        else
        {
            print("currentTurn has been added");
            currentTurn++;
        }
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
        //UnitStats currentUnitStats = TurnOrder[0];
        //TurnOrder.Remove(currentUnitStats);
        //if (!currentUnitStats.isDead)
        //{
        //    GameObject currentUnit = currentUnitStats.gameObject;
        //    currentUnitStats.calculateNextTurn(currentUnit.GetComponent<UnitStats>().nextTurnIn);
        //    TurnOrder.Add(currentUnitStats);
        //    TurnOrder.Sort(SortByTurn);
        if (currentUnit.tag == "PlayerUnit")
        {
            CanvasController.Instance.lightChanger.SetActive(true);
            Debug.Log("Player unit acting");
            actionDesc = "Player is now acting!";
            StartCoroutine(Wait());
            PlayerState();
        }
        else
        {
            CanvasController.Instance.lightChanger.SetActive(false);
            Debug.Log("Enemy unit acting");
            EnemyState();
            actionDesc = "Enemy is now acting!";
            StartCoroutine(Wait());
            if(state != GameStates.End)
            {
                currentUnit.GetComponent<EnemyCombatController>().Attack();
            }
        }
        //}
    }

    void PlayerState()
    {
        state = GameStates.Player;
        TurnOrder.Sort(SortByTurn);

    }
    void EnemyState()
    {
        state = GameStates.Enemy;
        TurnOrder.Sort(SortByTurn);
    }
    void PassivesState()
    {
        state = GameStates.Passive;
    }

    IEnumerator LostCombat()
    {
        state = GameStates.End;
        actionDesc = "You Lost!";
        yield return new WaitForSeconds(2);        
        // Uh load the scene before this
        fade.FadeOut();
        yield return new WaitForSeconds(1);
        LevelManager.Instance.hasWon = false;
        BaseEnemy.instance.hasLoaded = false;
        LevelManager.Instance.inCombat = false;
    }

    IEnumerator WinCombat()
    {
        state = GameStates.End;
        LevelManager.Instance.SpawnLightShard();
        actionDesc = "You Won!";
        yield return new WaitForSeconds(2);
        // Uh load the scene before this
        fade.FadeOut();
        yield return new WaitForSeconds(1);
        LevelManager.Instance.hasWon = true;
        BaseEnemy.instance.hasLoaded = false;
        LevelManager.Instance.inCombat = false;
    }
    public IEnumerator Wait()
    {
        print("Waiting to do stuff");
        yield return new WaitForSeconds(1f);
    }
}
