using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInventory : Item, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    Image image;
    Color tempColour;
    public Transform parentAfterDrag;
    CanvasGroup canvasGroup;
    [SerializeField] bool hasSetParent = false;
    [SerializeField] ItemInteraction itemInteraction;
    SpriteRenderer sr;

    void Awake()
    {
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        itemInteraction = FindObjectOfType<ItemInteraction>(true);
        sr = GetComponent<SpriteRenderer>();
    }
    //handles what happens on start of drag
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //sets the alpha value to translucent so that we can see behind
        tempColour = image.color;
        tempColour.a = 0.5f;
        image.color = tempColour;

        //sets the parentAfterDrag to the parent transform ie the slot the thing is attached to
        parentAfterDrag = transform.parent;
        //sets the parent to the topmost transform of the hierarchy ie the Inventory parent
        transform.SetParent(transform.root);
        //moves the item to the bottom most row
        transform.SetAsLastSibling();

        canvasGroup.blocksRaycasts = false;
    }
    //sets the object to the position of the pointer
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    //handles what happens on end of drag
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        //sets the alpha value to translucent so that we can see behind
        tempColour = image.color;
        tempColour.a = 1f;
        image.color = tempColour;

        transform.SetParent(parentAfterDrag);
        canvasGroup.blocksRaycasts = true;
        if (itemInteraction.hasDestroyed)
        {
            Destroy(gameObject);
        }
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        
    }
}
