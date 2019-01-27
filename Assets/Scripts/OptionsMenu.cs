using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	public AudioMixer audio;

	public bool waitingForKey;
	private Transform controls;
	private Event keyEvent;
	private KeyCode newKey;
	Text buttonText;

	void Start()
	{
		controls = transform.Find("Controls");
		waitingForKey = false;

		for(int i = 0; i < controls.childCount;i++)
		{
			if (controls.GetChild(i).name == "UpButton")
				controls.GetChild(i).Find("Text").GetComponent<Text>().text = PlayerSettings.Instance.Up.ToString();
			else if (controls.GetChild(i).name == "DownButton")
				controls.GetChild(i).Find("Text").GetComponent<Text>().text = PlayerSettings.Instance.Down.ToString();
			else if (controls.GetChild(i).name == "LeftButton")
				controls.GetChild(i).Find("Text").GetComponent<Text>().text = PlayerSettings.Instance.Left.ToString();
			else if (controls.GetChild(i).name == "RightButton")
				controls.GetChild(i).Find("Text").GetComponent<Text>().text = PlayerSettings.Instance.Right.ToString();
			else if (controls.GetChild(i).name == "InteractionButton")
				controls.GetChild(i).Find("Text").GetComponent<Text>().text = PlayerSettings.Instance.Interaction.ToString();
		}

	}

	public void OnGUI()
	{
		keyEvent = Event.current;

		if(keyEvent.isKey && waitingForKey)
		{
			newKey = keyEvent.keyCode;
			waitingForKey = false;
		}
	}

	public void StartAssigment(string keyName)
	{
		if (!waitingForKey)
			StartCoroutine(AssignKey(keyName));
	}

	public void SendText(Text text)
	{
		buttonText = text;
	}

	IEnumerator WaitForKey()
	{
		while (!keyEvent.isKey)
			yield return null;
	}

	public IEnumerator AssignKey(string keyName)
	{
		waitingForKey = true;
		yield return WaitForKey();

		switch (keyName)
		{
			case "Up":
				PlayerSettings.Instance.Up = newKey;
				buttonText.text = PlayerSettings.Instance.Up.ToString();
				PlayerPrefs.SetString("upKey", PlayerSettings.Instance.Up.ToString());
				break;
			case "Down":
				PlayerSettings.Instance.Down = newKey;
				buttonText.text = PlayerSettings.Instance.Down.ToString();
				PlayerPrefs.SetString("downKey", PlayerSettings.Instance.Down.ToString());
				break;
			case "Left":
				PlayerSettings.Instance.Left = newKey;
				buttonText.text = PlayerSettings.Instance.Left.ToString();
				PlayerPrefs.SetString("leftKey", PlayerSettings.Instance.Left.ToString());
				break;
			case "Right":
				PlayerSettings.Instance.Right = newKey;
				buttonText.text = PlayerSettings.Instance.Right.ToString();
				PlayerPrefs.SetString("rightKey", PlayerSettings.Instance.Right.ToString());
				break;
			case "Interaction":
				PlayerSettings.Instance.Interaction = newKey;
				buttonText.text = PlayerSettings.Instance.Interaction.ToString();
				PlayerPrefs.SetString("interactionKey", PlayerSettings.Instance.Interaction.ToString());
				break;
		}
	}

	public void SetVolume(float volume)
	{
		audio.SetFloat("volume", volume);
	}
}
