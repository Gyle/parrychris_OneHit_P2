using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TomsButtonHandler : MonoBehaviour {

	private GameObject MainMenu;
	private GameObject HowToPlay;

	void Start(){
		MainMenu = GameObject.Find("MenuPanel");
		HowToPlay = GameObject.Find("HowToPlayPanel");
		if(MainMenu ==null || HowToPlay==null){
			Debug.Log("Error finding game objects of name MenuPanel and HowToPlayPanel");
		}
	}

	public void playMap1(){
		SceneManager.LoadScene("Map1");
	}
	public void playMap2(){
		SceneManager.LoadScene("Map2");
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

	public void ToMainMenuFromHowToPlay(){
		HowToPlay.SetActive(false);
		MainMenu.SetActive(true);
	}

	public void ToHowToPlayFromMainMenu(){
		MainMenu.SetActive(false);
		HowToPlay.SetActive(true);
		
	}
}
