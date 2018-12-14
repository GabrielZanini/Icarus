using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour {

    public static GameMenuManager Instance { get; private set; }

    [Header("Buttons")]
    public Button btnContinue;
    public Button btnDisconnect;
    public Button btnWaitingDisconnect;
    public Button btnStartMatch;


    [Header("Texts")]
    public Text numberPlayers;
    public Text seconds;
    public Text timer;

    [Header("Panels")]
    public GameObject waitingPanel;
    public GameObject startPanel;
    public GameObject startAnywayPanel;
    public GameObject exitPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Exit() 
    {
        exitPanel.SetActive(true);
    }

    public void Continue()
    {
        exitPanel.SetActive(false);
    }
}
