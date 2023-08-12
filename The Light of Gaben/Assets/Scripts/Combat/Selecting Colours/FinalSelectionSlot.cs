using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FinalSelectionSlot : MonoBehaviour, IDropHandler
{
    //handles when the pointer stops dragging the item and drops it
    public void OnDrop(PointerEventData eventData)
    {
        //this is to ensure that the colour picker slot doesnt kill itself
        if (transform.childCount == 1)
        {            
            //gets the item
            DraggableColour draggableColour = eventData.pointerDrag.GetComponent<DraggableColour>();
            //sets its parent to the transform of the slot
            draggableColour.parentAfterDrag = transform;
            gameObject.GetComponent<ColourCombininig>().draggableColours.Add(draggableColour);
        }
    }
}
