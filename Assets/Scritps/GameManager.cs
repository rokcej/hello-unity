using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;

    public Text scoreText;
    public Text finalScoreText;
    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int amount) {
        score += amount;
        scoreText.text = "SCORE: " + score.ToString();
        finalScoreText.text = "FINAL SCORE: " + score.ToString();
    }

    public void EndGame() {
        Invoke("_EndGame", 2f);
	}

    private void _EndGame() {
        gameOverUI.SetActive(true);
	}

}
