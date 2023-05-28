using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public bool hasWon;

    void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        CheckToSeeIfCombatHasEnded();
    }
    void CheckToSeeIfCombatHasEnded()
    {
        if (BaseEnemy.instance.hasLoaded == false)
        {
            if (hasWon)
            {
                SceneManager.UnloadSceneAsync(BaseEnemy.instance.combatScene);
                BaseEnemy.instance.explorationCanvas.enabled = true;
                Destroy(BaseEnemy.instance.gameObject);
            }
            else
            {
                SceneManager.UnloadSceneAsync(BaseEnemy.instance.combatScene);
                BaseEnemy.instance.moveSpeed = BaseEnemy.instance.originalMoveSpeed;
                BaseEnemy.instance.turnSpeed = BaseEnemy.instance.originalTurnSpeed;
                BaseEnemy.instance.explorationCanvas.enabled = true;
            }
        }
    }
}
