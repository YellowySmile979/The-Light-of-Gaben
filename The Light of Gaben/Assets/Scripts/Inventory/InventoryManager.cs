using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public ItemData[] inventory;
    public int maxSlots = 9;
    InventoryOpener inventoryOpener;
    [SerializeField] private InventorySlot[] inventorySlots;
    public GameObject[] itemsInSlots;
    public Vector3 scaleInventoryItemSize;
    ItemInventory[] itemInventory;
    ItemInteraction itemInteraction;

    //a singleton
    public static InventoryManager Instance;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        inventory = new ItemData[maxSlots];
        inventoryOpener = FindObjectOfType<InventoryOpener>();
        itemInventory = FindObjectsOfType<ItemInventory>();
        itemInteraction = FindObjectOfType<ItemInteraction>(true);
    }
    //adds item to the inventory list
    public void AddItem(ItemData collectible)
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                print(collectible);
                inventory[i] = collectible;
                return;
            }
        }
    }
    //after we use the item, we delete it from the inventory
    public void DeleteItemAfterUse(GameObject usedItem)
    {
        print("Fire");
        for(int itemDataCounter = 0; itemDataCounter < inventory.Length; itemDataCounter++)
        {
            if(itemsInSlots[itemDataCounter].gameObject == usedItem)
            {
                itemInteraction.hasDestroyed = false;
                inventory[itemDataCounter] = null;
                itemsInSlots[itemDataCounter] = null;
                itemInteraction.hasDestroyed = true;
            }
        }
    }
    //when there is no item data recorded in the inventory and no stored item in that specific slot, spawn in one
    public void ShowItem()
    {
        for (int itemDataCounter = 0; itemDataCounter < inventory.Length; itemDataCounter++)
        {
            if (inventory[itemDataCounter] != null && itemsInSlots[itemDataCounter] == null)
            {
                //spawns in the item
                GameObject item = Instantiate(inventory[itemDataCounter].ic,
                    inventorySlots[itemDataCounter].transform.position, Quaternion.identity);

                //sets the spawned item to the slot
                itemsInSlots[itemDataCounter] = item;
                //sets the initial transform of the item to the respective slot 
                Transform parentTransform = inventorySlots[itemDataCounter].transform;
                item.transform.SetParent(parentTransform);
                if(itemInventory.Length > itemsInSlots.Length)
                {
                    print(itemsInSlots.Length);
                    itemInventory[itemDataCounter].parentAfterDrag = parentTransform.transform.parent;
                }                
            }
            //actiavtes the inventory object
            if(itemsInSlots[itemDataCounter] != null)
            {
                itemsInSlots[itemDataCounter].gameObject.SetActive(true);
            }
            else
            {
                return;
            }
        }       
        //print(itemDataCounter);
    }
    //deactivates the inventory objects
    public void HideItem()
    {
        for (int i = 0; i < itemsInSlots.Length; i++)
        {
            if(itemsInSlots[i] != null)
            {
                itemsInSlots[i].gameObject.SetActive(false);
            }
            else
            {
                return;
            }
        }
    }
    /*public void InventoryOpener()
    {
        Vector2 inventoryPosition = inventoryOpener.transform.position;

        // Get the mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the mouse to the object
        Vector3 direction = (inventoryPosition - mousePosition).normalized;

        float maxDistance = 310f;

        // Draw the ray for debugging purposes
        Debug.DrawRay(mousePosition, direction * maxDistance, Color.red);
        Debug.Log(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, direction, maxDistance, layerMask);
        
        if (hit.collider != null && hit.collider.tag == "Inventory" && !onOrOff)
        {
            Debug.Log("Inventory object on");
            //calls the TurnOnObjects function in the InventoryOpener attached to the other object
            InventoryOpener inventoryOpener = FindObjectOfType<InventoryOpener>();
            inventoryOpener.TurnOnObjects();
            onOrOff = true;
        }
        else if(hit.collider != null && hit.collider.tag == "Inventory" && onOrOff)
        {
            Debug.Log("Inventory object off");
            //calls the TurnOnObjects function in the InventoryOpener attached to the other object
            InventoryOpener inventoryOpener = FindObjectOfType<InventoryOpener>();
            inventoryOpener.TurnOffObjects();
            onOrOff = false;
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        if(inventoryOpener.onOrOff == true)
        {
            ShowItem();
        }
    }
}
