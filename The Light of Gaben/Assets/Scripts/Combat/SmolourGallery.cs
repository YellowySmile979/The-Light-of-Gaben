using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmolourGallery : MonoBehaviour
{
    public List<SmolourCombatController> collectedSmolours;

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

    // Update is called once per frame
    private void Awake()
    {
        foreach (SmolourCombatController smolour in collectedSmolours)
        {
        }
    }
}
