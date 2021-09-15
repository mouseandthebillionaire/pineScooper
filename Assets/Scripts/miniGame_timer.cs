using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniGame_timer : MonoBehaviour {

	public Text timerText;
	public static float timer;
    

	// Use this for initialization
	void Start () {
		ResetTimer();
	}

	public static void ResetTimer(){
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!miniGame.gameOver)
		{
			timer += Time.deltaTime;
			timerText.text = timer.ToString("F1");
		} else {
			if(miniGame.isFinished()){
				miniGame.S.numPoopText.text = "YOU";
				timerText.text = "WIN!";
			}
		}
	}
}
