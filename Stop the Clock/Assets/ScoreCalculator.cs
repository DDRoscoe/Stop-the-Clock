using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCalculator : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public float CalculateScore(float score, int combo)
    {
        score += 1000 + (combo * 25 * (combo * 0.5f));
        UpdateScoreText(score, combo);
        return score;
    }

    private void UpdateScoreText(float score, int combo)
    {
        scoreText.text = score.ToString("0000000");
    }
}
