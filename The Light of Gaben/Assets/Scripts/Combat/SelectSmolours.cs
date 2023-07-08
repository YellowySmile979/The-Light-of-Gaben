using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSmolours : MonoBehaviour
{
    public GameObject buttonPrefab, buttonParent;
    SmolourGallery gallery;
    PlayerCombatController player;

    private void Update()
    {
        for (int i = 0; i < gallery.collectedSmolours.Count; i++)
        {
            GameObject newbutton = Instantiate(buttonPrefab, buttonParent.transform);
        }
    }

    public void SelectSmolour(SmoloursData smolour)
    {
        player.smolourBuffs.Add(smolour);
    }

    public void Deselect(SmoloursData smolour)
    {
        player.smolourBuffs.Remove(smolour);
    }
}
