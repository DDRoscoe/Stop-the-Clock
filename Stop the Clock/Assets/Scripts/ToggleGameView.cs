using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameView : MonoBehaviour
{
    public GameObject gameView;
    public MainHand mainHandScript;

    void Start()
    {
        mainHandScript = GameObject.Find("Main Hand Body").GetComponent<MainHand>();
    }

    public void EnableView()
    {
        if (gameView != null)
            gameView.SetActive(true);
        else
            Debug.Log("Object not assigned");
    }

    public void DisableView()
    {
        if (gameView != null)
        {
            gameView.SetActive(false);
        }
    }

    public void ResetView()
    {
        mainHandScript.gameOver = false;
        EnableView();
        mainHandScript.InitializeValues();
    }
}
