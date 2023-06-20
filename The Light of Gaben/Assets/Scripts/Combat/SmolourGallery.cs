using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmolourGallery : MonoBehaviour
{
    public List<SmolourCombatController> collectedSmolours;

    public Sprite known;

    public SmolourCombatController[] Smolours;
    public Text[] Texts;
    public Image[] Images;
    public List<SmolourCombatController> RSmolours;
    public List<SmolourCombatController> SRSmolours;
    public List<SmolourCombatController> SSRSmolours;

    private void Start()
    {
        foreach (SmolourCombatController smolour in Smolours)
        {
            if (smolour.rarity == SmolourCombatController.Rarity.SSR) SSRSmolours.Add(smolour);
            else if (smolour.rarity == SmolourCombatController.Rarity.SR) SRSmolours.Add(smolour);
            else RSmolours.Add(smolour);
        }
    }


    private void Awake()
    {
        foreach (SmolourCombatController smolour in collectedSmolours)
        {
            // searches for the index of each collected smolour in the Smolour array, and changes the corresponding Image and text.
            int index = 0;
            for (; index < Smolours.Length; index++)
            {
                if (Smolours[index] == smolour) break;
            }

            Images[index].sprite = known;
            Texts[index].text = smolour.description;
            if (smolour.rarity == SmolourCombatController.Rarity.SSR) Images[index].color = Color.yellow;
            else if (smolour.rarity == SmolourCombatController.Rarity.SR) Images[index].color = Color.green;
            else Images[index].color = Color.blue;
        }
    }
}
