using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourCombininig : MonoBehaviour
{
    public List<DraggableColour> draggableColours = new List<DraggableColour>();
    public GameObject colourChild;
    public bool isColour1;

    void Start()
    {
        draggableColours.Add(GetComponentInChildren<DraggableColour>());
    }
    // Update is called once per frame
    void Update()
    {
        DetectColourChild();
    }
    //detects the colour
    void DetectColourChild()
    {
        if (draggableColours.Find(colour => colour.GetComponent<DraggableColour>().scriptableColour.typeOfColour != ColourType.Invisible))
        {
            //colourChild = GetComponentInChildren<DraggableColour>().gameObject;
            colourChild = draggableColours.Find(colour =>
            colour.GetComponent<DraggableColour>().scriptableColour.typeOfColour != ColourType.Invisible).gameObject;
            colourChild.transform.SetAsFirstSibling();
        }
        if (transform.childCount == 1)
        {
            colourChild.transform.SetAsLastSibling();
            //colourChild = GetComponentInChildren<DraggableColour>().gameObject;
            DraggableColour draggableColour = draggableColours.Find(colour =>
            colour.GetComponent<DraggableColour>().scriptableColour.typeOfColour != ColourType.Invisible);
            draggableColours.Remove(draggableColour);
            colourChild = draggableColours.Find(colour =>
            colour.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Invisible).gameObject;
        }
        //decides if the slot is colour1 or colour2
        if (isColour1) CanvasController.Instance.ReceiveColour1(colourChild);
        else CanvasController.Instance.ReceiveColour2(colourChild);
    }
}
