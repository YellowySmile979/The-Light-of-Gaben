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
            ppCount.text = "PP: " + addValueForPP.ToString();
        }
        else if (currencyDataType.currencyType == CurrencyData.Type.LightShard)
        {
            addValueForLS += (int)currencyValue;
            lightShardCount.text = "Light Shard: " + addValueForLS.ToString();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ppCount.text = "PP: " + 0;
        lightShardCount.text = "Light Shard: " + 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
