using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Combat")]
    public bool hasWon;
    public bool hasPlayed;
    public string currentScene, sceneToUnload, loseScene;
    public GameObject player;
    public bool hasLoaded;
    public GameObject lightShardToSpawn;
    [HideInInspector] public bool hasUnloaded;

    [Header("Audio")]
    public AudioSource camExplorationAudioSource;
    public AudioClip explorationMusic1, confirmSFX, selectUiSFX, itemDenySFX, itemUseSFX, pickUpItemSFX;

    [Header("Enemies")]
    public List<BaseEnemy> enemies = new List<BaseEnemy>();
    [HideInInspector] public int theEnemy;
    public bool inCombat;
    bool hasAddedIndex;
    public static LevelManager Instance;

    [Header("UI")]
    public GameObject gachaScreen;

    void Awake()
    {
        PlayExplorationMusic();
        DontDestroyOnLoad(player);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        enemies.AddRange(FindObjectsOfType<BaseEnemy>());
        camExplorationAudioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckToSeeIfCombatHasEnded();
    }
    public void TurnOffUiElement()
    {
        Time.timeScale = 1;
        gachaScreen.SetActive(false);
    }
    public void StopMusic()
    {
        camExplorationAudioSource.Stop();
    }
    void PlayExplorationMusic()
    {
        camExplorationAudioSource.PlayOneShot(explorationMusic1, 0.1f);
    }
    public void SpawnLightShard()
    {
        print("theEnemy LS: " + theEnemy);
        Instantiate(lightShardToSpawn, enemies[theEnemy].transform.position + new Vector3(1, 1, 0), Quaternion.identity);
    }
    //gets the index of the enemy that we r fighting
    public void DefeatedEnemy(BaseEnemy thisEnemy)
    {
        //stores what enemy was selected
        BaseEnemy givenEnemy = thisEnemy;
        //checks the list to see if enemy exists and then chooses the corresponding enemy,
        //and then sets the theEnemy int to the corresponding enemy's index
        if (enemies.Contains(givenEnemy) == thisEnemy)
        {
            hasAddedIndex = false;
            print("thisEnemy" + thisEnemy);
            theEnemy = enemies.IndexOf(thisEnemy);
        }
    }
    //checks to see if combat has ended
    //if yes, unload the combat scene, play exploration music and
    //if player wins, kill the enemy and remove it from the list. otherwise, reset the enemy and load the specified scene
    void CheckToSeeIfCombatHasEnded()
    {
        if (!hasAddedIndex)
        {
            if (enemies[theEnemy].hasLoaded == false)
            {
                //unloads combat scene. the hasUnloaded bool is to prevent scene from closing itself multiple times
                if (!hasUnloaded)
                {
                    SceneManager.UnloadSceneAsync(enemies[theEnemy].combatScene);
                }
                hasUnloaded = true;
                //plays exploration music
                if (!hasPlayed)
                {
                    PlayExplorationMusic();
                    hasPlayed = true;
                }
                //checks to see if player has won or not
                if (hasWon)
                {
                    print("hasWon");
                    enemies[theEnemy].explorationCanvas.enabled = true;
                    SpawnLightShard();
                    Destroy(enemies[theEnemy].gameObject);
                    enemies.RemoveAt(theEnemy);
                    hasAddedIndex = true;
                }
                else
                {
                    print("hasntWon");
                    enemies[theEnemy].explorationCanvas.enabled = true;
                    enemies[theEnemy].moveSpeed = enemies[theEnemy].originalMoveSpeed;
                    enemies[theEnemy].turnSpeed = enemies[theEnemy].originalTurnSpeed;
                    enemies[theEnemy].explorationCanvas.enabled = true;
                    hasAddedIndex = true;
                    Destroy(PlayerMovement.Instance.gameObject);
                    SceneManager.LoadScene(loseScene);
                }
            }
        }
    }
}
