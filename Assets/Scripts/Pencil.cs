using UnityEngine;
using System.Collections;

public class Pencil : MonoBehaviour
{
	
	public AudioClip coin;

	private bool m_flownThrough = false;
	private bool m_moveUp;
	private float m_speed;

	// Use this for initialization
	void Start()
	{
		m_speed = Random.Range(0.4f, 0.75f);
		if (GameState.instance.movePencils)
		{
			if (transform.position.y < 2.2f)
			{
				m_moveUp = true;
			}
			else
			{
				m_moveUp = false;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		MovePencils();
		CheckDistance();
	}

	public void MovePencils()
	{
		if (GameState.instance.movePencils && GameState.instance.currentState != GameState.gameState.gameOver)
		{
			if (transform.position.y < -0.2f)
			{
				m_moveUp = true;
			}
			else if (transform.position.y > 3.8f)
			{
				m_moveUp = false;
			}

			if (m_moveUp)
			{
				transform.Translate(new Vector3(0, m_speed, 0) * Time.deltaTime);
			}
			else
			{
				transform.Translate(new Vector3(0, -m_speed, 0) * Time.deltaTime);
			}
		}
	}

	public void CheckDistance()
	{
		float distance;
		distance = Vector3.Distance(transform.position, BirdMovement.instance.transform.position);

		if (distance > 8f && m_flownThrough)
		{
			DestroyGO();
		}
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player") && !m_flownThrough)
		{
			GetComponent<AudioSource>().PlayOneShot(coin);
			GameState.instance.playerScore += 1;
			m_flownThrough = true;
		}
	}

	public void DestroyGO()
	{
		GameState.instance.numberOfPencils -= 1;
		Destroy(gameObject);
	}

	public void DestroyYourself()
	{
		Destroy(gameObject);
	}
}
