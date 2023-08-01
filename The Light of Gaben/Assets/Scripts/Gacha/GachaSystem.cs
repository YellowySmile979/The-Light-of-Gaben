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
    public GameObject result1, result2, result3;
    public Text desc1, desc2, desc3;
    private List<SmoloursData> rolled;
    SmolourGallery smolour;
    SmoloursData rolledSmolour;
    public Button RollButton;
    public List<SmoloursData> RSmolours, SRSmolours, SSRSmolours;
    GachaWishingWell wishingWell;

    public GameObject SelectScreen, Instructions1Screen, Instructions2Screen;

    //The Gacha System
    // Current drop rates are:
    // SSR++* : 1/100 = 1 % (Not taking into account Pity)
    // SR : 10/100 = 10 %
    // R : 89/100 = 89 %
    private void Start()
    {
        smolour = FindObjectOfType<SmolourGallery>();
        wishingWell = FindObjectOfType<GachaWishingWell>();
        // If you have less than 1 PP, will not let you interact
        if (PlayerPrefs.GetInt("PP Count") < amontToPull) RollButton.interactable = false;
        else RollButton.interactable = true;
        // auto sorts Smolours into rarity.
        // These tables are called by GachaSystem to determine which Smolour the player receives.
        foreach (SmoloursData smolour in smolour.Smolours)
        {
            if (smolour.rarity == SmoloursData.Rarity.SSR) SSRSmolours.Add(smolour);
            else if (smolour.rarity == SmoloursData.Rarity.SR) SRSmolours.Add(smolour);
            else RSmolours.Add(smolour);
        }
        Close();
    }

    public void Close() { gameObject.SetActive(false); wishingWell.Close(); }
    public void Roll()
    {
        rolled = new List<SmoloursData>();
        // Rolls 3 times
        for (int i = 1; i < 4; i++)
        {
            roll = Random.Range(0, 101);

            //Once roll is calculated, an if else statement
            // is used to call the loot table according to the roll
            ; if (roll == 100 || pity == 50)
            {
                int rolledSmolourIndex = Random.Range(0, SSRSmolours.Count); // rolls on SSRare Smolours Table for the Smolour Rolled.
                rolledSmolour = SSRSmolours[rolledSmolourIndex]; // Finds the Smolour rolled
                pity = 0;
            }
            else if (roll >= 89)
            {
                pity += 1; //Increases Pity
                int rolledSmolourIndex = Random.Range(0, SRSmolours.Count); // rolls on SRare Smolours Table for the Smolour Rolled.
                rolledSmolour = SRSmolours[rolledSmolourIndex]; // Finds the Smolour rolled
            }
            else
            {
                pity += 1; //Increases Pity
                int rolledSmolourIndex = Random.Range(0, RSmolours.Count); // rolls on Rare Smolours Table for the Smolour Rolled.
                rolledSmolour = RSmolours[rolledSmolourIndex]; // Finds the Smolour rolled
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

        // Sets PP count to itself - amountToPull
        int minusPP = PlayerPrefs.GetInt("PP Count") - amontToPull;
        PlayerPrefs.SetInt("PP Count", minusPP);
        Results();

        // Updates how many smolours collected.
        PlayerPrefs.SetInt("Smolours Collected", smolour.collectedSmolours.Count);
    }
    //randomises which place the appropriate smolour should spawn in
    void Results()
    {
        GameObject[] results = new GameObject[] { result1, result2, result3 };
        Text[] descriptions = new Text[] { desc1, desc2, desc3 };
        for (int i = 0; i < 3; i++)
        {
            descriptions[i].text = rolled[i].description;
            results[i].GetComponent<Image>().sprite = rolled[i].known;
            results[i].GetComponent<GachaResultsAnimationController>().Pulled();
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
