using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    private void Start()
    {
        OnLevelWasLoaded(0);
    }

    public void StartupHost()
    {
        //Debug.Log("@ " + this.name + " - StartupHost");

        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoingGame()
    {
        //Debug.Log("@ " + this.name + " - JoingGame");

        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }


    public void SetIPAddress()
    {
        //Debug.Log("@ " + this.name + " - SetIPAddress");

        NetworkManager.singleton.networkAddress = MainMenuManager.Instance.inputAddress.text;
    }

    public void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    private void OnLevelWasLoaded(int level)
    {
        //Debug.Log("@ " + this.name + " - OnLevelWasLoaded - " + level);

        if (level == 0)
        {
            StartCoroutine(SetupMenuButtons());
        }
        else
        {
            StartCoroutine(SetupGameButtons());
        }
    }

    private IEnumerator SetupMenuButtons()
    {
        //Debug.Log("@ " + this.name  + " - SetupMenuButtons");
        yield return new WaitForSeconds(0.1f);

        MainMenuManager.Instance.btnStartHost.onClick.RemoveAllListeners();
        MainMenuManager.Instance.btnStartHost.onClick.AddListener(StartupHost);

        MainMenuManager.Instance.btnJoinGame.onClick.RemoveAllListeners();
        MainMenuManager.Instance.btnJoinGame.onClick.AddListener(JoingGame);
    }

    private IEnumerator SetupGameButtons()
    {
        //Debug.Log("@ " + this.name + " - SetupGameButtons");
        yield return new WaitForSeconds(0.1f);

        GameMenuManager.Instance.btnDisconnect.onClick.RemoveAllListeners();
        GameMenuManager.Instance.btnDisconnect.onClick.AddListener(NetworkManager.singleton.StopHost);

        GameMenuManager.Instance.btnWaitingDisconnect.onClick.RemoveAllListeners();
        GameMenuManager.Instance.btnWaitingDisconnect.onClick.AddListener(NetworkManager.singleton.StopHost);

        GameMenuManager.Instance.btnStartMatch.onClick.RemoveAllListeners();
        GameMenuManager.Instance.btnStartMatch.onClick.AddListener(ServerManager.Instance.UpdateMaxPlayerCount);
    }


}
