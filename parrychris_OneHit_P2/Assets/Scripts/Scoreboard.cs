using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour {
    public GameObject playerOneScore;
    public GameObject playerTwoScore;

	// Use this for initialization
	void Start () {
        DataStore.spriteManager.UpdateScoreboardPlayerOne(playerOneScore);
        DataStore.spriteManager.UpdateScoreboardPlayerTwo(playerTwoScore);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
