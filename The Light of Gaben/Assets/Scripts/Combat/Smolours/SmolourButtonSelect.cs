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
    public PlayerCombatController player;
    bool selected;

    public void Pressed()
    {
        if (selected)
        {
            selectSmolours.Deselect(smoloursData);
            selected = !selected; //inverts select, in this case to false;
        }
        else
        {
            selectSmolours.SelectSmolour(smoloursData);
            selected = !selected;
        }
    }

    private void Update()
    {
        if (player.smolourBuffs.Count == 6 && !selected) //if there are already 6 buffs selected and current button is not selected,
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
