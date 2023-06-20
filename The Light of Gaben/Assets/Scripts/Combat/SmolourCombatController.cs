using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmolourCombatController : MonoBehaviour
{
    public enum Rarity { R, SR, SSR }
    public Rarity rarity;
    public float redMultiplier = 0.0f;
    public float redBonus = 0.0f;
    public float yellowMultiplier = 0.0f;
    public float yellowBonus = 0.0f;
    public float blueMultiplier = 0.0f;
    public float blueBonus = 0.0f;
    public float speedMultiplier = 0.0f;
    public float speedBonus = 0.0f;
    public float attackMulitplier = 0.0f;
    public float attackBonus = 0.0f;
    public float defenseMultiplier = 0.0f;
    public float defenseBonus = 0.0f;
    public float shield = 0.0f;
    public float critBonus = 0.0f;

    public string description;
}
