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
    public int defeatedEnemies = 0;
    public bool isBossBattle = false;
    bool hasRandomised;
    int count, randomFinalCounter;
    public UnitStats PassiveUnit;

    public ShadowKingPhase shadowKingPhase;

    PlayerCombatController player;
    EnemyCombatController enemy;
    CanvasController canvasController;

    public static CombatStateController Instance;

    // Each turn has different states, Start State, Player Action, Enemy Action, and Passive Actions.
    // Honestly, this is prob gonna be more for debugging and testing. - noelle

    public enum GameStates { Start, Player, Enemy, Passive, End }
    public GameStates state = GameStates.Start;

    void Awake()
    {
        Instance = this;
    }

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
        if (randomTrack < 50)
        {
            camAudioSource.clip = combatMusic1;
            camAudioSource.volume = 0.7f;
            camAudioSource.Play();
            //camAudioSource.PlayOneShot(combatMusic1, 0.7f);
        }
        else
        {
            camAudioSource.clip = combatMusic2;
            camAudioSource.volume = 0.4f;
            camAudioSource.Play();
            //camAudioSource.PlayOneShot(combatMusic2, 0.7f);
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

        TurnOrder.Add(PassiveUnit);
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
        /*if (currentUnit.GetComponent<EnemyCombatController>())
        {
            if(currentUnit.GetComponent<EnemyCombatController>().turnOrder == EnemyManager.Instance.activeCombatEnemies.Count)
            {
                currentUnit = EnemyManager.Instance.activeCombatEnemies[currentUnit.GetComponent<EnemyCombatController>().turnOrder];
            }
        }*/

        //checks to see if either enemy or player has died
        //if the respective dude has died, perform the respective Coroutine
        //and then return
        if (player.health <= 0 && !isBossBattle)
        {
            StartCoroutine(LostCombat());
            return;
        }
        if (player.health <= 0 && isBossBattle)
        {
            StartCoroutine(LoseBossBattle());
            return;
        }
        /*if (EnemyManager.Instance.activeCombatEnemies.Find(s => s.health <= 0))
        {
            EnemyManager.Instance.activeCombatEnemies.Find(s => s.health <= 0).gameObject.SetActive(false);
            return;
        }
        if(defeatedEnemies == EnemyManager.Instance.activeCombatEnemies.Count)
        {
            StartCoroutine(WinCombat());
            return;
        }*/
        if (enemy.health <= 0 && !isBossBattle)
        {
            StartCoroutine(WinCombat());
            return;
        }
        if (enemy.health <= 0 && isBossBattle)
        {
            //boss battle win
            StartCoroutine(WinBossBattle());
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
        else if (currentUnit.tag == "EnemyUnit")
        {
            CanvasController.Instance.lightChanger.SetActive(false);
            EnemyState();
            actionDesc = "Enemy is now acting!";
            StartCoroutine(Wait());
            if (state != GameStates.End)
            {
                if (!isBossBattle)
                {
                    int randomInt = Random.Range(
                        currentUnit.GetComponent<EnemyCombatController>().lowestProbabilityInt,
                        currentUnit.GetComponent<EnemyCombatController>().highestProbabilityInt
                        );

                    if (randomInt <= 70)
                    {
                        currentUnit.GetComponent<EnemyCombatController>().Attack();
                    }
                    else if (randomInt <= 80 && randomInt > 70)
                    {
                        currentUnit.GetComponent<EnemyCombatController>().DoNothing();
                    }
                    else if (randomInt <= 100 && randomInt > 80)
                    {
                        currentUnit.GetComponent<EnemyCombatController>().HealSelf();
                    }
                }
                else
                {
                    //boss changes colour
                    if (!hasRandomised)
                    {
                        count = 0;
                        randomFinalCounter = Random.Range(4, 9);
                        hasRandomised = true;
                    }
                    count++;
                    if (count >= randomFinalCounter)
                    {
                        currentUnit.GetComponent<ShadowKingCombatController>().RandomColour();
                    }

                    if (shadowKingPhase == ShadowKingPhase.Phase1)
                    {
                        //phase 1 attacks                        
                        int randomInt = Random.Range(
                        currentUnit.GetComponent<ShadowKingCombatController>().lowestProbabilityInt,
                        currentUnit.GetComponent<ShadowKingCombatController>().highestProbabilityInt
                        );

                        if (randomInt <= 70) 
                        {
                            //base attack
                            currentUnit.GetComponent<ShadowKingCombatController>().Attack();
                        }
                        else if (randomInt <= 80 && randomInt > 70)
                        {
                            //do nothing
                            currentUnit.GetComponent<ShadowKingCombatController>().DoNothing();
                        }
                        else if (randomInt <= 100 && randomInt > 80)
                        {
                            //heal
                            currentUnit.GetComponent<ShadowKingCombatController>().HealSelf();
                        }
                        else if (shadowKingPhase == ShadowKingPhase.Phase2)
                        {
                            //phase 2 attacks                            
                            randomInt = Random.Range(
                            currentUnit.GetComponent<ShadowKingCombatController>().lowestProbabilityInt,
                            currentUnit.GetComponent<ShadowKingCombatController>().highestProbabilityInt
                            );

                            if (randomInt <= 70)
                            {
                                //base attack
                                currentUnit.GetComponent<ShadowKingCombatController>().Attack();
                            }
                            else if (randomInt <= 80 && randomInt > 70)
                            {
                                //do nothing
                                currentUnit.GetComponent<ShadowKingCombatController>().DoNothing();
                            }
                            else if (randomInt <= 100 && randomInt > 80)
                            {
                                //buffs himself to deal more damage
                                currentUnit.GetComponent<ShadowKingCombatController>().BuffSelf();
                            }
                        }
                        else if (shadowKingPhase == ShadowKingPhase.Phase3)
                        {
                            //phase 3 attacks
                            //applies status effect to player (damage overtime)
                            //stronger attack
                            //buff himself to take damage
                        }
                    }
                }
            }
        }
        else if (currentUnit.tag == "PassiveUnit") 
        {
            actionDesc = "Passive Damage has been set off!";
            currentUnit.GetComponent<PassiveAttacksController>().Damage();
            StartCoroutine(Wait());
            PassiveState();
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
        void PassiveState()
        {
            state = GameStates.Passive;
            TurnOrder.Sort(SortByTurn);
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
            camAudioSource.Stop();
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
            EnemyManager.Instance.activatedCombatEnemies.ForEach(delegate (EnemyCombatController enemy)
            {
                enemy.ScaleXPWithLevel();
            });
            yield return new WaitForSeconds(2);
            // Uh load the scene before this
            fade.FadeOut();
            yield return new WaitForSeconds(1);
            camAudioSource.Stop();
            canvasController.gabenHPBar.enabled = false;
            LevelManager.Instance.hasUnloaded = false;
            LevelManager.Instance.hasWon = true;
            LevelManager.Instance.enemies[LevelManager.Instance.theEnemy].hasLoaded = false;
            LevelManager.Instance.inCombat = false;
        }
        IEnumerator WinBossBattle()
        {
            print("WinCombat");
            state = GameStates.End;
            camAudioSource.PlayOneShot(victorySFX);
            actionDesc = "You Won!";
            EnemyManager.Instance.activatedCombatEnemies.ForEach(delegate (EnemyCombatController enemy)
            {
                enemy.ScaleXPWithLevel();
            });
            yield return new WaitForSeconds(2);
            // Uh load the scene before this
            fade.FadeOut();
            yield return new WaitForSeconds(1);
            //boots u to win screen
            SceneManager.LoadScene("Win");
        }
        IEnumerator LoseBossBattle()
        {
            state = GameStates.End;
            camAudioSource.PlayOneShot(lossSFX);
            actionDesc = "You Lost!";
            yield return new WaitForSeconds(2);
            // Uh load the scene before this
            fade.FadeOut();
            yield return new WaitForSeconds(1);
            camAudioSource.Stop();
            //boots u to lose scene
            SceneManager.LoadScene("Lose");
        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(1f);
        }
    } 
}
