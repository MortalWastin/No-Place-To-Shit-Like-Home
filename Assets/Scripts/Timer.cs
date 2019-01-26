﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float startTime = 60;
	private float currentTime = 0;
	private Text countdownText;
	// Use this for initialization
	void Start()
	{
		currentTime = startTime;
		countdownText = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
	{

		currentTime -= 1 * Time.deltaTime;

		if (currentTime > 1)
			countdownText.text = currentTime.ToString("0");
		else
		{
			countdownText.fontSize = 50;
			countdownText.text = "Time's Up!";
		}

		if (currentTime <= 0)
			this.gameObject.SetActive(false);
	}
}
