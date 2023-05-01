using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //targets the player using the player's x and y coords
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
