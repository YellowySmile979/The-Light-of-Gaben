using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager Instance;

    [Header("Scenes")]
    //please input ur selected scene names
    public List<string> sceneNames = new List<string>();
    public string loseScene;
    [HideInInspector] public bool deactivateTimer = false;
    int randomNumber;
    float timer, initialTime = 10f;

    [Header("Win Conditions")]
    [TextArea]
    public string info = "Do not delete or modify this. 0 is minKillRequirement, 1 is timer, 2 is itemsToCollect.";
    //REMINDER: please set minKillRequirement to less than the enemy count
    //i.e. if you want the player to kill 6 enemies and there are 10 in total, minKillRequirement = 4
    public float minKillRequirement, timeLeftBeforeLose, numberOfWinItemsToCollect; 
    float numberOfWinItemsCollected = 0;
    [SerializeField] int initialNumberOfEnemies;
    public int chosenWinCondtion = 0;
    public bool canWin = false;
    public List<GameObject> baseEnemies = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        canWin = false;
        randomNumber = Random.Range(0, sceneNames.Count - 1);
        timer = initialTime;        
    }

    // Update is called once per frame
    void Update()
    {
        initialNumberOfEnemies = baseEnemies.Count;
        if (!deactivateTimer) Timer();
        if (chosenWinCondtion == 0) WinConditionMinKill();
        else if (chosenWinCondtion == 1) WinConditionTimer();
        else if (chosenWinCondtion == 2) WinConditionCollectItems();
    }
    //handles the randomisation to ensure it isnt just predetermined at start and will randomise
    void Timer()
    {
        if (timer <= 0)
        {
            randomNumber = Random.Range(0, sceneNames.Count - 1);
            LevelEndGoal.Instance.Win(randomNumber);
            timer = initialTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    void WinConditionMinKill()
    {
        //min kill requirement
        //the count of enemies there
        if(minKillRequirement >= initialNumberOfEnemies)
        {
            canWin = true;
        }
    }
    void WinConditionTimer()
    {
        //timer to complete level
        if (timeLeftBeforeLose <= 0)
        {
            canWin = false;
            SceneManager.LoadScene(loseScene);
        }
        else
        {
            canWin = true;
            timeLeftBeforeLose -= Time.deltaTime;
        }
    }
    public void WinConditionCollectItems(float collectedItems = 0)
    {
        //thing(s) to get
        numberOfWinItemsCollected += collectedItems;
        if (numberOfWinItemsCollected == numberOfWinItemsToCollect)
        {
            canWin = true;
        }        
    }
}
