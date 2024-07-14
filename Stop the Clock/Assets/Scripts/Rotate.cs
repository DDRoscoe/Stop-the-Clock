using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{   
    public Transform pivotPoint;
    public float rotationSpeed = 50f;
    public GameObject[] numbers;
    public bool correctTiming;

    private int randomIndex;
    private bool isRotatingClockwise = true;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        randomIndex = Random.Range(2, 8);
        Debug.Log(randomIndex);
        numbers[randomIndex].GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {

        if (pivotPoint != null && gameOver == false)
        {
            float step = rotationSpeed * Time.deltaTime;
            step *= isRotatingClockwise ? 1 : -1;
            transform.RotateAround(pivotPoint.position, Vector3.forward, step);
        }


        if (Input.GetKeyDown(KeyCode.Space) && correctTiming)
        {   
            ChangeDirection();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !correctTiming)
        {
            gameOver = true;
            Debug.Log("Game Over!");
        }
        

    }

    private void ChangeDirection()
    {
        isRotatingClockwise = !isRotatingClockwise;
        rotationSpeed += 5f;
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
}
