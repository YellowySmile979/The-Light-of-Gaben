using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmolourButtonSelect : MonoBehaviour
{
    public Text buttonText;
    public Image displayed;
    public Button button;
    public SmoloursData smoloursData;
    public SelectSmolours selectSmolours;
    public GameObject checkmark;
    PlayerSmolourController smolourController;
    bool selected;

    public void Start()
    {
        smolourController = FindObjectOfType<PlayerSmolourController>();
    }

    public void Pressed()
    {
        if (selected)
        {
            selectSmolours.Deselect(smoloursData);
            checkmark.SetActive(false);
            selected = !selected; //inverts select, in this case to false;
        }
        else
        {
            selectSmolours.SelectSmolour(smoloursData);
            checkmark.SetActive(true); 
            selected = !selected;
        }
    }

    private void Update()
    {
        if (smolourController.smolourBuffsSelected.Count == 6 && !selected) //if there are already 6 buffs selected and current button is not selected,
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
