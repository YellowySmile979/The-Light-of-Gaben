using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSmolours : MonoBehaviour
{
    public GameObject buttonPrefab, buttonParent;
    public Text playerStats, finalStats;
    SmolourGallery gallery;
    public PlayerCombatController player;
    public GameObject playerPrefab;

    public void Close() { gameObject.SetActive(false); }

    private void Start()
    {
        Debug.Log("SelectScreenStart");
        player = FindObjectOfType<PlayerCombatController>();
        if (player = null)
        {
            GameObject tempplayer = Instantiate(playerPrefab, buttonParent.transform);
            player = tempplayer.GetComponent<PlayerCombatController>();
        }
        Close();
    }

    private void OnEnable()
    {
        Debug.Log("Select Screen Awake.");
        gallery = FindObjectOfType<SmolourGallery>();

        for (int i = 0; i < gallery.collectedSmolours.Count; i++)
        {
            Debug.Log("Created Button");
            SmoloursData smolour = gallery.collectedSmolours[i];
            GameObject newbutton = Instantiate(buttonPrefab, buttonParent.transform);
            newbutton.GetComponent<SmolourButtonSelect>().displayed.sprite = smolour.known;
            newbutton.GetComponent<SmolourButtonSelect>().buttonText.text = smolour.description;
            newbutton.GetComponent<SmolourButtonSelect>().smoloursData = smolour;
        }
        //UpdateStats();
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
