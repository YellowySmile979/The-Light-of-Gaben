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
    bool hasUnloaded;

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
        Instantiate(lightShardToSpawn, enemies[theEnemy].transform.position + new Vector3(2, 2, 0), Quaternion.identity);
    }
    //gets the index of the enemy that we r fighting
    public void DefeatedEnemy(BaseEnemy thisEnemy)
    {
        if (enemies.Contains(BaseEnemy.instance) == thisEnemy)
        {
            hasAddedIndex = false;
            print(thisEnemy);
            theEnemy = enemies.IndexOf(thisEnemy);
        }
    }
    //checks to see if combat has ended
    //if yes, unload the combat scene, play exploration music and
    //if player wins, kill the enemy and remove it from the list. otherwise, reset the enemy
    void CheckToSeeIfCombatHasEnded()
    {
        if (!hasAddedIndex)
        {
            if (enemies[theEnemy].hasLoaded == false)
            {
                if (!hasUnloaded)
                {
                    SceneManager.UnloadSceneAsync(sceneToUnload);
                }
                hasUnloaded = true;
                if (!hasPlayed)
                {
                    PlayExplorationMusic();
                    hasPlayed = true;
                }
                if (hasWon)
                {
                    enemies[theEnemy].explorationCanvas.enabled = true;
                    Destroy(enemies[theEnemy].gameObject);
                    enemies.RemoveAt(theEnemy);
                    hasAddedIndex = true;
                }
                else
                {
                    enemies[theEnemy].explorationCanvas.enabled = true;
                    enemies[theEnemy].moveSpeed = enemies[theEnemy].originalMoveSpeed;
                    enemies[theEnemy].turnSpeed = enemies[theEnemy].originalTurnSpeed;
                    enemies[theEnemy].explorationCanvas.enabled = true;
                    hasAddedIndex = true;
                    SceneManager.LoadScene(loseScene);
                }
            }
        }
    }
}
