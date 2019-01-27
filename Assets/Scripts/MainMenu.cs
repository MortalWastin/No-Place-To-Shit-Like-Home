using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame()
	{
		this.transform.parent.gameObject.SetActive(false);
		GameObject.Find("GameManager").GetComponent<GameManager>().StartGame();
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
