﻿using UnityEngine;

public class Camera : MonoBehaviour
{
	Vector2 rotation = new Vector2(0, 0);
	public float speed = 3;

	void Update()
	{
		rotation.y += Input.GetAxis("Mouse X");
		rotation.x += -Input.GetAxis("Mouse Y");
		transform.eulerAngles = (Vector2)rotation * speed;
	}
}