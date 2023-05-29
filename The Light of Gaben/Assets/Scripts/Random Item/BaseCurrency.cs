using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCurrency : MonoBehaviour
{
    public CurrencyData currencyData;
    bool hasCollided = false;
    Transform setSpawnPosition;

    CurrencyUI currencyUI;
    SpawnRandomItem spawnRandomItem;

    public void SetSpawnPosition(Transform transform)
    {
        setSpawnPosition = transform;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            //prevents function from being called so many times
            if (hasCollided) return;

            spawnRandomItem.spawnPositionList.Add(setSpawnPosition);
            //sets the hasRandomised to false to allow for the code to fire again even after all has spawned
            spawnRandomItem.hasRandomised = false;
            //returns itemValue and currencyData to get the type of currency
            currencyUI.UpdateText(currencyData.itemValue, currencyData);
            LevelManager.Instance.camExplorationAudioSource.PlayOneShot(LevelManager.Instance.pickUpItemSFX, 0.5f);
            Destroy(gameObject);
            //sets the bool to true so that we can prevent further function calls
            hasCollided = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnRandomItem = FindObjectOfType<SpawnRandomItem>();
        currencyUI = FindObjectOfType<CurrencyUI>();
    }
    void Update()
    {
        hasCollided = false;
    }
}
