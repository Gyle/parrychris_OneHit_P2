using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This is a utility class to help with loading sprites in the Resources 
 * folder into the game. It also helps changing the sprites associated 
 * with GameObjects.
 */
public class SpriteManager {
    private Sprite[] mapSprites;
    private Sprite[] characterSelectSprites;

    // currently two maps
    private int mapCount = 2;

    // currently three characters
    private int characterCount = 3;

	// Use this for initialization
    public SpriteManager () {
        // Initialise the arrays 
        this.mapSprites = new Sprite[mapCount+1];   // +1 because of default background at title screen.
        this.characterSelectSprites = new Sprite[characterCount];

        // Populate the arrays
        this.LoadMapSprites();
        this.LoadCharacterSprites();
	}

    /*
     * In order to load map images as backgrounds, the name of the file must follow 
     * a simple naming convention. EG: Map one = map1.png or .jpg
     */
    private void LoadMapSprites(){
        // Set index 0 as the main menu background
        mapSprites[0] = Resources.Load<Sprite>("Backgrounds/MainMenuBackground");

        // load all map background images
        for (int i = 1; i <= mapCount; i+=1){
            // determine file path of map image
            string filePath = "Backgrounds/map" + i;

            // Set map sprites to coresponding indices.
            mapSprites[i] = Resources.Load<Sprite>(filePath);
        }
    }

    /*
     * In order to load character images for character select, the name of the 
     * file must follow a simple naming convention. 
     * EG: Character one = character1.png or .jpg
     */
    private void LoadCharacterSprites(){
        // currently returns as we need to change the project structure to work 
        // for loading character images.
        return;

        // load all character images
        for (int i = 1; i < characterCount; i += 1)
        {
            // determine file path of map image
            string filePath = "Characters/character" + i;

            // Set map sprites to coresponding indices.
            characterSelectSprites[i] = Resources.Load<Sprite>(filePath);
        }
    }

    /*
     * This method is currently called by TomsButtonHandler class. Its purpose 
     * is to change the background image based on the information it is provided.
     * 
     *  - mainMenu is the GameObject that contains the Image of the background
     *  - background is the category. EG 1 = map1 sprite.
     */
    public void ChangeMapBackgroundImage(GameObject mainMenu, int background){
        // Get image component from the main menu
        Image imageScript = mainMenu.GetComponent<Image>();

        // Change the background of the image component
        imageScript.sprite = mapSprites[background];
    }
	
    /*
     * This method will change the character select image to the corresponding 
     * side.
     * 
     *  - characterPanel is the panel that will display the character image
     *  - isLeftSide determines is character image belongs on left or right side
     *  - character is the character type
     */
    public void ChangeCharacterSelectImage(GameObject characterPanel, bool isLeftSide, int character){
        // Get image component from the character select panel
        Image imageScript = characterPanel.GetComponent<Image>();

        // Get the corresponding character sprite
        Sprite characterSprite = characterSelectSprites[character];

        // Check if we need to flip the sprite.
        //if(!isLeftSide){
        //    FlipSprite(characterSprite);
        //}

        // change the sprite to the coressponding character
        imageScript.sprite = characterSprite;
    }

    /*
     * This function will flip a given sprite horizontally so that the renderer 
     * would not need to flip the sprite itself.
     */
    private void FlipSprite(Sprite sprite){
        // Unsure of how to flip sprite without manually flipping at render level code.
        //sprite.transform.Rotate(new Vector3(0, 180, 0));
    }
}
