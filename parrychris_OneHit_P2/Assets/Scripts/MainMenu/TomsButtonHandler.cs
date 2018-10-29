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

	void Start(){
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

    public void changeBackground(int arg){
        int backgroundImage = arg;
        string filePath;
        Image imageScript = MainMenu.GetComponent<Image>(); // component that renders the background

        Sprite newBackground;


        switch(backgroundImage){
            case 0:
                Debug.Log("display main background");
                //imageScript.sprite = Resources.Load<Sprite>("Assets/Texture/MainMenuBackground");
                filePath = "Backgrounds/MainMenuBackground";
                break;
            case 1:
                Debug.Log("display map 1 background");
                //imageScript.sprite = Resources.Load<Sprite>("Assets/Texture/map1");
                filePath = "Backgrounds/map1";
                break;  
            case 2:
                Debug.Log("display map 2 background");
                //imageScript.sprite = Resources.Load<Sprite>("Assets/Texture/map2");
                filePath = "Backgrounds/map2";
                break;
            default:
                Debug.Log("display main background");
                //imageScript.sprite = Resources.Load<Sprite>("Assets/Texture/MainMenuBackground");
                filePath = "Backgrounds/MainMenuBackground";
                break;

        }

        newBackground = Resources.Load<Sprite>(filePath);
        Debug.Log(newBackground);
        imageScript.sprite = newBackground;
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
