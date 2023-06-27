using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemData data;
    InventoryManager inventoryManager;
    public bool hasCollided = false;

    void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }
    void Update()
    {
        //ensures that we can indeed call the function at most once
        hasCollided = false;
    }
    //when i collide with this object, i cast the data of this object to the inventory
    //which then stores this info for further use
    void OnTriggerEnter2D(Collider2D collision)
    {       
        if(collision.GetComponent<PlayerMovement>())
        {
            //prevents function from being called so many times
            if (hasCollided) return;

            //returns the item data and destroy the object
            print(collision);
            inventoryManager.AddItem(data);
            Destroy(gameObject);
            //sets the bool to true so that we can prevent further function calls
            hasCollided = true;
        }
        if (collision.GetComponent<PlayerMovement>() && data.type == ItemData.Type.winItem)
        {
            //prevents function from being called so many times
            if (hasCollided) return;

            LoadSceneManager.Instance.WinConditionCollectItems(1);
            //returns the item data and destroy the object
            inventoryManager.AddItem(data);
            Destroy(gameObject);
            //sets the bool to true so that we can prevent further function calls
            hasCollided = true;
        }
    }
}
