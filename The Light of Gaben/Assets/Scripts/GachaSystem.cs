using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaSystem : MonoBehaviour
{
    private int roll;
    public int pity = 0;
    public Text pityCounter;
    public Sprite smolourSprite1, smolourSprite2;
    public Image result1, result2, result3;
    private List<string> rolled;


    //The Gacha System
    // Current drop rates are:
    // SSR++* : 1/1000 = 0.01 % (Not taking into account Pity)
    // SR : 99 / 1000 = 0.99 %
    // R : 900/1000 = 90 %

    public void Roll()
    {
        rolled = new List<string>();
        print("Start Rolls");
        // Rolls 3 times
        for (int i = 1; i <4; i++)
        {
            roll = Random.Range(1, 1001);
        
            //Once roll is calculated, an if else statement
            // is used to call the loot table according to the roll
;           if (roll == 1000 || pity == 80)
            {
                RollSSRTable();
                rolled.Add("SSR");
            }
            else if (roll > 901)
            {
                RollSRTable();
                rolled.Add("SR");
            }
            else
            { 
                RollRTable(); 
                rolled.Add("R"); 
            }
        }
        pityCounter.text = "Pity: " + pity;
        Results();
    }

    void Results()
    {
        print("Results() called");
        Image[] results = new Image[] { result1, result2, result3};

        for (int i = 0; i < 3; i++)
        {
            if (rolled[i] == "SSR") results[i].color = Color.yellow;
            else if (rolled[i] == "SR") results[i].color = Color.green;
            else results[i].color = Color.blue;

            int smolourRandom = Random.Range(1, 3);
            print(smolourRandom);
            if (smolourRandom == 1) results[i].sprite = smolourSprite1;
            else results[i].sprite = smolourSprite2;
        }
    }
    void RollSSRTable()
    {
        print("Rolled an SSR!");
        pity = 0;
        // We would have another roll here in each table to
        // randomise the loot that players get
    }

    void RollSRTable()
    {
        print("Rolled an SR!");
        pity += 1;
    }

    void RollRTable()
    {
        print("Rolled an R!");
        pity += 1;
    }
}
