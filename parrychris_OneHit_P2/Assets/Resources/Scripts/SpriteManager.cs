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
    private Sprite[] characterPosterSprites;
    private Sprite[] playerOneScore;
    private Sprite[] playerTwoScore;

    // currently two maps
    private int mapCount = 2;

    // currently three characters
    private int characterCount = 3;

    // amount of rounds required to win game
    private int rounds = 3;

	// Use this for initialization
    public SpriteManager () {
        // Initialise the arrays 
        this.mapSprites = new Sprite[mapCount+1];   // +1 because of default background at title screen.
        this.characterSelectSprites = new Sprite[characterCount*2]; // *2 because dark and light portraits
        this.characterPosterSprites = new Sprite[characterCount*2]; // *2 because left and right sides
        this.playerOneScore = new Sprite[rounds+1]; // +1 because need to include 0 wins sprite
        this.playerTwoScore = new Sprite[rounds+1];

        // Populate the arrays
        this.LoadMapSprites();
        this.LoadCharacterPortraitSprites();
        this.LoadCharacterPosterSprites();
        this.LoadScoreboardSprites();
	}

    /*
     * Find all scoreboard sprites, load them and store them into the 
     * scoreboard arrays.
     */
    private void LoadScoreboardSprites(){
        for (int i = 0; i <= rounds; i+=1){
            // determine file path of scoreboard sprites
            string filePathPlayerOne = "Scoreboard/player1_score" + i;
            string filePathPlayerTwo = "Scoreboard/player2_score" + i;

            // load and set sprites
            this.playerOneScore[i] = Resources.Load<Sprite>(filePathPlayerOne);
            this.playerTwoScore[i] = Resources.Load<Sprite>(filePathPlayerTwo);
        }
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
            this.mapSprites[i] = Resources.Load<Sprite>(filePath);
        }
    }

    /*
     * In order to load character portrait images for character select, the name of the 
     * file must follow a simple naming convention. 
     * EG: Character one = character1_dark.png and character1_light.png
     */
    private void LoadCharacterPortraitSprites(){

        // load all character portrait images
        for (int i = 0; i < characterCount; i += 1)
        {
            // determine starting index and file ID
            int index = i * 2;
            int fileID = i + 1;
  
            // determine file path of the portraits
            string filePathDark = "SelectionBox/character" + fileID + "_dark";
            string filePathLight = "SelectionBox/character" + fileID + "_light";

            // Set dark and light portraits to their corresponding indices.
            this.characterSelectSprites[index] = Resources.Load<Sprite>(filePathDark);
            this.characterSelectSprites[index+1] = Resources.Load<Sprite>(filePathLight);
        }
    }

    /*
    * In order to load character poster images for character select, the name of the 
    * file must follow a simple naming convention. 
    * EG: Character one = character1_left.png and character1_right.png
    */
    private void LoadCharacterPosterSprites()
    {

        // load all character portrait images
        for (int i = 0; i < characterCount; i += 1)
        {
            // determine starting index and file ID
            int index = i * 2;
            int fileID = i + 1;

            // determine file path of the portraits
            string filePathDark = "SelectionPoster/character" + fileID + "_left";
            string filePathLight = "SelectionPoster/character" + fileID + "_right";

            // Set dark and light portraits to their corresponding indices.
            this.characterPosterSprites[index] = Resources.Load<Sprite>(filePathDark);
            this.characterPosterSprites[index + 1] = Resources.Load<Sprite>(filePathLight);
        }

    }

    /*
     * Update the sprite image of the character portrait based on if it needs 
     * to be highlighted or unhighlighted and apply this to the corresponding 
     * character portrait
     * 
     *  - characterPortrait is the button for selecting the character
     *  - highlight is if we make the sprite dark or light
     *  - character is the ID. eg 1 = character 1
     */
    public void UpdateCharacterPortrait(GameObject characterPortrait, bool highlight, int character){
        // Get image component from the portrait
        Image imageScript = characterPortrait.GetComponent<Image>();

        // Determine the starting index of the character
        int index = (character-1)*2;

        // highlight or unhighlight the portrait. Dark = i, Light = i+1
        if (highlight){
            imageScript.sprite = characterSelectSprites[index + 1];
        }
        else{
            imageScript.sprite = characterSelectSprites[index];
        }
    }

    /*
     * Update the sprite image of the character poster based on if it needs 
     * to be highlighted or unhighlighted and apply this to the corresponding 
     * character portrait
     * 
     *  - characterPoster is the box area for displaying the character
     *  - isLeftSide is if we are updating left poster
     *  - character is the ID. eg 1 = character 1
     */
    public void UpdateCharacterPoster(GameObject characterPoster, bool isLeftSide, int character)
    {
        // Get image component from the portrait
        Image imageScript = characterPoster.GetComponent<Image>();

        // If we are making poster empty
        Color posterColor = new Color(255, 255, 255);
        if (character == 0)
        {
            posterColor.a = 0;
        }
        else
        {
            posterColor.a = 255;
        }

        // apply color to hide or display poster image
        imageScript.color = posterColor;

        // Determine the starting index of the character
        int index = (character != 0)? (character - 1) * 2 : 0;

        // highlight or unhighlight the portrait. right facing = i, left facing = i+1
        if (!isLeftSide)
        {
            imageScript.sprite = characterPosterSprites[index + 1];
        }
        else
        {
            imageScript.sprite = characterPosterSprites[index];
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

        // change the sprite to the coressponding character
        imageScript.sprite = characterSprite;
    }

    /*
     * Given the scoreboard game object, reset the sprite 
     * to the zero points sprite.
     * 
     *  - scoreboard is the game object of the given player
     *  - player represents which player's scoreboard
     */
    private void ResetScoreboard(GameObject scoreboard, int player){
        // Get image component from the scoreboard
        Image imageScript = scoreboard.GetComponent<Image>();

        // Get the corresponding character sprite
        Sprite defaultScore = (player == 1)? playerOneScore[0]:playerTwoScore[0];

        // change the sprite to the coressponding character
        imageScript.sprite = defaultScore;
    }


    public void ResetScoreboardPlayerOne(GameObject scoreboard){
        ResetScoreboard(scoreboard, 1);
    }

    public void ResetScoreboardPlayerTwo(GameObject scoreboard){
        ResetScoreboard(scoreboard, 2);
    }

    /*
     * Increase the scoreboard value based on which player and how many wins
     */
    private void IncrementScoreboard(GameObject scoreboard, int player){
        // Get image component from the scoreboard
        Image imageScript = scoreboard.GetComponent<Image>();

        // Array index of next scoreboard sprite
        int index = (player == 1) ? DataStore.p1Wins : DataStore.p2Wins;

        // Get the corresponding character sprite
        Sprite defaultScore = (player == 1) ? playerOneScore[index] : playerTwoScore[index];

        // change the sprite to the coressponding character
        imageScript.sprite = defaultScore;
    }

    public void UpdateScoreboardPlayerOne(GameObject scoreboard)
    {
        UpdateScoreboard(scoreboard, 1);
    }

    public void UpdateScoreboardPlayerTwo(GameObject scoreboard)
    {
        UpdateScoreboard(scoreboard, 2);
    }

    /*
     * Set the scoreboard to the appropriate sprites to match the 
     * wins for both players.
     */
    private void UpdateScoreboard(GameObject scoreboard, int player)
    {
        Debug.Log("try to update");
        // Get image component from the scoreboard
        Image imageScript = scoreboard.GetComponent<Image>();

        // Array index of next scoreboard sprite
        int index = (player == 1) ? DataStore.p1Wins : DataStore.p2Wins;

        // Get the corresponding character sprite
        Sprite defaultScore = (player == 1) ? playerOneScore[index] : playerTwoScore[index];

        // change the sprite to the coressponding character
        imageScript.sprite = defaultScore;

        Debug.Log("done");
    }
        


}
