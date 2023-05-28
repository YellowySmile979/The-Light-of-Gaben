using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Items", menuName = "ItemData/ScriptableItems")]
public class ItemData : ScriptableObject
{
    public string nameOfItem;
    public enum Type
    {
        key,
        consumables
    }
    public Type type;
    public Sprite icon;
    public GameObject ic;
}
