using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Vector4(0, 1, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Light Source")
        {
            Debug.Log("YES");
            sr.color = new Vector4(1, 1, 1, 1);
        }
    }
}
