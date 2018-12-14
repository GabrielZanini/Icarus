using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MatchManager : NetworkBehaviour
{
    public static MatchManager Instance { get; private set; }

    [SyncVar]
    public int playerCount = 0;


    [SyncVar]
    public int duration = 120;
    [SyncVar]
    public int startCountdown = 5;

    [SyncVar]
    public bool isWaiting = false;
    [SyncVar]
    public bool isStarting = false;
    [SyncVar]
    public bool isPlaying = false;
    [SyncVar]
    public bool isExiting = false;

    bool _inputExitDown = false;



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

    void Start ()
    {
        StartCoroutine(WaitForPlayers());
    }
	
	void Update ()
    {
        if (isServer)
        {
            playerCount = ServerManager.Instance.PlayersCount;
        }

        if (isWaiting)
        {
            GameMenuManager.Instance.numberPlayers.text = playerCount.ToString();
        }
        else if (isStarting)
        {
            GameMenuManager.Instance.seconds.text = startCountdown.ToString();
        }
        else if (isPlaying)
        {
            _inputExitDown = Input.GetButton("Exit");

            if (_inputExitDown && isExiting)
            {
                GameMenuManager.Instance.exitPanel.SetActive(false);
            }
            else if (_inputExitDown && !isExiting)
            {
                GameMenuManager.Instance.exitPanel.SetActive(true);
            }

            GameMenuManager.Instance.timer.text = duration.ToString();
        }
    }



    IEnumerator WaitForPlayers()
    {
        yield return null;

        isWaiting = true;

        GameMenuManager.Instance.waitingPanel.SetActive(true);
        if (isServer)
        {
            GameMenuManager.Instance.startAnywayPanel.SetActive(true);
        }

        if (isServer)
        {
            while (!ServerManager.Instance.IsFull)
            {
                yield return null;
            }
        }
        else
        {
            while (isWaiting)
            {
                yield return null;
            }
        }
                
        GameMenuManager.Instance.waitingPanel.SetActive(false);
        GameMenuManager.Instance.startAnywayPanel.SetActive(false);        

        isWaiting = false;

        yield return StartMatch();
    }

    IEnumerator StartMatch()
    {
        yield return null;

        isStarting = true;

        GameMenuManager.Instance.startPanel.SetActive(true);

        if (isServer)
        {
            while (startCountdown >= 0)
            {
                yield return new WaitForSeconds(1);

                startCountdown--;
            }
        }
        else
        {
            while (isStarting || startCountdown >= 0)
            {
                yield return null;
            }
        }

        GameMenuManager.Instance.startPanel.SetActive(false);

        isStarting = false;

        yield return PlayMatch();
    }

    IEnumerator PlayMatch()
    {
        yield return null;

        isPlaying = true;

        if (isServer)
        {
            while (duration >= 0)
            {
                yield return new WaitForSeconds(1);

                duration--;
            }
        }
        else
        {
            while (isPlaying)
            {
                yield return null;
            }
        }

        NetworkManager.singleton.StopHost();
        isPlaying = false;

        yield return PlayMatch();
    }
}
