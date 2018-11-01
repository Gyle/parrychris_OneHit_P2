using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class TomsButtonHandler : MonoBehaviour {

    // Main menu panels
	private GameObject MainMenu;
	private GameObject HowToPlay;

    //audio
    private AudioSource btnPressed;
    private AudioSource fightMusic;
    private AudioSource menuMusic;

    // Character select panels
    private GameObject playerOneSelect;
    private GameObject playerTwoSelect;

    // Character select posters
    private GameObject playerOnePoster;
    private GameObject playerTwoPoster;

    // Which character panel is displayed on screen;
    private bool playerOneSelectingCharacter;

    private UnityEngine.EventSystems.EventSystem myEventSystem;//used to unselect buttons

    private bool checkingForNextKeyPress = false;
    private GameObject buttonCheckingFor;
    private SpriteManager spriteManager;

	void Start(){
        this.btnPressed = GetComponent<AudioSource>();
        this.fightMusic = GameObject.Find("FightMusic").GetComponent<AudioSource>();
        this.menuMusic = GameObject.Find("MenuMusic").GetComponent<AudioSource>();
        spriteManager = new SpriteManager();
		MainMenu = GameObject.Find("MenuPanel");
		HowToPlay = GameObject.Find("HowToPlayPanel");
        playerOneSelect = GameObject.Find("PlayerOneCharacterSelectPanel");
        playerTwoSelect = GameObject.Find("PlayerTwoCharacterSelectPanel");
        playerOnePoster = GameObject.Find("PlayerOnePoster");
        playerTwoPoster = GameObject.Find("PlayerTwoPoster");

        // On start, player one is always the first to select character
        playerOneSelectingCharacter = true;   


        myEventSystem = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        if (MainMenu == null)
        {
            Debug.LogError("Error finding game objects of name MenuPanel");
        }
        if (HowToPlay == null)
        {
            Debug.LogError("Error finding game objects of name HowToPlayPanel");
        }
        if (playerOneSelect == null){
            Debug.Log("Error finding game objects of name PlayerOneCharacterSelectPanel");
        }
        if (playerTwoSelect == null)
        {
            Debug.Log("Error finding game objects of name PlayerTwoCharacterSelectPanel");
        }
        if (playerOnePoster == null)
        {
            Debug.Log("Error finding game objects of name playerOnePoster");
        }
        if (playerTwoPoster == null)
        {
            Debug.Log("Error finding game objects of name playerTwoPoster");
        }

        // hide player 2 character select panel on load
        playerTwoSelect.SetActive(false);
	}

    public void ToCharacterSelect(){
        SceneManager.LoadScene("Character_Menu");
    }

	public void playMap1(){
        this.menuMusic.Stop();
        this.fightMusic.PlayDelayed(2.0f);
		SceneManager.LoadScene("Map1");
	}
	public void playMap2(){
        this.menuMusic.Stop();
        this.fightMusic.PlayDelayed(2.0f);
		SceneManager.LoadScene("Map2");
	}

    public void UpdatePlayerOnePoster(int character){
        SetPlayerPosterImage(true, character);
    }

    public void UpdatePlayerTwoPoster(int character)
    {
        SetPlayerPosterImage(false, character);
    }

    /*
     * Upon the mouse hovering over a button, when applicable, this method 
     * will change the poster image to a sprite of the corresponding player 
     * to display which character they are selecting
     * 
     *  Case 1 = character1
     *  Case 2 = character2 
     *  Case 3 = character3
     * 
     * This means it may be extended to showcase more maps by adding more cases.
     * 
     *  - isLeftSide is if we are modifying left poster
     *  - character is the character ID. EG 1 = character 1.
     */
    private void SetPlayerPosterImage(bool isLeftSide, int character)
    {
        // Determine which character poster it should modify
        GameObject characterPoster = (isLeftSide) ? this.playerOnePoster : this.playerTwoPoster;

        // change the background sprite based on the corresponding value.
        switch (character)
        {
            case 1:
                spriteManager.UpdateCharacterPoster(characterPoster, isLeftSide, 1);
                break;
            case 2:
                spriteManager.UpdateCharacterPoster(characterPoster, isLeftSide, 2);
                break;
            case 3:
                spriteManager.UpdateCharacterPoster(characterPoster, isLeftSide, 3);
                break;
            default:
                spriteManager.UpdateCharacterPoster(characterPoster, isLeftSide, 0);
                break;

        }
    }

    /*
     * Upon the mouse hovering over a button, when applicable, this method 
     * will change the background to a sprite of the map to display to the 
     * player what the map looks like. 
     * 
     *  Case 0 = Main menu background
     *  Case 1 = Map one 
     *  Case 2 = Map two
     * 
     * This means it may be extended to showcase more maps by adding more cases.
     * 
     *
     *  - backgroundImage represents which background to display. EG 1 = map1 background
     */
    public void ChangeBackground(int backgroundImage){
        // change the background sprite based on the corresponding value.
        switch(backgroundImage){
            case 0:
                spriteManager.ChangeMapBackgroundImage(MainMenu, 0);
                break;
            case 1:
                spriteManager.ChangeMapBackgroundImage(MainMenu, 1);
                break;  
            case 2:
                spriteManager.ChangeMapBackgroundImage(MainMenu, 2);
                break;
            default:
                spriteManager.ChangeMapBackgroundImage(MainMenu, 0);
                break;

        }
    }

    public void HighlightPortrait(int character){
        ChangePortraitHighlight(true, character);
    }

    public void UnhighlightPortrait(int character)
    {
        ChangePortraitHighlight(false, character);
    }

    /*
     * Upon the mouse hovering over a button, when applicable, this method 
     * will change the highlight of a  character portrait display to the 
     * player what character they are currently selecting
     * 
     *  Case 1 = character1
     *  Case 2 = character2
     *  Case 3 = character3
     * 
     * This means it may be extended to showcase more maps by adding more cases.
     * 
     *  - highlight is if the portrait must be highlighted or not
     *  - character is the character ID. EG 1 = character 1.
     */
    private void ChangePortraitHighlight(bool highlight,int character)
    {
        // Determine which character panel it should highlight
        GameObject characterPanel = (this.playerOneSelectingCharacter) ? this.playerOneSelect : this.playerTwoSelect;

        // Get the portrait that needs to be highlighted from the character panel
        GameObject characterPortrait = GetPortraitGameObjectByParent(characterPanel, character);


        // change the background sprite based on the corresponding value.
        switch (character)
        {
            case 1:
                spriteManager.UpdateCharacterPortrait(characterPortrait, highlight, 1);
                break;
            case 2:
                spriteManager.UpdateCharacterPortrait(characterPortrait, highlight, 2);
                break;
            case 3:
                spriteManager.UpdateCharacterPortrait(characterPortrait, highlight, 3);
                break;
            default:
                spriteManager.UpdateCharacterPortrait(characterPortrait, highlight, 0);
                break;

        }
    }

    /*
     * Given a parent game object that is a character select panel, return the 
     * corresponding child as a gabe object.
     * 
     *  - parent is character select panel
     *  - character is the character ID. EG 1 = character 1.
     */
    private GameObject GetPortraitGameObjectByParent(GameObject parent,int character){
        switch (character)
        {
            case 1:
                return parent.transform.Find("character1").gameObject;
            case 2:
                return parent.transform.Find("character2").gameObject;
            case 3:
                return parent.transform.Find("character3").gameObject;
            default:
                Debug.Log("Error, character was out of range of 1 to 3");
                return null;

        }
    }

    /*
     * When the player selects their character via button click, then the 
     * button will write to the data store static class which character 
     * the corresponding player selected.
     * 
     *  - player represents if it is player 1 or 2
     *  - character is the character ID. EG 1 = character 1.
     */
    private void WriteCharacterToDataStore(int player, int character){

        // Update the Data Store based on which player we are dealing with.
        switch(player){
            case 1:
                DataStore.PlayerOneCharacter = character;
                break;
            case 2:
                DataStore.PlayerTwoCharacter = character;
                break;
            default:
                Debug.Log("Error: Player must either be 1 or 2 (player was outside of range)");
                break;
        }
    }


    public void SelectCharacterForPlayerOne(int character){
        WriteCharacterToDataStore(1, character);
    }

    public void SelectCharacterForPlayerTwo(int character)
    {
        WriteCharacterToDataStore(2, character);
    }



    public void PlayerOnePanelToPlayerTwo(){
        this.btnPressed.Play();//make btn pressed sound
        playerOneSelectingCharacter = false;
        playerOneSelect.SetActive(false);
        playerTwoSelect.SetActive(true);
    }

    public void PlayerTwoPanelToPlayerOne(){
        this.btnPressed.Play();//make btn pressed sound
        playerOneSelectingCharacter = true;
        playerOneSelect.SetActive(true);
        playerTwoSelect.SetActive(false);
    }

    public void ToMapSelect(){
        this.btnPressed.Play();//make btn pressed sound
        SceneManager.LoadScene("Map_Menu");
    }

    void Update()
    {
        if (checkingForNextKeyPress)//only check for keypresses if we are changing the controls.
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))//find which key was pressed
            {
                if (Input.GetKey(vKey))
                {
                    this.checkingForNextKeyPress = false;
                    modifyKey(vKey);
                    return;
                }
            }
        }
    }

    public void Quit()
	{
        this.btnPressed.Play();//make btn pressed sound
		//If we are running in a standalone build of the game
	#if UNITY_STANDALONE
		//Quit the application
		Application.Quit();
	#endif

		//If we are running in the editor
	#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
	#endif
	}

    /**
     * Navigate From 'Main Menu' section to 'How to play' section
     */
	public void ToMainMenuFromHowToPlay(){
        this.btnPressed.Play();//make btn pressed sound
		HowToPlay.SetActive(false);
		MainMenu.SetActive(true);
	}

    /**
     * Navigate From 'How to play' section back to 'MainMenu' section
     */
	public void ToHowToPlayFromMainMenu(){
        this.btnPressed.Play();//make btn pressed sound
		MainMenu.SetActive(false);
		HowToPlay.SetActive(true);
		
	}

    public void checkForNextKeyPress(GameObject button){
        this.btnPressed.Play();//make btn pressed sound
        this.checkingForNextKeyPress = true;
        this.buttonCheckingFor = button;
    }

    /*
     * Switch between player one using controller in option menu
     */
    public void TogglePS3ControllerPlayerOne(GameObject button){
        DataStore.controller1 = !DataStore.controller1;
        string setting = (DataStore.controller1) ? "on" : "off";
        Text child = button.GetComponentInChildren<Text>();

        //change the value of the text
        child.text = setting;
    }

    /*
     * Switch between player two using controller in option menu
     */
    public void TogglePS3ControllerPlayerTwo(GameObject button){
        DataStore.controller2 = !DataStore.controller2;
        string setting = (DataStore.controller2) ? "on" : "off";
        Text child = button.GetComponentInChildren<Text>();

        //change the value of the text
        child.text = setting;
            

    }

    private void modifyKey(KeyCode newKey){
        this.btnPressed.Play();//make btn pressed sound
        TomsControlTextScript child = this.buttonCheckingFor.GetComponentInChildren<TomsControlTextScript>();
        //change the key code using reflection
        child.controls.GetType().GetField(child.command).SetValue(child.controls,newKey);
        //refresh the text field.
        child.Start();
        //unselect the button
        myEventSystem.SetSelectedGameObject(null);
    }
}
