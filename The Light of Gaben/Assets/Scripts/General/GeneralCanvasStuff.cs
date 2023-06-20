using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralCanvasStuff : MonoBehaviour
{
    public Text levelText;

    public static GeneralCanvasStuff Instance;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateLevelText(float level)
    {
        levelText.text = "Level: " + level;
    }
}
