using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public GameObject fadeOutUI;

	public void BackToMenu() {
		Invoke("_BackToMenu", 1f);
		fadeOutUI.SetActive(true);
	}

	public void _BackToMenu() {
		SceneManager.LoadScene("Menu");
	}
}
