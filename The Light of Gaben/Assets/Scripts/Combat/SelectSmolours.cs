using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSmolours : MonoBehaviour
{
    SmolourGallery smolourGallery;
    public Button Select;
    public GameObject player;
    public SmoloursData[] selectedSmolours;

    public GameObject smolourSelectScreen;

    public void OnSmolourSelect(int smolourChoice)
    {
        smolourSelectScreen.SetActive(false);
        
        SmoloursData selectedCharacter = selectedSmolours[smolourChoice];
    }
}
