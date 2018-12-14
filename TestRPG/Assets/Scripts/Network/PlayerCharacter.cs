using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Cameras;

public class PlayerCharacter : NetworkBehaviour {
    
    public PlayerConnection connection;

    [SyncVar]
    public int score = 0;
    [SyncVar]
    public bool win = false;
    [SyncVar]
    public bool lose = false;
    [SyncVar]
    public bool draw = false;



    public PlayerInput input;

    public FreeLookCam gameCamera;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    void Start ()
    {
        StartCoroutine(StartCharacter());        
	}

    
    void OnEnable()
    {

    }

    void OnDisable()
    {
        if (gameCamera != null)
        {
            Destroy(gameCamera.gameObject);
        }
    }

    void EnableLocalScripts()
    {
        GetComponent<CopyRotation>().enabled = true;
        GetComponent<PlayerInput>().enabled = true;
        GetComponent<MovePlayer>().enabled = true;
        GetComponent<ShootArrow>().enabled = true;
    }

    void EnableScripts()
    {
        GetComponent<ShootArrow>().enabled = true;
    }
    
    IEnumerator StartCharacter()
    {
        yield return null;

        if (hasAuthority)
        {
            EnableLocalScripts();

            gameCamera.gameObject.SetActive(true);

            MatchManager.Instance.localCharacter = this;
        }


        //EnableScripts();

        gameObject.name = "PlayerCharacter";
        gameObject.name += hasAuthority ? "_LocalAuthority" : "_Other";
        gameObject.name += isServer ? "_Server" : "_Client";
    }

    public void AddScore(int points)
    {
        score += points;
    }
}
