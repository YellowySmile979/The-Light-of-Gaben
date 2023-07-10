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
    public Image result1, result2, result3;
    public Text desc1, desc2, desc3;
    private List<SmoloursData> rolled;
    SmolourGallery smolour;
    SmoloursData rolledSmolour;
    public Button RollButton;
    public List<SmoloursData> RSmolours, SRSmolours, SSRSmolours;

    public GameObject SelectScreen, Instructions1Screen, Instructions2Screen;

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
        
        // auto sorts Smolours into rarity.
        // These tables are called by GachaSystem to determine which Smolour the player receives.
        foreach (SmoloursData smolour in smolour.Smolours)
        {
            if (smolour.rarity == SmoloursData.Rarity.SSR) SSRSmolours.Add(smolour);
            else if (smolour.rarity == SmoloursData.Rarity.SR) SRSmolours.Add(smolour);
            else RSmolours.Add(smolour);
        }
    }

    public void Close() { gameObject.SetActive(false); }
    public void Roll()
    {
        rolled = new List<SmoloursData>();
        // Rolls 3 times
        for (int i = 1; i < 4; i++)
        {
            roll = Random.Range(0, 1001);

            //Once roll is calculated, an if else statement
            // is used to call the loot table according to the roll
            ; if (roll == 1000 || pity == 80)
            {
                int rolledSmolourIndex = Random.Range(0, SSRSmolours.Count); // rolls on Rare Smolours Table for the Smolour Rolled.
                rolledSmolour = SSRSmolours[rolledSmolourIndex]; // Finds the Smolour rolled
                pity = 0;
            }
            else if (roll > 901)
            {
                pity += 1; //Increases Pity
                int rolledSmolourIndex = Random.Range(0, SRSmolours.Count); // rolls on Rare Smolours Table for the Smolour Rolled.
                rolledSmolour = SRSmolours[rolledSmolourIndex]; // Fidns the Smolour rolled
            }
            else
            {
                pity += 1; //Increases Pity
                int rolledSmolourIndex = Random.Range(0, RSmolours.Count); // rolls on Rare Smolours Table for the Smolour Rolled.
                rolledSmolour = RSmolours[rolledSmolourIndex]; // Fidns the Smolour rolled
            }

            rolled.Add(rolledSmolour);

            // if the rolled smolour has already been collected, it does not get added to the collected Smolours array
            bool alreadyCollected = false;
            foreach (SmoloursData j in smolour.collectedSmolours)
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
        Text[] descriptions = new Text[] { desc1, desc2, desc3 };
        for (int i = 0; i < 3; i++)
        {
            
            descriptions[i].text = rolled[i].description;
            results[i].sprite = rolled[i].known;
        }
    }

    public void Gallery() { smolour.Open(); }
    public void Select() { SelectScreen.SetActive(true); }

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
