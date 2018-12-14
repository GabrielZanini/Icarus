using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    public static MainMenuManager Instance { get; private set; }

    public Button btnStartHost;
    public Button btnJoinGame;
    public InputField inputAddress;


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
}
