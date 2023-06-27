using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] int numberOfEnemies, initialNumberOfEnemies;
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
        initialNumberOfEnemies = baseEnemies.Count - (int)minKillRequirement;
        timer = initialTime;        
    }

    // Update is called once per frame
    void Update()
    {
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
        //updates UI to show min number of kills needed
        string winConditionMinKill = "Minimum amount of kills required: " + initialNumberOfEnemies.ToString();
        GeneralCanvasStuff.Instance.UpdateWinConditionText(winConditionMinKill);
        //min kill requirement
        //the count of enemies there
        if(minKillRequirement >= numberOfEnemies)
        {
            canWin = true;
            string winConditionMet = "You may proceed to the next level.";
            GeneralCanvasStuff.Instance.UpdateWinConditionText(winConditionMet);
        }
    }
    void WinConditionTimer()
    {        
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
        //updates UI to this specific instance of the win condition
        string winConditionCollectItems = "Items to collect (Note that some items DO NOT count): " + numberOfWinItemsToCollect.ToString();
        GeneralCanvasStuff.Instance.UpdateWinConditionText(winConditionCollectItems);
        //item(s) to get
        numberOfWinItemsCollected += collectedItems;
        if (numberOfWinItemsCollected == numberOfWinItemsToCollect)
        {
            //updates UI to say that the win condition has been met
            string winConditionMet = "You have collected all the items.";
            GeneralCanvasStuff.Instance.UpdateWinConditionText(winConditionMet);
            canWin = true;
        }        
    }
}
