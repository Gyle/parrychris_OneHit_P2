using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class TomsButtonHandler : MonoBehaviour {

	private GameObject MainMenu;
	private GameObject HowToPlay;
    private UnityEngine.EventSystems.EventSystem myEventSystem;//used to unselect buttons

    private bool checkingForNextKeyPress = false;
    private GameObject buttonCheckingFor;
    private SpriteManager spriteManager;

	void Start(){
        spriteManager = new SpriteManager();
		MainMenu = GameObject.Find("MenuPanel");
		HowToPlay = GameObject.Find("HowToPlayPanel");
        myEventSystem = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        if (MainMenu == null)
        {
            Debug.LogError("Error finding game objects of name MenuPanel");
        }
        if (HowToPlay == null)
        {
            Debug.LogError("Error finding game objects of name HowToPlayPanel");
        }
	}

	public void playMap1(){
		SceneManager.LoadScene("Map1");
	}
	public void playMap2(){
		SceneManager.LoadScene("Map2");
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

    /*
     * When the player selects their character via button click, then the 
     * button will write to the data store static class which character 
     * the corresponding player selected.
     */
    public void WriteCharacterToDataStore(int player, int character){

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
		HowToPlay.SetActive(false);
		MainMenu.SetActive(true);
	}

    /**
     * Navigate From 'How to play' section back to 'MainMenu' section
     */
	public void ToHowToPlayFromMainMenu(){
		MainMenu.SetActive(false);
		HowToPlay.SetActive(true);
		
	}

    public void checkForNextKeyPress(GameObject button){
        this.checkingForNextKeyPress = true;
        this.buttonCheckingFor = button;
    }

    private void modifyKey(KeyCode newKey){
        TomsControlTextScript child = this.buttonCheckingFor.GetComponentInChildren<TomsControlTextScript>();
        //change the key code using reflection
        child.controls.GetType().GetField(child.command).SetValue(child.controls,newKey);
        //refresh the text field.
        child.Start();
        //unselect the button
        myEventSystem.SetSelectedGameObject(null);
    }
}
