using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSmolours : MonoBehaviour
{
    public GameObject buttonPrefab, buttonParent;
    public Text playerStats;
    public Text smolourBuffs;
    SmolourGallery gallery;
    public PlayerCombatController player;

    public void Close() { gameObject.SetActive(false); }

    private void Start() { Close(); }
    private void OnEnable()
    {
        gallery = FindObjectOfType<SmolourGallery>();
        player = FindObjectOfType<PlayerCombatController>();
        if (player = null)
        {
            GameObject tempPlayer = Instantiate(gameObject);
            tempPlayer.GetComponent<PlayerCombatController>();
        }

        for (int i = 0; i < gallery.collectedSmolours.Count; i++)
        {
            SmoloursData smolour = gallery.collectedSmolours[i];
            GameObject newbutton = Instantiate(buttonPrefab, buttonParent.transform);
            newbutton.GetComponent<SmolourButtonSelect>().displayed.sprite = smolour.known;
            newbutton.GetComponent<SmolourButtonSelect>().buttonText.text = smolour.description;
            newbutton.GetComponent<SmolourButtonSelect>().smoloursData = smolour;
        }

        playerStats.text = player.attack.ToString();
    }

    public void SelectSmolour(SmoloursData smolour)
    {
        Debug.Log("Added smolour to smolour buffs");
        player.smolourBuffs.Add(smolour);
    }

    public void Deselect(SmoloursData smolour)
    {
        player.smolourBuffs.Remove(smolour);
    }
}
