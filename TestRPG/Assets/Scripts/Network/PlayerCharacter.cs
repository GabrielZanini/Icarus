using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Cameras;

public class PlayerCharacter : NetworkBehaviour {
    
    public PlayerConnection connection;

    [SyncVar]
    public int score = 0;

    public GameObject camera;

    void Start ()
    {
        StartCoroutine(StartCharacter());        
	}

    
    void OnEnable()
    {

    }

    void OnDisable()
    {
        if (camera != null)
        {
            Destroy(camera);
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
            camera.SetActive(true);
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
