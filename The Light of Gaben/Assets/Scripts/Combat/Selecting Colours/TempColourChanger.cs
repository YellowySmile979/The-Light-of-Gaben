using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempColourChanger : MonoBehaviour
{
    //quick code noelle did at 12am to test stuff

    public PlayerCombatController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerCombatController>();
        Close();
    }
    public void Close()
    {
        player.Attack();
        gameObject.SetActive(false);
    }
    public void Red()
    {
        player.lightType = UnitStats.LightTypes.Red;
        Close();
    }
    public void Blue()
    {
        player.lightType = UnitStats.LightTypes.Blue;
        Close();
    }
    public void Yellow()
    {
        player.lightType = UnitStats.LightTypes.Yellow;
        Close();
    }
    public void Orange()
    {
        player.lightType = UnitStats.LightTypes.Orange;
        Close();
    }
    public void Green()
    {
        player.lightType = UnitStats.LightTypes.Green;
        Close();
    }
    public void Magenta()
    {
        player.lightType = UnitStats.LightTypes.Magenta;
        Close();
    }
}
