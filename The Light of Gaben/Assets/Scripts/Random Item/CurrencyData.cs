using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrencyData", menuName = "CurrencyData/ScriptableCurrency")]
public class CurrencyData : ScriptableObject
{
    public int itemValue;
    public enum Type
    {
        PP,
        LightShard
    }
    public Type currencyType;
}
