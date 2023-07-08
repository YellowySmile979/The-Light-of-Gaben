using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Lists of Enemies")]
    public List<EnemyCombatController> activeCombatEnemies = new List<EnemyCombatController>();
    public List<EnemyCombatController> activatedCombatEnemies = new List<EnemyCombatController>();

    //a singleton
    public static EnemyManager Instance;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        activeCombatEnemies.AddRange(FindObjectsOfType<EnemyCombatController>());
        activatedCombatEnemies.AddRange(FindObjectsOfType<EnemyCombatController>());
    }

    // Update is called once per frame
    void Update()
    {
        if(activeCombatEnemies.Count > 0) RemoveFromList();
    }
    void RemoveFromList()
    {
        if(activeCombatEnemies.Find(s => s.health <= 0))
        {
            activeCombatEnemies.ForEach(delegate (EnemyCombatController enemy)
            {
                if (enemy.health <= 0)
                {
                    CombatStateController.Instance.defeatedEnemies++;
                    activeCombatEnemies.Remove(enemy);
                }
            });
        }
    }
}
