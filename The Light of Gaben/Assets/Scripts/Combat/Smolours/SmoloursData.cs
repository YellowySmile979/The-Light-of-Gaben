using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SmolourData", menuName = "SmolourData/SmolourBuffs")]
public class SmoloursData : ScriptableObject
{
    public enum Rarity { R, SR, SSR }
    public Sprite known;
    public Rarity rarity;
    public float hpBonus = 0;
    public float redMultiplier = 0.0f, yellowMultiplier = 0.0f, blueMultiplier = 0.0f, orangeMultiplier = 0.0f, greenMultiplier = 0.0f, magentaMultiplier = 0.0f;
    public float speedBonus = 0.0f;
    public float attackBonus = 0.0f;
    public float defenseBonus = 0.0f;
    public float shield = 0.0f;
    public float critBonus = 0.0f;

    public string description;
}
