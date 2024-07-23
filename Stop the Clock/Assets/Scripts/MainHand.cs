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
    public TextMeshProUGUI highestComboText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highestComboValueText;
    public TextMeshProUGUI finalScoreValueText;
    public int combo;
    public int highestCombo;
    public float rotationSpeed = 75f;
    public float score;
    public float emissionRate = 2f;
    public bool correctTiming;
    public bool isRotatingClockwise = true;
    public bool gameStart = false;
    public bool gameOver;
    public GameObject[] numbers;
    public GameObject clock;
    public GameObject cover;
    public Animator clockSpawnAnim;
    public Animator comboAnim;
    public ParticleSystem particleSystem;
    private ParticleSystem.EmissionModule emissionModule;

    private int multiplier;
    private int randomIndex;

    public Timer timerScript;
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
        scoreCalculatorScript = GameObject.Find("Score").GetComponent<ScoreCalculator>();
        emissionModule = particleSystem.emission;
        clockSpawnAnim = GameObject.Find("Clock").GetComponent<Animator>();
        comboAnim = comboText.GetComponent<Animator>();
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
                audioManagerScript.tickingSource.pitch += 0.03f;
                emissionRate += 1f;
                emissionModule.rateOverTime = emissionRate;
                comboAnim.SetTrigger("ComboHit");
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !correctTiming)
            {
                ComboBreak();
                ChangeDirection();
                audioManagerScript.PlaySFX(audioManagerScript.comboBreak);
                audioManagerScript.tickingSource.pitch = 1.0f;
                emissionRate = 2f;
                particleSystem.Stop();
                comboAnim.SetTrigger("ComboMiss");
            }
        }
    }

    public void PressedBegin()
    {
        clockSpawnAnim.SetTrigger("ClockSpawn");
        InitializeValues();
        StartCoroutine(CountdownToStart());
    }

    public void InitializeValues()
    {
        emissionRate = 2f;
        highestComboValueText.gameObject.SetActive(false);
        highestComboText.gameObject.SetActive(false);
        finalScoreValueText.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false);
        audioManagerScript.tickingSource.pitch = 1.0f;
        cover.SetActive(false);
        countdownText.gameObject.SetActive(true);
        rotationSpeed = 75f;
        score = 0;
        combo = 0;
        highestCombo = 0;
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
        timerScript.timerText.text = timerScript.timer.ToString();
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
        randomIndex = Random.Range(0, 11);
        numbers[randomIndex].GetComponent<SpriteRenderer>().color = Color.yellow;
        gameStart = true;
        audioManagerScript.PlayTicking();
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
        if (highestCombo < combo)
            highestCombo = combo;
        if (combo == 4)
            particleSystem.Play();
    }

    public void ComboBreak()
    {
        combo = 0;
        scoreCalculatorScript.UpdateScoreAndComboText(score, combo);
        rotationSpeed = 75f;
    }

    public void GameOver()
    {
        audioManagerScript.StopTicking();
        timerScript.timer = 60f;
        //timerScript.timerText.gameObject.SetActive(false);
        particleSystem.Stop();
        DisplayComboAndScores();
        ResetCircles();

        gameOver = true;
        gameStart = false;
    }

    public void DisplayComboAndScores()
    {
        int intScore = (int)score;
        highestComboValueText.text = highestCombo.ToString();
        highestComboValueText.gameObject.SetActive(true);
        highestComboText.gameObject.SetActive(true);
        finalScoreValueText.text = intScore.ToString();
        finalScoreValueText.gameObject.SetActive(true);
        finalScoreText.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

