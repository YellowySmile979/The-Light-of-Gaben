using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    public GameObject player;
    public GameObject footstepsSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   // play the footstep sfx if player is walking and pause it when we aren't
        if (player.GetComponent<PlayerMovement>().isMoving)
        {
            footsteps();
        }
        else
        {
            stopFootsteps();
        }
    }

    void footsteps()
    {
        footstepsSound.SetActive(true);
    }

    void stopFootsteps()
    {
        footstepsSound.SetActive(false);
    }
}
