using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresReady : MonoBehaviour {

	public Text HighScore1Name;
	public Text HighScore1Score;

	public Text HighScore2Name;
	public Text HighScore2Score;

	public Text HighScore3Name;
	public Text HighScore3Score;

	public Text HighScore4Name;
	public Text HighScore4Score;

	public Text HighScore5Name;
	public Text HighScore5Score;

	public Text HighScore6Name;
	public Text HighScore6Score;

	public Text HighScore7Name;
	public Text HighScore7Score;

	public Text HighScore8Name;
	public Text HighScore8Score;

	public Text HighScore9Name;
	public Text HighScore9Score;

	public Text HighScore10Name;
	public Text HighScore10Score;

	void OnEnable(){
		HighScore1Name.text = GameManager.gameManager.highScores.highScoreNames [0];
		HighScore1Score.text = GameManager.gameManager.highScores.highScoreScores [0].ToString ();

		HighScore2Name.text = GameManager.gameManager.highScores.highScoreNames [1];
		HighScore2Score.text = GameManager.gameManager.highScores.highScoreScores [1].ToString ();

		HighScore3Name.text = GameManager.gameManager.highScores.highScoreNames [2];
		HighScore3Score.text = GameManager.gameManager.highScores.highScoreScores [2].ToString ();

		HighScore4Name.text = GameManager.gameManager.highScores.highScoreNames [3];
		HighScore4Score.text = GameManager.gameManager.highScores.highScoreScores [3].ToString ();

		HighScore5Name.text = GameManager.gameManager.highScores.highScoreNames [4];
		HighScore5Score.text = GameManager.gameManager.highScores.highScoreScores [4].ToString ();

		HighScore6Name.text = GameManager.gameManager.highScores.highScoreNames [5];
		HighScore6Score.text = GameManager.gameManager.highScores.highScoreScores [5].ToString ();

		HighScore7Name.text = GameManager.gameManager.highScores.highScoreNames [6];
		HighScore7Score.text = GameManager.gameManager.highScores.highScoreScores [6].ToString ();

		HighScore8Name.text = GameManager.gameManager.highScores.highScoreNames [7];
		HighScore8Score.text = GameManager.gameManager.highScores.highScoreScores [7].ToString ();

		HighScore9Name.text = GameManager.gameManager.highScores.highScoreNames [8];
		HighScore9Score.text = GameManager.gameManager.highScores.highScoreScores [8].ToString ();

		HighScore10Name.text = GameManager.gameManager.highScores.highScoreNames [9];
		HighScore10Score.text = GameManager.gameManager.highScores.highScoreScores [9].ToString ();
	}
}
