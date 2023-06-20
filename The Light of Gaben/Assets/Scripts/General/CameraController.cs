using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        //targets the player using the player's x and y coords
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    /*public List<string> scenesToLoad = new List<string>();
    public List<int> indexes = new List<int>();
    void SceneLoading()
    {
        int index = Random.Range(indexes[0], indexes.Count);
        SceneManager.LoadScene(scenesToLoad[index]);
    }*/
}
