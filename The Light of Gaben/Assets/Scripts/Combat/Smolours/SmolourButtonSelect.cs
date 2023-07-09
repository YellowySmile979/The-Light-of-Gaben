using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmolourButtonSelect : MonoBehaviour
{
    public Text buttonText;
    public Image displayed;
    public SmoloursData smoloursData;
    SelectSmolours selectSmolours;
    bool selected;


    public void Pressed()
    {
        if (selected)
        {
            selectSmolours.Deselect(smoloursData);
            selected = !selected;
        }
        else
        {
            selectSmolours.SelectSmolour(smoloursData);
            selected = !selected;
        }
    }

    private void Update()
    {
    }
}
