using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CountdownController : MonoBehaviour
{
    public int countdownTime = 3;
    public TextMeshProUGUI countdownText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
            Debug.Log(countdownTime);
        }

        countdownText.gameObject.SetActive(false);
    }
}
