using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSmolours : MonoBehaviour
{
    public GameObject buttonPrefab, buttonParent;
    SmolourGallery gallery;
    PlayerCombatController player;

    private void OnEnable()
    {
        for (int i = 0; i < gallery.collectedSmolours.Count; i++)
        {
            SmoloursData smolour = gallery.collectedSmolours[i];
            GameObject newbutton = Instantiate(buttonPrefab, buttonParent.transform);
            newbutton.GetComponent<SmolourButtonSelect>().displayed.sprite = smolour.known;
            newbutton.GetComponent<SmolourButtonSelect>().buttonText.text = smolour.description;
            newbutton.GetComponent<SmolourButtonSelect>().smoloursData = smolour;

            newbutton.GetComponent<Button>().onClick.AddListener(() => SelectSmolour(smolour));
        }
    }

    private void SelectSmolour(SmoloursData smolour)
    {
        Debug.Log("Added smolour to smolour buffs");
        player.smolourBuffs.Add(smolour);
    }

    public void Deselect(SmoloursData smolour)
    {
        player.smolourBuffs.Remove(smolour);
    }
}
