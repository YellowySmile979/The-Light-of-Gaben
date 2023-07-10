using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmolourGallery : MonoBehaviour
{
    public List<SmoloursData> collectedSmolours;

    public SmoloursData[] Smolours;
    public Text[] Texts;
    public Image[] Images;
    public GameObject Gallery;
    public GameObject SelectScreen;
    private void Start()
    {
        Close();
    }
    public void Close()
    {
        Gallery.SetActive(false);
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void Open()
    {
        Gallery.SetActive(true);
        foreach (SmoloursData smolour in collectedSmolours)
        {
            // searches for the index of each collected smolour in the Smolour array, and changes the corresponding Image and text.
            int index = 0;
            for (; index < Smolours.Length; index++)
            {
                if (Smolours[index] == smolour) break;
            }

            Images[index].sprite = smolour.known;
            Texts[index].text = smolour.description;
        }
    }
}
