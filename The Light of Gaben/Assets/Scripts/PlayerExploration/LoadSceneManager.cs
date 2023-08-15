using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager Instance;

    [Header("Scenes")]
    //please input ur selected scene names FOR EASY
    public List<string> easySceneNames = new List<string>();
    public List<string> mediumSceneNames = new List<string>();
    public List<string> hardSceneNames = new List<string>();

    public string loseScene;
    [HideInInspector] public bool deactivateTimer = false;
    public int easyRandomNumber, mediumRandomNumber, hardRandomNumber;
    float timer, initialTime = 10f;

    public Scene currentScene;

    [Header("Win Conditions")]
    [TextArea]
    public string info = "Do not delete or modify this. 0 is minKillRequirement, 1 is timer, 2 is itemsToCollect.";
    //REMINDER: please set minKillRequirement to less than the enemy count
    //i.e. if you want the player to kill 6 enemies and there are 10 in total, minKillRequirement = 4
    public float minKillRequirement, timeLeftBeforeLose, numberOfWinItemsToCollect; 
    float numberOfWinItemsCollected = 0;
    [SerializeField] int numberOfEnemies, enemiesKilled;
    public int chosenWinCondtion = 0;
    public bool canWin = false;
    public List<GameObject> baseEnemies = new List<GameObject>();
    bool printed = false;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //sets the currentscene variable to this scene
        currentScene = SceneManager.GetActiveScene();
        //checks to see if the name is found in any of the following and removes it if there is
        if (easySceneNames.Contains(currentScene.name))
        {
            easySceneNames.Remove(currentScene.name);
        }
        if (mediumSceneNames.Contains(currentScene.name))
        {
            mediumSceneNames.Remove(currentScene.name);
        }
        if (hardSceneNames.Contains(currentScene.name))
        {
            hardSceneNames.Remove(currentScene.name);
        }

        canWin = false;
        easyRandomNumber = Random.Range(0, easySceneNames.Count - 1);
        mediumRandomNumber = Random.Range(0, mediumSceneNames.Count - 1);
        hardRandomNumber = Random.Range(0, hardSceneNames.Count - 1);
        timer = initialTime;        
    }

    // Update is called once per frame
    void Update()
    {
        // quick fix for runtime error.
        // if in Start scene, doesn't check for all of these
        if (currentScene.name == "Start") return;

        numberOfEnemies = baseEnemies.Count;
        //helps prevent bugs
        if (!deactivateTimer) Timer();
        //handles which win condition we want
        if (chosenWinCondtion == 0) WinConditionMinKill();
        else if (chosenWinCondtion == 1) WinConditionTimer();
        else if (chosenWinCondtion == 2) WinConditionCollectItems();
    }
    //handles the randomisation to ensure it isnt just predetermined at start and will randomise
    void Timer()
    {
        if (timer <= 0)
        {
            print("Timer() is firing" + timer);
            if (PlayerPrefs.GetInt("Floor Number") <= 5)
            {
                easyRandomNumber = Random.Range(0, easySceneNames.Count - 1);
                LevelEndGoal.Instance.Win(easyRandomNumber);
            }
            else if(PlayerPrefs.GetInt("Floor Number") <= 10 && PlayerPrefs.GetInt("Floor Number") > 5)
            {
                mediumRandomNumber = Random.Range(0, mediumSceneNames.Count - 1);
                LevelEndGoal.Instance.Win(mediumRandomNumber);
            }
            else if(PlayerPrefs.GetInt("Floor Number") <= 14 && PlayerPrefs.GetInt("Floor Number") > 10)
            {
                hardRandomNumber = Random.Range(0, hardSceneNames.Count - 1);
                LevelEndGoal.Instance.Win(hardRandomNumber);
            }
            timer = initialTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    void WinConditionMinKill()
    {
        if (!printed)
        {
            print("Using Min Kill");
            printed = true;
        }
        //updates UI to show min number of kills needed
        string winConditionMinKill = "Minimum amount of kills required: " + minKillRequirement.ToString();
        GeneralCanvasStuff.Instance.UpdateWinConditionText(winConditionMinKill);
        //min kill requirement
        //the count of enemies there
        if(minKillRequirement <= enemiesKilled)
        {
            print("Min kill reached");
            canWin = true;
            string winConditionMet = "You may proceed to the next level.";
            GeneralCanvasStuff.Instance.UpdateWinConditionText(winConditionMet);
        }
    }
    public void EnemiesKilled(int killed)
    {
        enemiesKilled += killed;
    }
    void WinConditionTimer()
    {
        if (!printed)
        {
            print("Using Timer");
            printed = true;
        }
        //timer to complete level
        if (timeLeftBeforeLose <= 0)
        {
            StartCoroutine(WaitToLose());
        }
        else
        {
            canWin = true;
            //decreases timer
            timeLeftBeforeLose -= Time.deltaTime;
            //updates UI to amount of time left
            string winConditionTimer = "Beat this time: " + Mathf.Round(timeLeftBeforeLose).ToString() + "seconds";
            GeneralCanvasStuff.Instance.UpdateWinConditionText(winConditionTimer);
        }
    }
    //allows for text to be displayed before you lose
    IEnumerator WaitToLose()
    {
        //you lost lmao
        string winConditionMet = "You have run out of time.";
        GeneralCanvasStuff.Instance.UpdateWinConditionText(winConditionMet);
        yield return new WaitForSeconds(2f);
        canWin = false;
        GeneralCanvasStuff.Instance.GameOver();
    }
    public void WinConditionCollectItems(float collectedItems = 0)
    {
        if (!printed)
        {
            print("Using Collect Items");
            printed = true;
        }
        //updates UI to this specific instance of the win condition
        string winConditionCollectItems = "Items collected/Items to collect (Note that some items DO NOT count): " 
                                        + numberOfWinItemsCollected 
                                        + "/" 
                                        + numberOfWinItemsToCollect;
        GeneralCanvasStuff.Instance.UpdateWinConditionText(winConditionCollectItems);
        //item(s) to get
        numberOfWinItemsCollected += collectedItems;
        if (numberOfWinItemsCollected == numberOfWinItemsToCollect)
        {
            print("Collected all items");
            //updates UI to say that the win condition has been met
            string winConditionMet = "You have collected all the items.";
            GeneralCanvasStuff.Instance.UpdateWinConditionText(winConditionMet);
            canWin = true;
        }        
    }
}
