using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timesUpText;
    public Transform pivotPoint;
    public float rotationSpeed;
    public float timer = 60f;
    public float colorChangeInterval;
    public GameObject filter;

    private float colorTimer = 0f;
    private float timesAlmostUpTimer = 0f;
    private int intTimer;
    private bool isColor1Active;

    private MainHand mainHandScript;
    private AudioManager audioManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        mainHandScript = GameObject.Find("Main Hand Body").GetComponent<MainHand>();
        audioManagerScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        rotationSpeed = 360f / timer;
        isColor1Active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainHandScript.gameStart && mainHandScript.gameOver == false)
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
            timesAlmostUpTimer += Time.deltaTime;

            if (timer < 10)
            {
                colorChangeInterval = timer < 5 ? 0.25f : 0.5f;
                if (colorTimer >= colorChangeInterval)
                {
                    SwitchColor();
                    colorTimer = 0f;
                }
            }

            if (timer < 6)
            {
                if (timesAlmostUpTimer >= 1f)
                {
                    audioManagerScript.PlaySFX(audioManagerScript.timesAlmostUp);
                    timesAlmostUpTimer = 0f;
                }
            }
        
        }

        if (timer < 0)
        {
            audioManagerScript.PlaySFX(audioManagerScript.timesUp);
            StartCoroutine(DisplayTimesUp());
            mainHandScript.GameOver();
        }
    }

    public void SwitchColor()
    {
        if (isColor1Active)
            timerText.color = Color.red;
        else
            timerText.color = Color.white;
        
        isColor1Active = !isColor1Active;
    }

    IEnumerator DisplayTimesUp()
    {
        timesUpText.gameObject.SetActive(true);
        filter.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        mainHandScript.cover.gameObject.SetActive(true);
        timesUpText.gameObject.SetActive(false);
        filter.gameObject.SetActive(false);
    }
}
