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
    public float rotationSpeed = 75f;
    public GameObject[] numbers;
    public bool correctTiming;
    public bool isRotatingClockwise = true;
    public bool gameStart = false;
    public bool gameOver;
    public float score;
    public int combo;
    public GameObject clock;
    public GameObject cover;
    public Animator clockSpawnAnim;
    public Timer timerScript;

    private int multiplier;
    private int randomIndex;

    private AudioManager audioManagerScript;
    private ScoreCalculator scoreCalculatorScript;

    private void Awake()
    {
        audioManagerScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timerScript = GameObject.Find("Square").GetComponent<Timer>();
        clockSpawnAnim = clock.GetComponent<Animator>();
        scoreCalculatorScript = GameObject.Find("Score").GetComponent<ScoreCalculator>();
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
                audioManagerScript.PlaySFX(audioManagerScript.coin);

            }
            else if (Input.GetKeyDown(KeyCode.Space) && !correctTiming)
            {
                ComboBreak();
                ChangeDirection();
                audioManagerScript.PlaySFX(audioManagerScript.comboBreak);
            }
        }
    }

    public void PressedBegin()
    {
        clockSpawnAnim.SetTrigger("Play");
        InitializeValues();
        StartCoroutine(CountdownToStart());
    }

    public void InitializeValues()
    {
        cover.SetActive(false);
        countdownText.gameObject.SetActive(true);
        timerScript.timer = 60f;
        rotationSpeed = 75f;
        score = 0;
        combo = 0;
        scoreCalculatorScript.UpdateScoreAndComboText(score, combo);
        gameStart = false;
        isRotatingClockwise = true;
        gameOver = false;
    }

    public void ResetCircles()
    {
        for (int i = 0; i < 12; i++)
        {
            numbers[i].GetComponent<SpriteRenderer>().color = Color.grey;
        }
    }


    IEnumerator CountdownToStart()
    {
        audioManagerScript.PlaySFX(audioManagerScript.countdown);
        int countdownTime = 3;
        while(countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            if (countdownTime > 1)
                audioManagerScript.PlaySFX(audioManagerScript.countdown);
            countdownTime--;
            if (countdownTime == 0)
                audioManagerScript.PlaySFX(audioManagerScript.begin);
        }

        countdownText.gameObject.SetActive(false);
        timerScript.timerText.gameObject.SetActive(true);
        randomIndex = Random.Range(2, 8);
        numbers[randomIndex].GetComponent<SpriteRenderer>().color = Color.yellow;
        gameStart = true;
        audioManagerScript.PlaySFX(audioManagerScript.ticking);
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
    }

    public void ComboBreak()
    {
        combo = 0;
        scoreCalculatorScript.UpdateScoreAndComboText(score, combo);
        rotationSpeed = 75f;
    }

    public void GameOver()
    {
        cover.SetActive(true);
        timerScript.timerText.gameObject.SetActive(false);
        ResetCircles();
        gameOver = true;
        gameStart = false;
    }
}

