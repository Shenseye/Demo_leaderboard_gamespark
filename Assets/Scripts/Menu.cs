using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {


    private static Vector2 BUTTON_SIZE = new Vector2(100, 50);
    
	private Rect buttonPositionShowAds;
	private Rect buttonPositionHideAds;

	void Start() {

		buttonPositionShowAds = new Rect(
			(Screen.width - BUTTON_SIZE.x) / 2,
			(Screen.height - BUTTON_SIZE.y) / 2,
			BUTTON_SIZE.x, BUTTON_SIZE.y);

		buttonPositionHideAds = new Rect(
			buttonPositionShowAds.x, buttonPositionShowAds.y + BUTTON_SIZE.y * 3 / 2,
			buttonPositionShowAds.width, buttonPositionShowAds.height);
    }

	void OnEnable() {

	}

	void OnDisable() {

	}

    void OnGUI () {
		if (GUI.Button(buttonPositionShowAds, "Show Ads")) {

		}

		if (GUI.Button(buttonPositionHideAds, "Hide Ads")) {

		}
    }
}
