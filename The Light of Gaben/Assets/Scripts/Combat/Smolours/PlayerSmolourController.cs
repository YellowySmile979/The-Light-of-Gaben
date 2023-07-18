using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmolourController : MonoBehaviour
{
    public static PlayerSmolourController Instance;
    public GameObject gachaScreen;
    public List<SmoloursData> smolourBuffsSelected;

    public float hpPlus, atkPlus, defPlus, spPlus, critPlus, shieldPlus, redPlus, bluePlus, yellowPlus, orangePlus, greenPlus, magentaPlus;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        } else { Instance = this; }
        DontDestroyOnLoad(gameObject);
    }

    public void OpenGacha() { gachaScreen.SetActive(true); }
    public void CloseGacha() { gachaScreen.SetActive(false); }

    public void UpdateComb()
    {
        float combHP = 0, 
            combATK = 0, 
            combDEF = 0, 
            combSP = 0, 
            combCRIT = 0,
            combSHIELD = 0,
            combred = 1, 
            combblue = 1,
            combyellow = 1,
            comborange = 1,
            combgreen = 1,
            combmagenta = 1;

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

        hpPlus = combHP;
        atkPlus = combATK;
        defPlus = combDEF;
        spPlus = combSP;
        critPlus = combCRIT;
        shieldPlus = combSHIELD;
        redPlus = combred;
        bluePlus = combblue;
        yellowPlus = combyellow;
        orangePlus = comborange;
        greenPlus = combgreen;
        magentaPlus = combmagenta;
        Instance = this;
    }
}
