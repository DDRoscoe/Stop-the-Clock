using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Transform pivotPoint;
    public float rotationSpeed;
    public float timer = 30f;
    public float colorChangeInterval;

    private float colorTimer = 0f;
    private int intTimer;
    private bool isColor1Active;

    private MainHand mainHandScript;
    // Start is called before the first frame update
    void Start()
    {
        mainHandScript = GameObject.Find("Main Hand Body").GetComponent<MainHand>();
        rotationSpeed = 360f / timer;
        isColor1Active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainHandScript.gameOver == false)
        {
            if (pivotPoint != null)
            {
                float step = rotationSpeed * Time.deltaTime;
                transform.RotateAround(pivotPoint.position, Vector3.forward, -step);
                timer -= Time.deltaTime;
                intTimer = (int)timer;
                timerText.text = intTimer.ToString();
            }

            colorTimer += Time.deltaTime;

            if (timer > 0 && timer < 10)
            {
                colorChangeInterval = timer < 5 ? 0.25f : 0.5f;
                Debug.Log(colorTimer);
                if (colorTimer >= colorChangeInterval)
                {
                    SwitchColor();
                    colorTimer = 0f;
                }
            }
        }

        if (timer < 0)
        {
            mainHandScript.GameOver();
        }
    }

    void SwitchColor()
    {
        if (isColor1Active)
            timerText.color = Color.red;
        else
            timerText.color = Color.white;
        
        isColor1Active = !isColor1Active;
    }
}
