using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public bool mirror = false;
	public Transform goToFollow;
	// Use this for initialization
	void Start()
	{
		if (mirror)
		{
			Matrix4x4 mat = Camera.main.projectionMatrix;
			mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
			Camera.main.projectionMatrix = mat;
		}
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector3(goToFollow.position.x + 1.19f, transform.position.y, transform.position.z);
	}
}
