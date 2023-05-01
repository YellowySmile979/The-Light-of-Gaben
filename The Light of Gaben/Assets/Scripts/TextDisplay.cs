using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>())
        {
            mr.enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>())
        {
            mr.enabled = false;
        }
    }
}
