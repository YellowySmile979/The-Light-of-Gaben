using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableColour", menuName = "ColourData/ScriptableColour")]
public class ScriptableColour : ScriptableObject
{
    [Header("Type of Colour")]
    public ColourType typeOfColour;
    [Header("Colour")]
    public Color colour;
}
public enum ColourType
{
    Red,
    Blue,
    Yellow,
    Orange,
    Magenta,
    Green
}
