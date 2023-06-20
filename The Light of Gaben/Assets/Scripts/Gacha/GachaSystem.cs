using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaSystem : MonoBehaviour
{
    private int roll;
    public int pity = 0;
    public int amontToPull = 50;
    public Text pityCounter;
    public Sprite smolourSprite1, smolourSprite2;
    public Image result1, result2, result3;
    private List<SmolourCombatController> rolled;
    SmolourGallery smolour;
    SmolourCombatController rolledSmolour;
    public Button RollButton;

    //The Gacha System
    // Current drop rates are:
    // SSR++* : 1/1000 = 0.01 % (Not taking into account Pity)
    // SR : 99 / 1000 = 0.99 %
    // R : 900/1000 = 90 %
    private void Start()
    {
        // If you have less than 50 PP, will not let you interact
        if ((int)CurrencyData.Type.PP < amontToPull) RollButton.interactable = false;
        else RollButton.interactable = true;
        smolour = FindObjectOfType<SmolourGallery>();
    }
    public void Roll()
    {
        rolled = new List<SmolourCombatController>();
        print("Start Rolls");
        // Rolls 3 times
        for (int i = 1; i < 4; i++)
        {
            roll = Random.Range(1, 1001);

            //Once roll is calculated, an if else statement
            // is used to call the loot table according to the roll
            ; if (roll == 1000 || pity == 80)
            {
                int rolledSmolourIndex = Random.Range(1, smolour.SSRSmolours.Length); // rolls on Rare Smolours Table for the Smolour Rolled.
                rolledSmolour = smolour.SSRSmolours[rolledSmolourIndex]; // Fidns the Smolour rolled
            }
            else if (roll > 901)
            {
                pity += 1; //Increases Pity
                int rolledSmolourIndex = Random.Range(1, smolour.SRSmolours.Length); // rolls on Rare Smolours Table for the Smolour Rolled.
                rolledSmolour = smolour.SRSmolours[rolledSmolourIndex]; // Fidns the Smolour rolled
            }
            else
            {
                pity += 1; //Increases Pity
                int rolledSmolourIndex = Random.Range(1, smolour.RSmolours.Length); // rolls on Rare Smolours Table for the Smolour Rolled.
                rolledSmolour = smolour.RSmolours[rolledSmolourIndex]; // Fidns the Smolour rolled
            }
            

            // if the rolled smolour has already been collected, it does not get added to the collected Smolours array
            bool alreadyCollected = false;
            foreach (SmolourCombatController j in smolour.collectedSmolours)
            {
                if (j == rolledSmolour) alreadyCollected = true;
            }
            if (!alreadyCollected) smolour.collectedSmolours.Add(rolledSmolour);
        }
        pityCounter.text = "Pity: " + pity;
        Results();
    }
    //randomises which place the appropriate smolour should spawn in
    void Results()
    {
        Image[] results = new Image[] { result1, result2, result3 };
        for (int i = 0; i < 3; i++)
        {
            if (rolled[i].rarity == SmolourCombatController.Rarity.SSR) results[i].color = Color.yellow;
            else if (rolled[i].rarity == SmolourCombatController.Rarity.SR) results[i].color = Color.green;
            else results[i].color = Color.blue;

            int smolourRandom = Random.Range(1, 3);
            print(smolourRandom);
            if (smolourRandom == 1) results[i].sprite = smolourSprite1;
            else results[i].sprite = smolourSprite2;
        }
    }

/*    void RollSSRTable()
    {
        print("Rolled an SSR!");
        pity = 0;
    }

    void RollSRTable()
    {
        print("Rolled an SR!");
        pity += 1;
    }

    void RollRTable()
    {
        print("Rolled an R!");
        pity += 1; //Increases Pity

        int rolledSmolourIndex = Random.Range(1, smolour.RSmolours.Count); // rolls on Rare Smolours Table for the Smolour Rolled.
        SmolourCombatController rolledSmolour = smolour.RSmolours[rolledSmolourIndex]; // Fidns the Smolour rolled

        // if the rolled smolour has already been collected, it does not get added to the collected Smolours array
        bool alreadyCollected = false;
        foreach (SmolourCombatController i in smolour.collectedSmolours)
        {
            if (i == rolledSmolour) alreadyCollected = true;
        }
        if (!alreadyCollected) smolour.collectedSmolours.Add(rolledSmolour);
    }*/
}
