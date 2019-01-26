using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public int fontSizeLimit;
	private float startTime = 15;
	private float currentTime = 0;
	private int previousTime;
	private Text countdownText;
	public GameObject GameOver_UI;

	void Start()
	{
		currentTime = startTime;
		countdownText = GetComponent<Text>();
	}

	void Update()
	{
		previousTime = (int)(currentTime + 0.9f);
		if (currentTime > 0 && previousTime == (int)currentTime)
		{
			if(currentTime < 11)
			countdownText.fontSize = fontSizeLimit;
			countdownText.text = currentTime.ToString("0");
		}
		currentTime -= 1 * Time.deltaTime;

		if (currentTime <= 0)
			GameOver_UI.SetActive(true);

		if(currentTime <= 10 && currentTime > 0 && countdownText.fontSize > 50)
		{
			countdownText.fontSize -= 1;
			Debug.Log(countdownText.fontSize);
		}
	}
}
