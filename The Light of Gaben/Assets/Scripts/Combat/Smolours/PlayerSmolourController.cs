using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmolourController : MonoBehaviour
{
    public static PlayerSmolourController Instance;
    public List<SmoloursData> smolourBuffsSelected;

    public string hpPlus, atkPlus, defPlus, spPlus, critPlus, shieldPlus, redPlus, bluePlus, yellowPlus, orangePlus, greenPlus, magentaPlus;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void UpdateComb()
    {
        float combHP = 0, 
            combATK = 0, 
            combDEF = 0, 
            combSP = 0, 
            combCRIT = 0,
            combSHIELD = 0,
            combred = 0, 
            combblue = 0,
            combyellow = 0,
            comborange = 0,
            combgreen = 0,
            combmagenta = 0;

        foreach (SmoloursData smoloursData in smolourBuffsSelected)
        {
            combHP += smoloursData.hpBonus;
            combATK += smoloursData.attackBonus;
            combDEF += smoloursData.defenseBonus;
            combSP += smoloursData.speedBonus;
            combCRIT += smoloursData.critBonus;
            combSHIELD += smoloursData.shield;
            combred += smoloursData.redMultiplier;
            combblue += smoloursData.blueMultiplier;
            combyellow += smoloursData.yellowMultiplier;
            comborange += smoloursData.orangeMultiplier;
            combgreen += smoloursData.greenMultiplier;
            combmagenta += smoloursData.magentaMultiplier;
        }

        hpPlus = combHP.ToString();
        atkPlus = combATK.ToString();
        defPlus = combDEF.ToString();
        spPlus = combSP.ToString();
        critPlus = combCRIT.ToString();
        shieldPlus = combSHIELD.ToString();
        redPlus = combred.ToString();
        bluePlus = combblue.ToString();
        yellowPlus = combyellow.ToString();
        orangePlus = comborange.ToString();
        greenPlus = combgreen.ToString();
        magentaPlus = combmagenta.ToString();

        Instance = this;
        Instance.smolourBuffsSelected = smolourBuffsSelected;
    }
}
