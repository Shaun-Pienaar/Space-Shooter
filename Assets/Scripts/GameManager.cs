using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour {

	public static bool hardMode;
	public static string newHighScoreName = string.Empty;
    public static int newHighScore = 0;
    public static int newHighScoreRank = -1;
	public static GameManager gameManager;

	public GameObject newHSNC;

	public HighScores highScores;

	void OnEnable(){
		highScores = LoadHighScores ();
	}

	// Use this for initialization
	void Start () {
		if (gameManager == null) {
			DontDestroyOnLoad (gameObject);
			gameManager = this;
		}
		else{
			if (gameManager != this)
				Destroy (gameObject);
		}
	}

	void OnDisable(){
		SaveHighScores (highScores);
	}

	HighScores LoadHighScores(){
		BinaryFormatter bf = new BinaryFormatter ();
		HighScores returnVal = new HighScores();
		if (File.Exists (Application.persistentDataPath + "/HighScores.dat")) {
			FileStream fs = new FileStream (Application.persistentDataPath + "/HighScores.dat", FileMode.Open, FileAccess.Read);
			returnVal = bf.Deserialize (fs) as HighScores;
			fs.Close ();
		}
		else
			returnVal = new HighScores ();
		return returnVal;
	}

	void SaveHighScores(HighScores highScoresToSave){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fs = new FileStream (Application.persistentDataPath + "/HighScores.dat", FileMode.OpenOrCreate, FileAccess.Write);
		bf.Serialize (fs, highScoresToSave);
		fs.Close ();
	}

	public int CheckHighScores(int newScore){
		int rank = highScores.CheckHighScore (newScore);
		if(rank != -1){
			Instantiate (newHSNC, new Vector3 (0f, 0f, 0f), Quaternion.identity);
		}
		return rank;
	}

	public void SetHighScores(){
		if(newHighScoreRank != -1){
			highScores.SetNewHighScore (newHighScoreRank, newHighScore, newHighScoreName);
		}
        newHighScoreRank = -1;
        newHighScore = 0;
        newHighScoreName = string.Empty;
    }
}

[System.Serializable]
public class HighScores{
	public string[] highScoreNames = new string[10];
	public int[] highScoreScores = new int[10];

	public HighScores(){
		for (int i = 0; i < 10; i++) {
			highScoreNames [i] = "Not Set";
			highScoreScores [i] = 0;
		}
	}
		
	public int CheckHighScore(int scoreToCheck){
		for(int i = 0; i < 10; i++){
			if (scoreToCheck > highScoreScores [i])
				return i;
		}
		return -1;
	}
		
	public void SetNewHighScore(int index, int score, string name){
		for(int i = 9; i > index; i--){
			highScoreNames [i] = highScoreNames [i - 1];
			highScoreScores [i] = highScoreScores [i - 1];
		}
		highScoreNames [index] = name;
		highScoreScores [index] = score;
	}
}