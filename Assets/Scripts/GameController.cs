using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController gameController;
	public static bool paused;

	int score;
	int lives;

	public int Score {
		get { return score; }
	}

	public int Lives {
		get { return lives; }
	}

	[Range(1,21)]
	public int level = 1;

	public Text scoreText;
	public Text livesText;
	public Text restartText;
	public Text gameOverText;
	public Text loadingText;
	public Canvas endOfLevelScreen;
	public Text completedLevelText;
	public Text livesLeftText_LevelEnd;
	public Text currentScoreText_LevelEnd;
	public Canvas deathScreen;
	public Text livesLeftText_Restart;
	public Text currentScoreText_Restart;
	public Canvas pauseScreen;
	public AudioSource backGroundMusic;
	public GameObject[] powerUps;
	public GameObject[] hazards;
	public GameObject enemy;
	public GameObject hardEnemy;
	public GameObject newPlayer;
	public int spawnCount = 1;

	Vector3 spawnLocation;
	Transform playerTransform;

	bool levelEnded;
	bool gameOver;
	bool canRestart;

	public bool multiShot;
	public int isSplit;

	public float timeBetweenHazards = 0.7f;
	public float timeBetweenWaves = 2f;

	void Awake(){
		gameController = this;
		backGroundMusic = GetComponent<AudioSource> ();
		isSplit = 1;
		playerTransform = GameObject.FindGameObjectWithTag ("PlayerMain").GetComponent<Transform> ();
	}

	void Start(){
		if (GameManager.hardMode)
			Time.timeScale = 2;
		gameOver = false;
		gameOverText.text = string.Empty;
		canRestart = false;
		restartText.text = string.Empty;
		score = 0;
		SetScoreText ();
		lives = 3;
		SetLivesText ();
		spawnCount = 30 - level;
		StartCoroutine(SpawnWaves ());
		StartCoroutine (SpawnEnemies ());
		StartCoroutine (SpawnHardEnemies ());
	}

	Coroutine currentLoadingRoutine;
	int rank = -1;

	void Update(){
		if (!gameOver) {
			if (Input.GetButtonDown ("Fire3")) {
				if (paused)
					Resume ();
				else
					Pause ();
			}
		}
		if(levelEnded){
			StopAllCoroutines ();
			StartCoroutine (LoadNextLevel ());
			StartCoroutine (LoadingRoutine ());
			loadingText.gameObject.SetActive (true);
			levelEnded = false;
		}
		if(canRestart){
			if(Input.GetButtonDown("Fire2")){
				SceneManager.LoadScene ("Menu");
			}
		}
		if(gameOver){
            StopAllCoroutines ();
			canRestart = true;
			restartText.text = "Press 'Enter' for Main Menu";
			loadingText.gameObject.SetActive (false);
		}
	}

	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds (2f);
		for (int i = 0; i < level; i++) {
			for (int j = 0; j < spawnCount; j++) {
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				spawnLocation = new Vector3 (Random.Range (-6f, 6f), 0f, 19.5f);
				Instantiate (hazard, spawnLocation, Quaternion.identity);
				yield return new WaitForSeconds (timeBetweenHazards);
			}
			yield return new WaitForSeconds (timeBetweenWaves);
		}
		if(!gameOver)
			levelEnded = true;
	}

	IEnumerator SpawnEnemies(){
		Vector3 randomPos;
		yield return new WaitForSeconds (4f);
		while(true){
			if (gameOver) {
				break;
			}
			if(levelEnded){
				break;
			}
			randomPos = new Vector3 (Random.Range (-6f, 6f), 0f, 18.5f);
			Instantiate (enemy, randomPos, Quaternion.identity);
			float waitTime = (10f - level/2f) + (Random.Range (1, 4) / 2f);
			if (GameController.gameController.level < 5)
				waitTime *= 0.5f;
			else if (GameController.gameController.level < 10)
				waitTime *= 0.75f;
			yield return new WaitForSeconds (waitTime);
		}
	}

	IEnumerator SpawnHardEnemies(){
		Vector3 randomPos;
		yield return new WaitForSeconds (10f);
		while(true){
			if (gameOver) {
				break;
			}
			if(levelEnded){
				break;
			}
			randomPos = new Vector3 (Random.Range (-6f, 6f), 0f, 18.5f);
			Instantiate (hardEnemy, randomPos, Quaternion.identity);
			float waitTime = 13 - level / 2;
			yield return new WaitForSeconds (waitTime);
		}
	}

	void SetScoreText(){
		scoreText.text = "Score: " + score;
	}

	void SetLivesText(){
		if(lives == 1)
			livesText.text = lives + " Life remaining";
		else
			livesText.text = lives + " Lives remaining";
	}

	public void AddScore(int scoreToAdd){
		score += scoreToAdd;
		SetScoreText();
	}

	public void AddLife(){
		lives += 1;
		SetLivesText ();
	}

	public bool LoseLife(){
		lives -= 1;
		SetLivesText ();
		if (lives == 0)
			return false;
		else
			return true;
	}

	public void GameOver(){
		gameOver = true;
		if(level == 21)
			gameOverText.text = "You Won";
		else
			gameOverText.text = "Game Over";
		rank = GameManager.gameManager.CheckHighScores (Score);
        GameManager.newHighScoreRank = rank;
        GameManager.newHighScore = score;
		restartText.text = "Your Score: " + score;
		loadingText.gameObject.SetActive (true);
	}

	IEnumerator LoadingRoutine(){
		yield return new WaitForSeconds (2f);
		for(int i = 0; i < 10; i++){
			loadingText.text = loadingText.text + ".";
			yield return new WaitForSeconds (2f);
		}
	}

	IEnumerator LoadNextLevel(){
		yield return new WaitForSeconds (4f);
		UpdateEndOfLevelScreen ();
		endOfLevelScreen.gameObject.SetActive (true);
		yield return new WaitForSeconds (5f);
		endOfLevelScreen.gameObject.SetActive (false);
		loadingText.gameObject.SetActive (false);
		StartNextLevel ();
	}

	void UpdateEndOfLevelScreen(){
		completedLevelText.text = "Congratulations!  You Completed Level " + level;
		livesLeftText_LevelEnd.text = lives + " Lives remaining";
		currentScoreText_LevelEnd.text = "Your Score: " + score;
	}

	void StartNextLevel(){
		StopAllCoroutines ();
		level += 1;
		if (level == 21)
			gameController.GameOver ();
		loadingText.text = "Loading";
		StartCoroutine (SpawnWaves ());
		StartCoroutine (SpawnEnemies ());
		StartCoroutine (SpawnHardEnemies ());
	}

	public void Respawn(){
		StopAllCoroutines ();
		StartCoroutine (SpawnNewPlayer ());
	}

	IEnumerator SpawnNewPlayer(){
		livesLeftText_Restart.text = lives + " Lives remaining";
		currentScoreText_Restart.text = "Score: " + score;
		deathScreen.gameObject.SetActive (true);
		yield return new WaitForSeconds (5f);
		deathScreen.gameObject.SetActive (false);
		Instantiate (newPlayer, new Vector3 (0f, 0f, 0f), Quaternion.identity, playerTransform);
		RestartLevel();
	}

	void RestartLevel(){
		StartCoroutine (SpawnWaves ());
		StartCoroutine (SpawnEnemies ());
		StartCoroutine (SpawnHardEnemies ());
	}

	public void Resume(){
		pauseScreen.gameObject.SetActive (false);
		backGroundMusic.Play ();
		if (GameManager.hardMode)
			Time.timeScale = 2;
		else
			Time.timeScale = 1;
		paused = false;
	}

	void Pause(){
		pauseScreen.gameObject.SetActive (true);
		backGroundMusic.Pause ();
		Time.timeScale = 0;
		paused = true;
	}

	public void LoadMenu(){
		SceneManager.UnloadSceneAsync ("MainGame");
		SceneManager.LoadScene ("Menu");
	}

	public void Quit(){
		Application.Quit ();
	}

	public void Save(){
		
	}

	public void Load(){
		
	}
}

[System.Serializable]
class GameData{
	
	public int Score {
		get;
		set;
	}
	public int Level {
		get;
		set;
	}
	public int Lives {
		get;
		set;
	}

	public GameData(int score, int level, int lives){
		Score = score;
		Level = level;
		Lives = lives;
	}
}