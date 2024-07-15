using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainHand : MonoBehaviour
{   
    public Transform pivotPoint;
    public TextMeshProUGUI scoreText;
    public float rotationSpeed = 50f;
    public GameObject[] numbers;
    public bool correctTiming;
    public bool isRotatingClockwise = true;
    public bool gameOver;
    private int score;
    private int randomIndex;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        randomIndex = Random.Range(2, 8);
        numbers[randomIndex].GetComponent<SpriteRenderer>().color = Color.yellow;
        AudioManager.Instance.LoopSFX("Clock");
    }

    // Update is called once per frame
    void Update()
    {

        if (pivotPoint != null && gameOver == false)
        {
            float step = rotationSpeed * Time.deltaTime;
            step *= isRotatingClockwise ? -1 : 1;
            transform.RotateAround(pivotPoint.position, Vector3.forward, step);
        }


        if (Input.GetKeyDown(KeyCode.Space) && correctTiming)
        {   
            ChangeDirection();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !correctTiming)
        {
            GameOver();
        }
        

    }

    private void ChangeDirection()
    {
        UpdateScore();
        isRotatingClockwise = !isRotatingClockwise;
        rotationSpeed += 7f;
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

    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        AudioManager.Instance.StopSFX("Clock");
        gameOver = true;
        //Debug.Log("Game Over!");
    }
}

