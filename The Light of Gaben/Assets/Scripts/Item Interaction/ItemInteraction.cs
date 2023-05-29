using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInteraction : MonoBehaviour, IDropHandler
{
    [SerializeField] BaseObjectInteraction[] baseObjectInteraction;    
    [SerializeField] GameObject correspondingItem;
    public GameObject objectToInteractWith;
    [HideInInspector] public bool hasDestroyed = false;
    [SerializeField] GameObject usedItem;
    InventoryManager inventoryManager;

    void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        baseObjectInteraction = FindObjectsOfType<BaseObjectInteraction>();
    }
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        for(int i = 0; i < baseObjectInteraction.Length; i++)
        {
            usedItem = eventData.pointerPress;
            if (baseObjectInteraction[i].hasDetectedObject && eventData.pointerPress.layer == correspondingItem.layer)
            {
                baseObjectInteraction[i].detectItemUI.enabled = false;
                baseObjectInteraction[i].itemInteraction.SetActive(false);
                LevelManager.Instance.camExplorationAudioSource.PlayOneShot(LevelManager.Instance.itemUseSFX, 0.5f);
                Destroy(objectToInteractWith);
                inventoryManager.DeleteItemAfterUse(usedItem);
            }
        }            
    }
}
