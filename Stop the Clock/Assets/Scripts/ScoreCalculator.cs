using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCalculator : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public float CalculateScore(float score, int combo)
    {
        score += 1000 + (combo * 25 * (combo * 0.5f));
        UpdateScoreAndComboText(score, combo);
        return score;
    }

    public void UpdateScoreAndComboText(float score, int combo)
    {
        scoreText.text = score.ToString("0000000");
        comboText.text = "Combo: " + combo;
    }
}
