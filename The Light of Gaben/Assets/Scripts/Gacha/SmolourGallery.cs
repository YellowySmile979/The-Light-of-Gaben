using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmolourGallery : MonoBehaviour
{
    public List<SmoloursData> collectedSmolours;
    public Sprite unknown;

    public SmoloursData[] Smolours;
    public Text[] Texts;
    public Image[] Images;
    public GameObject Gallery;
    public GameObject SelectScreen;

    public GameObject smolourGalleryPrefab, galleryParent;
    public List<GameObject> smolourGalleryControllers;

    private void Start()
    {

        foreach (SmoloursData smolours in Smolours)
        {
            SmoloursData smolour = smolours;    
            GameObject newSmolour = Instantiate(smolourGalleryPrefab, galleryParent.transform);
            newSmolour.GetComponent<SmolourGalleryController>().displayedSprite.sprite = unknown;
            newSmolour.GetComponent<SmolourGalleryController>().description.text = "UNKNOWN";
            newSmolour.GetComponent<SmolourGalleryController>().smoloursData = smolour;
            smolourGalleryControllers.Add(newSmolour);
        }
        Close();
    }
    public void Close()
    {
        Gallery.SetActive(false);
    }
    public void Open()
    {
        Gallery.SetActive(true);
        foreach (SmoloursData smolour in collectedSmolours)
        {
            // searches for the index of each collected smolour in the Smolour array, and changes the corresponding Image and text.
            
            foreach (GameObject smolourGallery in smolourGalleryControllers)
            {
                if (smolourGallery.GetComponent<SmolourGalleryController>().smoloursData == smolour)
                {
                    smolourGallery.GetComponent<SmolourGalleryController>().collected = true;
                }
            }
        }                                                                                   
    }
}