using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasController : MonoBehaviour
{
    PlayerCombatController player;
    CombatStateController stateController;
    //a singleton
    public static CanvasController Instance;

    [Header("Main Combat")]
    public Image gabenHPBar;
    public Text combatActions;
    public GameObject lightChanger;
    public Image lightBG;
    bool hasPickedColour = false;

    [Header("Final Resulting Colour")]
    public GameObject result;
    public ScriptableColour red, blue, yellow, orange, magenta, green;
    public GameObject colour1, colour2;    

    [Header("UI")]
    public GameObject colourPicker;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerCombatController>();
        stateController = FindObjectOfType<CombatStateController>();
        if (lightBG == null) lightBG = GameObject.Find("LightBackGround").GetComponent<Image>();
        lightChanger.SetActive(false);
    }
    private void Update()
    {
        //corresponds the hp to the visual element
        gabenHPBar.fillAmount = Mathf.Clamp(player.health / player.maxHealth, 0, 1f);
        //sets the text
        combatActions.text = stateController.actionDesc.ToString();
        //changes the colour of the attack accordingly depending on what colour the player chooses
        FinalResultingColour();
        /*if (player.lightType == UnitStats.LightTypes.Red) lightBG.color = Color.red;
        else if (player.lightType == UnitStats.LightTypes.Blue) lightBG.color = Color.blue;
        else if (player.lightType == UnitStats.LightTypes.Yellow) lightBG.color = Color.yellow;
        else lightBG.color = Color.white;*/

        //deactivates/activates the UI for the battle menu
        if (stateController.state == CombatStateController.GameStates.Player) LightChangeMenu();
        else LightChangeExit();
    }
    //self-explanatory
    public void LightChangeMenu()
    {
        lightChanger.SetActive(true);
    }
    //self-explanatory
    public void LightChangeExit()
    {
        lightChanger.SetActive(false);
    }
    //receives the gameobject for colour 1 and updates the variable accordingly
    public void ReceiveColour1(GameObject color1 = null)
    {
        colour1 = color1;
    }
    //receives the gameobject for colour 2 and updates the variable accordingly
    public void ReceiveColour2(GameObject color2 = null)
    {
        colour2 = color2;
    }
    //self-explanatory
    public void TurnOffColourPicker()
    {
        colourPicker.SetActive(false);
    }
    //self-explanatory
    public void TurnOnColourPicker()
    {
        colourPicker.SetActive(true);
    }
    public void FinalResultingColour()
    {
        if (colour1.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Invisible 
            && colour2.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Invisible)
        {
            //changes the result to the stated colour
            result.GetComponent<Image>().color = Color.white;
            //updates the appropriate info to this colour
            lightBG.color = Color.white;
            player.lightType = UnitStats.LightTypes.White;
            if(!hasPickedColour)
            {
                stateController.actionDesc = "Player changes their light to 'White'!";
                hasPickedColour = true;
            }
        }
        //decides what the final result will be
        //and yes, there is a condition for every possible combination of colours
        if ((colour1.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Red
            && colour2.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Invisible)
            ||
            (colour2.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Red
            && colour1.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Invisible))
        {
            //changes the result to the stated colour
            result.GetComponent<Image>().color = red.colour;
            //updates the appropriate info to this colour
            lightBG.color = red.colour;
            player.lightType = UnitStats.LightTypes.Red;
            if(!hasPickedColour)
            {
                stateController.actionDesc = "Player changes their light to 'Red'!";
                hasPickedColour = true;
            }
        }
        else if ((colour1.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Blue
            && colour2.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Invisible)
            ||
            (colour2.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Blue
            && colour1.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Invisible))
        {
            //changes the result to the stated colour
            result.GetComponent<Image>().color = blue.colour;
            //updates the appropriate info to this colour
            lightBG.color = blue.colour;
            player.lightType = UnitStats.LightTypes.Blue;
            if (!hasPickedColour)
            {
                stateController.actionDesc = "Player changes their light to 'Blue'!";
                hasPickedColour = true;
            }
        }
        else if ((colour1.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Yellow
            && colour2.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Invisible)
            ||
            (colour2.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Yellow
            && colour1.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Invisible))
        {
            //changes the result to the stated colour
            result.GetComponent<Image>().color = yellow.colour;
            //updates the appropriate info to this colour
            lightBG.color = yellow.colour;
            player.lightType = UnitStats.LightTypes.Yellow;
            if(!hasPickedColour)
            {
                stateController.actionDesc = "Player changes their light to 'Yellow'!";
                hasPickedColour = true;
            }
        }
        if ((colour1.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Red
            && colour2.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Yellow)
            ||
            (colour1.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Yellow
            && colour2.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Red))
        {
            //changes the result to the stated colour
            result.GetComponent<Image>().color = orange.colour;
            //updates the appropriate info to this colour
            lightBG.color = orange.colour;
            player.lightType = UnitStats.LightTypes.Orange;
            if(!hasPickedColour)
            {
                stateController.actionDesc = "Player changes their light to 'Orange'!";
                hasPickedColour = true;
            }
        }
        else if ((colour1.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Red
            && colour2.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Blue)
            ||
            (colour1.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Blue
            && colour2.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Red))
        {
            //changes the result to the stated colour
            result.GetComponent<Image>().color = magenta.colour;
            //updates the appropriate info to this colour
            lightBG.color = magenta.colour;
            player.lightType = UnitStats.LightTypes.Magenta;
            if(!hasPickedColour)
            {
                stateController.actionDesc = "Player changes their light to 'Magenta'!";
                hasPickedColour = true;
            }
        }
        else if ((colour1.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Blue
            && colour2.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Yellow)
            ||
            (colour1.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Yellow
            && colour2.GetComponent<DraggableColour>().scriptableColour.typeOfColour == ColourType.Blue))
        {
            //changes the result to the stated colour
            result.GetComponent<Image>().color = green.colour;
            //updates the appropriate info to this colour
            lightBG.color = green.colour;
            player.lightType = UnitStats.LightTypes.Green;
            if(!hasPickedColour)
            {
                stateController.actionDesc = "Player changes their light to 'Green'!";
                hasPickedColour = true;
            }
        }
    }
}
