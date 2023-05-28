using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public bool hasWon;
    public AudioClip explorationMusic1;
    public AudioSource camExplorationAudioSource;
    public bool hasPlayed;
    public string currentScene;
    public GameObject player;
    public bool hasLoaded;

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
        camExplorationAudioSource.PlayOneShot(explorationMusic1);
    }
    void CheckToSeeIfCombatHasEnded()
    {        
        if (BaseEnemy.instance.hasLoaded == false)
        {
            SceneManager.UnloadSceneAsync(BaseEnemy.instance.combatScene);
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
