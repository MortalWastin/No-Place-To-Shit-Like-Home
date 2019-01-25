using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Vector3 moveInput = Vector3.zero;
	float speed = 7f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		moveInput = new Vector3(h, 0.0f, v) * speed;

		transform.position += moveInput * Time.deltaTime;

	}
}
