using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour {

	public static PlayerSettings Instance;

	public KeyCode Up;
	public KeyCode Down;
	public KeyCode Right;
	public KeyCode Left;
    public KeyCode Interaction;

	public void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			//DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(this.gameObject);
			return;
		}

		Up = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("upKey","W"));
		Down = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("downKey", "S"));
		Right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
		Left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
		Interaction = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("interactionKey", "E"));
	}
}
