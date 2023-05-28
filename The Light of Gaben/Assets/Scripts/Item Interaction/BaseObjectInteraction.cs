using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseObjectInteraction : MonoBehaviour
{
    [Header("Detect Object")]
    public Transform detectObjectArea;
    public float detectObjectRadius;
    public LayerMask whatIsAnObject;
    public bool hasDetectedObject;

    [Header("UI")]
    public Image detectItemUI;
    public GameObject itemInteraction;
    ItemInteraction ItemInteraction;

    void Awake()
    {
        ItemInteraction = FindObjectOfType<ItemInteraction>(true);
    }
    void Update()
    {
        hasDetectedObject = Physics2D.OverlapCircle(transform.position, detectObjectRadius, whatIsAnObject);
        if (hasDetectedObject)
        {
            itemInteraction.SetActive(true);
            detectItemUI.enabled = true;
            ItemInteraction.objectToInteractWith = gameObject;
        }
        else if (ItemInteraction.objectToInteractWith == gameObject)
        {
            itemInteraction.SetActive(false);
            detectItemUI.enabled = false;
            ItemInteraction.objectToInteractWith = null;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap Sphere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, detectObjectRadius);
    }
}
