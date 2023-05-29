using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryOpener : MonoBehaviour
{
    public GameObject[] inventorySlots;
    [HideInInspector] public bool onOrOff = false;
    InventoryManager inventoryManager;
    public int chosenSlot;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }
    public void TurnOnObjects()
    {
        if(!onOrOff)
        {
            LevelManager.Instance.camExplorationAudioSource.PlayOneShot(LevelManager.Instance.selectUiSFX, 0.5f);
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
            LevelManager.Instance.camExplorationAudioSource.PlayOneShot(LevelManager.Instance.selectUiSFX, 0.5f);
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
