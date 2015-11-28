using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class ButtonEvents : MonoBehaviour
{
	public enum ButtonName { playButton, normalMode, hardMode, harderMode, hardestMode, insaneMode, leaderBoardsButton, leaderBoardsButtonMenus, rateButton, facebookPost, mainMenu} 
	public ButtonName thisButton;

	private string levelType;
	private string currentLevel;

	void Start()
	{

		switch (Application.loadedLevel)
		{
			case(0):
			levelType = "mainMenu";
			currentLevel = "MainMenu";
			break;
			case (1):
			levelType = "Normal Mode";
			currentLevel = "normalmode";
			break;
			case (2):
			levelType = "Hard Mode";
			currentLevel = "hardmode";
			break;
			case (3):
			levelType = "Harder Mode";
			currentLevel = "hardermode";
			break;
			case (4):
			levelType = "Hardest Mode";
			currentLevel = "hardestmode";
			break;
			case (5):
			levelType = "Insane Mode";
			currentLevel = "insanemode";
			break;
			case (6):
			levelType = "mainMenu";
			currentLevel = "MainMenu";
			break;
		}
	}

	public void OnClick()
	{
		switch (thisButton)
		{
			case ButtonName.playButton:

			if (Application.loadedLevel == 0 || Application.loadedLevel == 6)
			{
				GameState.instance.currentState = GameState.gameState.levelSelect;
			}
			else
			{
				Application.LoadLevel(Application.loadedLevel);
			}
			break;
			case ButtonName.normalMode:
			Application.LoadLevel(1);
			break;
			case ButtonName.hardMode:
			Application.LoadLevel(2);
			break;
			case ButtonName.harderMode:
			Application.LoadLevel(3);
			break;
			case ButtonName.hardestMode:
			Application.LoadLevel(4);
			break;
			case ButtonName.insaneMode:
			Application.LoadLevel(5);
			break;
			case ButtonName.leaderBoardsButton:
			//This will get the leaderboard for the current level we are on, this is obviously depending on how you name your Scene files.
			break;
			case ButtonName.leaderBoardsButtonMenus:
			//This will get any leaderboard we specify

			break;
			case ButtonName.rateButton:
			Application.OpenURL("market://details?id=ie.shaneobrien.tappybird");
			break;
			case ButtonName.facebookPost:
			//This is how we post to facebook about our app
			
			break;
			case ButtonName.mainMenu:
			Application.LoadLevel(6);
			break;
		}
	}
}
