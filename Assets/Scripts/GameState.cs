using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour
{

	public static GameState instance;
	public enum gameState { mainMenu, levelSelect, gameStart, playing, gameOver }
	public gameState currentState;

	//Playing
	public GameObject pencil, player;
	public int numberOfPencils, maxNumberOfPencils = 5;
	public float distanceOfPencils = 3.297882f, addedDistance = 0f;
	public bool movePencils = false;
	public int playerScore = 0;

	//GUI
	public UIPanel initialLoadPanel, loggingInPanel, mainMenuPanel, levelSelectPanel, gameStartPanel, scorePanel, gameOverPanel;
	public UILabel scoreLabel, endScoreLabel, endBestLabel;
	public GameObject bronze, silver, gold, platinum;
	public AudioClip fail, successClip;

	public void Awake()
	{
		PlayerPrefs.DeleteAll();
		instance = this;
	}

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.Home))
		{
			Application.Quit();

		}
		CheckState();
	}

	public void CheckState()
	{
		switch (currentState)
		{
			case gameState.mainMenu:
			MainMenu();
			break;
			case gameState.levelSelect:
			LevelSelect();
			break;
			case gameState.gameStart:
			GameStart();
			break;
			case gameState.playing:
			Playing();
			break;
			case gameState.gameOver:
			GameOver();
			break;
		}
	}

	public void MainMenu()
	{
		mainMenuPanel.gameObject.SetActive(true);
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void LevelSelect()
	{
		mainMenuPanel.gameObject.SetActive(false);
		levelSelectPanel.gameObject.SetActive(true);
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			levelSelectPanel.gameObject.SetActive(false);
			currentState = gameState.mainMenu;
		}
	}

	public void GameStart()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(6);

		}
		gameStartPanel.gameObject.SetActive(true);
		if (numberOfPencils != 1)
		{
			Instantiate(pencil, new Vector3(6f, Random.Range(-0.4f, 4.0f), 0), transform.rotation);
			numberOfPencils += 1;
			addedDistance = 6;
		}
		if (Application.platform != RuntimePlatform.Android)
		{
			if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)|| Input.GetKeyDown(KeyCode.JoystickButton0))
			{
				currentState = gameState.playing;
			}
		}
		else
		{
			if (Input.touchCount > 0)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					currentState = gameState.playing;
				}
			}
		}
	}

	public void Playing()
	{
		gameStartPanel.gameObject.SetActive(false);
		scorePanel.gameObject.SetActive(true);
		while (numberOfPencils < maxNumberOfPencils)
		{
			Instantiate(pencil, new Vector3(addedDistance + distanceOfPencils, Random.Range(-0.4f, 4.0f), 0), transform.rotation);
			numberOfPencils += 1;
			addedDistance += distanceOfPencils;
		}
		scoreLabel.text = playerScore.ToString();
	}

	public void GameOver()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(6);
		}
		if (PlayerPrefs.GetInt(Application.loadedLevelName + "BestScore") != null)
		{
			endBestLabel.text = PlayerPrefs.GetInt(Application.loadedLevelName+"BestScore").ToString();
		}
		scorePanel.gameObject.SetActive(false);
		gameOverPanel.gameObject.SetActive(true);
		endScoreLabel.text = playerScore.ToString();

		if (playerScore > PlayerPrefs.GetInt(Application.loadedLevelName + "BestScore"))
		{
			Debug.Log("Posting Score");
			PostScores(Application.loadedLevelName, playerScore);
			PlayerPrefs.SetInt(Application.loadedLevelName + "BestScore", playerScore);
		}
	}

	public void PostScores(string levelName, int score)
	{
        new GameSparks.Api.Requests.LogEventRequest_postScore().Set_score(score).Send((response) => 
        {
            if (response.HasErrors)
            {
                Debug.Log("Failed");
            }
            else
            {
                Debug.Log("Successful");
            }
        });
	}

	public void PostAchievements(int score)
	{
		
	}
}
