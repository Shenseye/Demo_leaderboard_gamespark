using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour {

	public static GameObject instance;

	public float speed = 1.8f, forceToAdd = 350f, maxHeight = 5.5f, velocityUp = 4f, velocityDown = -90f;

	public AudioClip hit, fall;
	public AudioClip[] flap;

	public Animator anim;

	// Use this for initialization
	void Start () {
		instance = gameObject;
	}
	
	// Update is called once per frame
	void Update()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector3 (0, Mathf.Clamp(GetComponent<Rigidbody2D>().velocity.y, velocityDown, velocityUp), 0);
		transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -2f, 5.5f));
		if (GameState.instance.currentState != GameState.gameState.playing)
		{
			anim.SetBool("GameOver", false);
			GetComponent<Rigidbody2D>().isKinematic = true;
		}

		Playing();
		GameOver();
	}

	public void Playing()
	{
		if (GameState.instance.currentState == GameState.gameState.playing)
		{
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetComponent<Rigidbody2D>().velocity.y * 10f));
			GetComponent<Rigidbody2D>().isKinematic = false;
			transform.parent.transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
			if (Application.platform != RuntimePlatform.Android)
			{
				if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0))
				{
					for (int i = 0; i < flap.Length; i++)
					{
						GetComponent<AudioSource>().PlayOneShot(flap[i]);
					}
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceToAdd));
				}
			}
			else
			{
				if (Input.touchCount > 0)
				{
					if (Input.GetTouch(0).phase == TouchPhase.Began)
					{
						for (int i = 0; i < flap.Length; i++)
						{
							GetComponent<AudioSource>().PlayOneShot(flap[i]);
						}
						GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceToAdd));
					}
				}
			}
		}
	}

	public void GameOver()
	{
		if (GameState.instance.currentState == GameState.gameState.gameOver)
		{
			anim.SetBool("GameOver", true);
		}
	}

	public void OnCollisionEnter2D(Collision2D col)
	{
		if (GameState.instance.currentState != GameState.gameState.gameOver)
		{
			if (col.gameObject.CompareTag("Level"))
			{
				if (GameState.instance.currentState == GameState.gameState.playing)
				{
					GetComponent<AudioSource>().PlayOneShot(hit);
					GameState.instance.currentState = GameState.gameState.gameOver;
				}
			}

			if (col.gameObject.CompareTag("Pencil"))
			{
				GetComponent<AudioSource>().PlayOneShot(hit);
				GetComponent<AudioSource>().PlayOneShot(fall);
				col.collider.enabled = false;
				GameState.instance.currentState = GameState.gameState.gameOver;
			}
		}
	}
}
