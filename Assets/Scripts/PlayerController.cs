using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	[Range(0, 1)]
	public float rotationSpeed;

	void Update()
	{
		Movement();


	}

	private void Movement()
	{
		Vector3 deltaPosition = Vector3.zero;
		if (Input.GetKey(PlayerSettings.Instance.Up))
		{
			deltaPosition += Vector3.forward * speed * Time.deltaTime;
		}
		else if (Input.GetKey(PlayerSettings.Instance.Down))
		{
			deltaPosition += Vector3.back * speed * Time.deltaTime;
		}

		if (Input.GetKey(PlayerSettings.Instance.Left))
		{
			deltaPosition += Vector3.left * speed * Time.deltaTime;
		}
		else if (Input.GetKey(PlayerSettings.Instance.Right))
		{
			deltaPosition += Vector3.right * speed * Time.deltaTime;
		}
		transform.LookAt(Vector3.Lerp(transform.position + transform.forward, transform.position + deltaPosition, rotationSpeed));
		transform.position += deltaPosition;
	}

	private void OnTriggerEnter(Collider other)
	{

	}
	private void OnTriggerExit(Collider other)
	{
		
	}
}
