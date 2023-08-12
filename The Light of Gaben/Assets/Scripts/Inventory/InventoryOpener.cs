using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryOpener : MonoBehaviour
{
    [Header("Inventory Stuff")]
    public GameObject[] inventorySlots;
    [HideInInspector] public bool onOrOff = false;
    InventoryManager inventoryManager;
    public int chosenSlot;

    [Header("Other UI")]
    public GameObject mapButton;
    public Text floorText, levelText;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }
    //opens/closes the inventory and turns on/off the items
    public void TurnOnObjects()
    {
        if(!onOrOff)
        {
            //sets UI to be turned on
            mapButton.SetActive(true);
            floorText.color = new Color(1, 1, 1, 1);
            levelText.color = new Color(1, 1, 1, 1);

            for (int i = 0; i < inventorySlots.Length; i++)
            {
                Image image = inventorySlots[i].GetComponent<Image>();
                if (image != null)
                {
                    image.enabled = true;
                }
            }
            if (inventorySlots.Length == 9 && !onOrOff) onOrOff = true;            
        }
        else
        {
            //sets UI to be turned off
            mapButton.SetActive(false);
            floorText.color = new Color(1, 1, 1, 0);
            levelText.color = new Color(1, 1, 1, 0);

            for (int i = 0; i < inventorySlots.Length; i++)
            {
                Image image = inventorySlots[i].GetComponent<Image>();
                if (image != null)
                {
                    image.enabled = false;
                }              
            }
            if (inventorySlots.Length == 9 && onOrOff) onOrOff = false;
            inventoryManager.HideItem();
        }        
    }
    /*public void TurnOffObjects()
{
   for (int i = 0; i < inventorySlots.Length; i++)
   {
       Image image = inventorySlots[i].GetComponent<Image>();
       if (image != null)
       {
           image.enabled = false;
       }
   }
}*/
}
