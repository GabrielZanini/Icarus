using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MatchManager : NetworkBehaviour
{
    public static MatchManager Instance { get; private set; }

    [SyncVar]
    public int playerCount = 0;

    public PlayerConnection localPlayer;
    public PlayerCharacter localCharacter;


    [SyncVar]
    public int duration = 120;
    [SyncVar]
    public int startCountdown = 5;
    [SyncVar]
    public int endCountdown = 5;

    [SyncVar]
    public bool isWaiting = false;
    [SyncVar]
    public bool isStarting = false;
    [SyncVar]
    public bool isPlaying = false;
    [SyncVar]
    public bool isExiting = false;
    [SyncVar]
    public bool isShowingResults = false;

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
            _inputExitDown = Input.GetButtonDown("Exit");

            if (_inputExitDown && isExiting)
            {
                GameMenuManager.Instance.exitPanel.SetActive(false);
                UnLockPlayer();
                isExiting = false;
            }
            else if (_inputExitDown && !isExiting)
            {
                GameMenuManager.Instance.exitPanel.SetActive(true);
                LockPlayer();
                isExiting = true;
            }

            GameMenuManager.Instance.timer.text = duration.ToString();
        }
    }


    public void LockPlayer()
    {
        PlayerInput.canRead = false;
        LockCamera();
    }

    public void UnLockPlayer()
    {
        PlayerInput.canRead = true;
        UnLockCamera();
    }

    public void LockCamera()
    {
        localCharacter.gameCamera.enabled = false;
        Cursor.visible = true;
    }

    public void UnLockCamera()
    {
        localCharacter.gameCamera.enabled = true;
        Cursor.visible = false;
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

        while (localCharacter == null || localCharacter.gameCamera == null)
        {
            yield return null;
        }

        LockCamera();

        if (isServer)
        {
            while (!ServerManager.Instance.IsFull)
            {
                yield return null;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else
        {
            while (isWaiting)
            {
                yield return null;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
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

        UnLockCamera();
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

        UnLockPlayer();

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

        GameMenuManager.Instance.exitPanel.SetActive(false);

        LockPlayer();

        isPlaying = false;

        yield return ShowResults();
    }


    IEnumerator ShowResults()
    {
        yield return null;

        isShowingResults = true;

        if (isServer)
        {
            ServerManager.Instance.GetResults();
        }

        while (!localCharacter.win && !localCharacter.lose && !localCharacter.draw)
        {
            yield return null;
        }

        if (localCharacter.win)
        {
            GameMenuManager.Instance.winPanel.SetActive(true);
        }
        else if (localCharacter.lose)
        {
            GameMenuManager.Instance.losePanel.SetActive(true);
        }
        else if (localCharacter.draw)
        {
            GameMenuManager.Instance.drawPanel.SetActive(true);
        }

        if (isServer)
        {
            endCountdown += 2;
        }

        yield return new WaitForSeconds(endCountdown);

        NetworkManager.singleton.StopHost();
        isShowingResults = false;

        yield return PlayMatch();
    }
}
