using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableColour : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    [Header("Scriptable Colour")]
    public ScriptableColour scriptableColour;
    [Header("Related to Dragging")]
    public Image colour; 
    public Transform parentAfterDrag;
    public bool isColour = false;

    void Start()
    {
        //SetColour();
    }
    //sets the colour of the colour
    public void SetColour()
    {
        colour.color = scriptableColour.colour;
    }
    //handles the start of dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        //sets raycast target to false so that mouse can detect what's underneath it
        colour.raycastTarget = false;
        //sets the parent to return to
        parentAfterDrag = transform.parent;
        //sets the parent to the parent of the colour's parent
        transform.SetParent(transform.root);
        //sets this to appear above everything else
        transform.SetAsLastSibling();
    }
    //handles dragging
    public void OnDrag(PointerEventData eventData)
    {
        //sets the colour to be the same position as the pointer position
        transform.position = Input.mousePosition;
    }
    //handles end of drag
    public void OnEndDrag(PointerEventData eventData)
    {
        //turn raycast target back on so that pointer can detect item again
        colour.raycastTarget = true;
        //sets parent to be the original parent
        transform.SetParent(parentAfterDrag);
        transform.SetAsFirstSibling();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
