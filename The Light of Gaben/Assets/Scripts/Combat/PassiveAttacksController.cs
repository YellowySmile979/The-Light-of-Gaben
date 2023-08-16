using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAttacksController : UnitStats
{
    [Header("PassiveStateController")]
    public float dmgToGive;
    public int turnsToDmg;
    public bool DoTnotEmpty;
    public UnitStats player;

    public void Damage()
    {
        player.TakeDoT(dmgToGive);
        turnsToDmg -= 1;

        if (turnsToDmg == 0) { DoTnotEmpty = false; }
        else { DoTnotEmpty = true; }
        StartCoroutine(WaitUnitStatsVer());
    }
}
