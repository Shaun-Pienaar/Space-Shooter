using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHighScoreNameChanger : MonoBehaviour {

	void OnEnable(){
		Time.timeScale = 0;
	}

	void OnDisable(){
		Time.timeScale = 1;
	}

	public void ChangeName(string name){
        GameManager.newHighScoreName = name;
        GameManager.gameManager.SetHighScores();
	}

	public void SaveAndExit(){
        Destroy (gameObject);
	}
}
