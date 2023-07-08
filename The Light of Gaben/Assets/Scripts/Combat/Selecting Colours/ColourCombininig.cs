using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourCombininig : MonoBehaviour
{
    public GameObject colourChild;
    public bool isColour1;

    // Update is called once per frame
    void Update()
    {
        DetectColourChild();
    }
    //detects the colour
    void DetectColourChild()
    {
        if (gameObject.GetComponentInChildren<DraggableColour>())
        {
            colourChild = GetComponentInChildren<DraggableColour>().gameObject;
        }
        else
        {
            return;
        }
        if (isColour1) CanvasController.Instance.ReceiveColour1(colourChild);
        else CanvasController.Instance.ReceiveColour2(colourChild);
    }
}
