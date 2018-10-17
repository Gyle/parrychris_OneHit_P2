using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TomsButtonHandler : MonoBehaviour {

	private GameObject MainMenu;
	private GameObject HowToPlay;
    private UnityEngine.EventSystems.EventSystem myEventSystem;//used to unselect buttons

    private bool checkingForNextKeyPress = false;
    private GameObject buttonCheckingFor;

	void Start(){
		MainMenu = GameObject.Find("MenuPanel");
		HowToPlay = GameObject.Find("HowToPlayPanel");
        myEventSystem = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
		if(MainMenu ==null || HowToPlay==null){
            Debug.LogError("Error finding game objects of name MenuPanel and HowToPlayPanel");
		}
	}

	public void playMap1(){
		SceneManager.LoadScene("Map1");
	}
	public void playMap2(){
		SceneManager.LoadScene("Map2");
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
