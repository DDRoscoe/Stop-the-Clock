using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainHand : MonoBehaviour
{   
    public Transform pivotPoint;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI countdownText;
    public float rotationSpeed = 60f;
    public GameObject[] numbers;
    public bool correctTiming;
    public bool isRotatingClockwise = true;
    public bool gameStart = false;
    public bool gameOver;
    public float score;
    public int combo;

    private int multiplier;
    private int randomIndex;

    private ScoreCalculator scoreCalculatorScript;

    // Start is called before the first frame update
    void Start()
    {
        scoreCalculatorScript = GameObject.Find("Score").GetComponent<ScoreCalculator>();
        InitializeValues();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart && gameOver == false) 
        {
            if (pivotPoint != null)
            {
                float step = rotationSpeed * Time.deltaTime;
                step *= isRotatingClockwise ? -1 : 1;
                transform.RotateAround(pivotPoint.position, Vector3.forward, step);
            }


            if (Input.GetKeyDown(KeyCode.Space) && correctTiming)
            {   
                rotationSpeed += 10f;
                UpdateScoreAndCombo();
                ChangeDirection();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !correctTiming)
            {
                Reset();
                ChangeDirection();
            }
        }
    }

    public void InitializeValues()
    {
        //scoreCalculatorScript = GameObject.Find("Score").GetComponent<ScoreCalculator>();
        score = 0;
        combo = 0;
        randomIndex = Random.Range(2, 8);
        numbers[randomIndex].GetComponent<SpriteRenderer>().color = Color.yellow;
        StartCoroutine(CountdownToStart());
    }

    private void ChangeDirection()
    {
        isRotatingClockwise = !isRotatingClockwise;
        GetNewNumber();
        correctTiming = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == numbers[randomIndex])
            correctTiming = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        correctTiming = false;
    }

    private void GetNewNumber()
    {
        int newNum;

        numbers[randomIndex].GetComponent<SpriteRenderer>().color = Color.grey;

        do
        {
            newNum = Random.Range(0, 11);
        }
        while (randomIndex == newNum);
        
        randomIndex = newNum;
        numbers[randomIndex].GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public void UpdateScoreAndCombo()
    {
        combo++;
        score = scoreCalculatorScript.CalculateScore(score, combo);
        comboText.text = "Combo: " + combo;
    }

    public void Reset()
    {
        combo = 0;
        comboText.text = "Combo: " + combo;
        rotationSpeed = 75f;
    }


    IEnumerator CountdownToStart()
    {
        int countdownTime = 3;
        while(countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownText.gameObject.SetActive(false);
        gameStart = true;
    }

    public void GameOver()
    {
        //AudioManager.Instance.StopSFX("Clock");
        gameOver = true;
        gameStart = false;
        gameObject.SetActive(false);
    }
}

