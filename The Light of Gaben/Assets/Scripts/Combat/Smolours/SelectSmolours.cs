using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSmolours : MonoBehaviour
{
    public GameObject buttonPrefab, buttonParent, selectScreen;
    public Text finalStats;
    SmolourGallery gallery;
    PlayerSmolourController smolourController;
    public int buttonCount = 0;

    public void Close()
    { 
        selectScreen.SetActive(false);
    }

    private void Start()
    {
        gallery = FindObjectOfType<SmolourGallery>();
        smolourController = FindObjectOfType<PlayerSmolourController>();
        Debug.Log("SelectScreenStart");
        Close();
    }

    private void Update()
    {
        if (gallery.collectedSmolours.Count != buttonCount)
        {
            SmoloursData smolour = gallery.collectedSmolours[buttonCount];
            GameObject newbutton = Instantiate(buttonPrefab, buttonParent.transform);
            newbutton.GetComponent<SmolourButtonSelect>().displayed.sprite = smolour.known;
            newbutton.GetComponent<SmolourButtonSelect>().buttonText.text = smolour.description;
            newbutton.GetComponent<SmolourButtonSelect>().smoloursData = smolour;
            newbutton.GetComponent<SmolourButtonSelect>().selectSmolours = this;
            newbutton.GetComponent<SmolourButtonSelect>().button = newbutton.GetComponent<Button>();
            buttonCount += 1;
            //UpdateStats();
        }
    }

    public void SelectSmolour(SmoloursData smolour)
    {
        Debug.Log("Added smolour to smolour buffs");
        //player.smolourBuffs.Add(smolour);
        smolourController.smolourBuffsSelected.Add(smolour);
        smolourController.UpdateComb();
        UpdateStats();
    }

    public void Deselect(SmoloursData smolour)
    {
        //player.smolourBuffs.Remove(smolour);
        smolourController.smolourBuffsSelected.Remove(smolour);
        smolourController.UpdateComb();
        UpdateStats();
    }

    public void UpdateStats()
    {
        finalStats.text =
            "+" + smolourController.hpPlus + "\n" +
            "+" + smolourController.atkPlus + "\n" +
            "+" + smolourController.defPlus + "\n" +
            "+" + smolourController.critPlus + "\n" +
            "+" + smolourController.spPlus + "\n" +
            "\n"+
            "x" + smolourController.redPlus + "\n" +
            "x" + smolourController.bluePlus + "\n" +
            "x" + smolourController.yellowPlus + "\n" +
            "x" + smolourController.orangePlus + "\n" +
            "x" + smolourController.orangePlus + "\n" +
            "x" + smolourController.magentaPlus + "\n";
    }
}
