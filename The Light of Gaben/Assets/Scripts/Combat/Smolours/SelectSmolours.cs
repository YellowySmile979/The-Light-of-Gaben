using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSmolours : MonoBehaviour
{
    public GameObject buttonPrefab, buttonParent;
    public Text playerStats, finalStats;
    SmolourGallery gallery;
    SmolourCombatController smolourController;
    public int buttonCount = 0;

    public void Close()
    { 
        gameObject.SetActive(false);
    }

    private void Start()
    {
        gallery = FindObjectOfType<SmolourGallery>();
        smolourController = FindObjectOfType<SmolourCombatController>();
        Debug.Log("SelectScreenStart");
        Close();
    }

    private void Update()
    {
        if (gallery.collectedSmolours.Count != buttonCount)
        {
            Debug.Log("Created Button");
            SmoloursData smolour = gallery.collectedSmolours[buttonCount];
            GameObject newbutton = Instantiate(buttonPrefab, buttonParent.transform);
            newbutton.GetComponent<SmolourButtonSelect>().displayed.sprite = smolour.known;
            newbutton.GetComponent<SmolourButtonSelect>().buttonText.text = smolour.description;
            newbutton.GetComponent<SmolourButtonSelect>().smoloursData = smolour;
            newbutton.GetComponent<SmolourButtonSelect>().selectSmolours = this;
            newbutton.GetComponent<SmolourButtonSelect>().player = player;
            newbutton.GetComponent<SmolourButtonSelect>().button = newbutton.GetComponent<Button>();
            buttonCount += 1;
            //UpdateStats();
        }
    }

    public void SelectSmolour(SmoloursData smolour)
    {
        Debug.Log("Added smolour to smolour buffs");
        //player.smolourBuffs.Add(smolour);
        UpdateStats();
    }

    public void Deselect(SmoloursData smolour)
    {
        //player.smolourBuffs.Remove(smolour);
        UpdateStats();
    }

    public void UpdateStats()
    {
        // Converting Player Stats to String
        playerStats.text = "0";
    }
}
