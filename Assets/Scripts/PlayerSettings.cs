using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour {

	public static PlayerSettings Instance;

	public KeyCode Up;
	public KeyCode Down;
	public KeyCode Right;
	public KeyCode Left;

	public void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(this.gameObject);
			return;
		}
	}

}
