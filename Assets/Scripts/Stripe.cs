using UnityEngine;
using System.Collections;

public class Stripe : MonoBehaviour
{

	public Animator anim;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (GameState.instance.currentState == GameState.gameState.gameOver)
		{
			anim.enabled = false;
		}
	}
}
