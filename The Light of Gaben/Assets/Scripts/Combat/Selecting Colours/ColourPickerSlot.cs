using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColourPickerSlot : MonoBehaviour, IDropHandler
{
    //handles when the pointer stops dragging the item and drops it
    public void OnDrop(PointerEventData eventData)
    {
        //if there isnt anything in the slot, perform the following
        if(transform.childCount == 0)
        {
            //gets the item
            DraggableColour draggableColour = eventData.pointerDrag.GetComponent<DraggableColour>();
            //sets its parent to the transform of the slot
            draggableColour.parentAfterDrag = transform;
        }
    }
}
