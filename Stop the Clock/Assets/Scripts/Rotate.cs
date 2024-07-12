using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{   
    public Transform pivotPoint;
    public float rotationSpeed = 50f;
    public GameObject[] numbers;

    private int randomIndex;
    private bool isRotatingClockwise = true;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        randomIndex = Random.Range(1, 13);
        if (pivotPoint != null && gameOver == false)
        {
            float step = rotationSpeed * Time.deltaTime;
            step *= isRotatingClockwise ? 1 : -1;
            transform.RotateAround(pivotPoint.position, Vector3.forward, step);
        }
    }

    private void ChangeDirection()
    {
        isRotatingClockwise = !isRotatingClockwise;
        rotationSpeed += 10f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == numbers[randomIndex])
        {
            ChangeDirection();
        }
        else
            gameOver = true;
    }
}
