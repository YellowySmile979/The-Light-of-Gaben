using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{   
    public bool hasWon;
    public AudioClip explorationMusic1, confirmSFX;
    public AudioSource camExplorationAudioSource;
    public bool hasPlayed;
    public string currentScene;
    public GameObject player;
    public bool hasLoaded;
    public GameObject lightShardToSpawn;
    bool hasUnloaded;

    public List<BaseEnemy> enemies = new List<BaseEnemy>();
    public static LevelManager Instance;    

    void Awake()
    {        
        PlayExplorationMusic();
        DontDestroyOnLoad(player);
        if(Instance == null)
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
        print("Spawn Light Shard");
        Instantiate(lightShardToSpawn, BaseEnemy.instance.transform.position, Quaternion.identity);
    }
    void CheckToSeeIfCombatHasEnded()
    {        
        if (BaseEnemy.instance.hasLoaded == false)
        {
            if(!hasUnloaded)
            {
                SceneManager.UnloadSceneAsync(BaseEnemy.instance.combatScene);
            }
            hasUnloaded = true; 
            if (!hasPlayed)
            {
                PlayExplorationMusic();
                hasPlayed = true;
            }
            if (hasWon)
            {                
                BaseEnemy.instance.explorationCanvas.enabled = true;
                Destroy(BaseEnemy.instance.gameObject);
            }
            else
            {
                BaseEnemy.instance.explorationCanvas.enabled = true;
                BaseEnemy.instance.moveSpeed = BaseEnemy.instance.originalMoveSpeed;
                BaseEnemy.instance.turnSpeed = BaseEnemy.instance.originalTurnSpeed;
                BaseEnemy.instance.explorationCanvas.enabled = true;
            }
        }
    }
}
