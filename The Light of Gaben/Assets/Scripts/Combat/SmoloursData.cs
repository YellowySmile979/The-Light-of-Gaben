using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SmolourData", menuName = "SmolourData/SmolourBuffs")]
public class SmoloursData : ScriptableObject
{
    public enum Rarity { R, SR, SSR }
    public Rarity rarity;
    public float redMultiplier = 0.0f, redBonus = 0.0f;
    public float yellowMultiplier = 0.0f, yellowBonus = 0.0f;
    public float blueMultiplier = 0.0f, blueBonus = 0.0f;
    public float speedMultiplier = 0.0f, speedBonus = 0.0f;
    public float attackMulitplier = 0.0f, attackBonus = 0.0f;
    public float defenseMultiplier = 0.0f, defenseBonus = 0.0f;
    public float shield = 0.0f;
    public float critBonus = 0.0f;

    public string description;
}
