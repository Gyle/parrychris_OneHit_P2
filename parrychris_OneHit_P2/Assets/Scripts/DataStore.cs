using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This static class is only used for holding data. By doing so, it allows data 
 * to be carried over between different scenes.
 * 
 * To access the data:      DataStore.Variable
 * To write to the data:    DataStore.Variable = data;
 */
public static class DataStore
{
    // declare the data that will be carried over between scenes.
    private static int playerOneCharacter, playerTwoCharacter, map;
    public static bool ready = false;
    public static bool controller = false;

    // This variable represents which character player one selected
    public static int PlayerOneCharacter{
        get
        {
            return playerOneCharacter;
        }
        set{
            playerOneCharacter = value;
        }
    }

    // This variable represents which character player two selected
    public static int PlayerTwoCharacter
    {
        get
        {
            return playerTwoCharacter;
        }
        set
        {
            playerTwoCharacter = value;
        }
    }

    // This variable represents which map was selected
    public static int Map
    {
        get
        {
            return map;
        }
        set
        {
            map = value;
        }
    }
}
