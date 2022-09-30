using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    private TMP_Text scoreText;
    private int score;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Current Score: " + score.ToString();
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        scoreText.text = "Current Score: " + score.ToString();
        // Debug.Log("Current Score: " + score);
    }
}
