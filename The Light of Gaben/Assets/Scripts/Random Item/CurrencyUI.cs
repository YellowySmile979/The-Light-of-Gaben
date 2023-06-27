using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public Text ppCount;
    public Text lightShardCount;
    int addValueForPP, addValueForLS;

    //updates the UI text for the total currency
    public void UpdateText(float currencyValue, CurrencyData currencyDataType)
    {
        if (currencyDataType.currencyType == CurrencyData.Type.PP)
        {
            addValueForPP += (int)currencyValue;
            ppCount.text = addValueForPP.ToString();
        }
        else if (currencyDataType.currencyType == CurrencyData.Type.LightShard)
        {
            addValueForLS += (int)currencyValue;
            lightShardCount.text = addValueForLS.ToString();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //sets the respective currency text to display the value of the appropriate amount of currency
        //so that it carries between levels
        ppCount.text = PlayerPrefs.GetInt("PP Count").ToString();
        lightShardCount.text = PlayerPrefs.GetInt("LS Count").ToString();
    }
    void Update()
    {
        //constantly updates the player prefs
        PlayerPrefs.SetInt("PP Count", addValueForPP);
        PlayerPrefs.SetInt("LS Count", addValueForLS);
    }
}
