using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameCountDown : MonoBehaviour {

    public GameObject players;

    private float countDownTime = 4.0f;
    private MergedPlayerBehaviour[] playerScripts;
    private GameObject[] countDownObjects = new GameObject[4];

	// Use this for initialization
	void Start () {
        playerScripts = players.GetComponentsInChildren<MergedPlayerBehaviour>();
        //disable the control scripts for each player
        // foreach(MergedPlayerBehaviour script in playerScripts){
        //     //script.enabled = false;
        // }

        countDownObjects[0] = GameObject.Find("CountDown3");
        countDownObjects[1] = GameObject.Find("CountDown2");
        countDownObjects[2] = GameObject.Find("CountDown1");
        countDownObjects[3] = GameObject.Find("CountDownFight");

        foreach (GameObject obj in countDownObjects){
            obj.SetActive(false);
        }
        Debug.Log(countDownObjects[0]);
        countDownObjects[0].SetActive(true);

	}
	
	// Update is called once per frame
	void Update () {
        countDownTime -= Time.deltaTime;

        if(countDownTime<=0){
            countDownObjects[3].SetActive(false);
        }
        else if(countDownTime<=1){
            DataStore.ready = true;
            //reenable player controls
            // foreach (MergedPlayerBehaviour script in playerScripts)
            // {
            //     script.enabled = true;
            // }
            countDownObjects[2].SetActive(false);
            countDownObjects[3].SetActive(true);
        }else if (countDownTime <= 2){
            countDownObjects[1].SetActive(false);
            countDownObjects[2].SetActive(true);
        }else if (countDownTime <= 3){
            countDownObjects[0].SetActive(false);
            countDownObjects[1].SetActive(true);
        }
	}
}
