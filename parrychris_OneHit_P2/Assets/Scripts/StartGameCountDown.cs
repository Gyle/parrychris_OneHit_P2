using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameCountDown : MonoBehaviour {

    public GameObject players;

    private float countDownTime = 5.0f;
    private GameObject[] countDownObjects = new GameObject[4];
    private AudioSource buzzer;
    private AudioSource startGame;
    private int beatsSounded = 0;

	// Use this for initialization
	void Start () {
        buzzer = GetComponents<AudioSource>()[0];
        startGame = GetComponents<AudioSource>()[1];
        countDownObjects[0] = GameObject.Find("CountDown3");
        countDownObjects[1] = GameObject.Find("CountDown2");
        countDownObjects[2] = GameObject.Find("CountDown1");
        countDownObjects[3] = GameObject.Find("CountDownFight");

        foreach (GameObject obj in countDownObjects){
            obj.SetActive(false);
        }
        beatsSounded = 0;
	}
	
	// Update is called once per frame
	void Update () {
        countDownTime -= Time.deltaTime;

        if(countDownTime<=0){
            countDownObjects[3].SetActive(false);
        }
        else if(countDownTime<=1){
            DataStore.ready = true;
            if (beatsSounded == 3)
            {
                startGame.Play();
                beatsSounded++;
            }
            countDownObjects[2].SetActive(false);
            countDownObjects[3].SetActive(true);
        }else if (countDownTime <= 2){
            if (beatsSounded == 2)
            {
                buzzer.Play();
                beatsSounded++;
            }
            countDownObjects[1].SetActive(false);
            countDownObjects[2].SetActive(true);
        }else if (countDownTime <= 3){
            if (beatsSounded == 1)
            {
                buzzer.Play();
                beatsSounded++;
            }
            countDownObjects[0].SetActive(false);
            countDownObjects[1].SetActive(true);
        }else if (countDownTime<=4){
            if (beatsSounded == 0)
            {
                buzzer.Play();
                beatsSounded++;
            }
            countDownObjects[0].SetActive(true);
        }
	}
}
