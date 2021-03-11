using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public GameObject fadeOutUI;
	public void PlayGame() {
		fadeOutUI.SetActive(true);
		Invoke("_PlayGame", 1f);
	}
	private void _PlayGame() {
		SceneManager.LoadScene("Game");
	}

	public void ShowInfo() {
		fadeOutUI.SetActive(true);
		Invoke("_ShowInfo", 1f);
	}

	private void _ShowInfo() {
		SceneManager.LoadScene("Info");
	}

	public void ExitGame() {
		Application.Quit();
	}
}
