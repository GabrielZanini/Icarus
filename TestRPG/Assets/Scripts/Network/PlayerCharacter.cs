using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Cameras;

public class PlayerCharacter : NetworkBehaviour {
    
    public GameObject cameraPrefab;
    public PlayerConnection connection;

    [SyncVar]
    public int score = 0;

    GameObject camera;

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

    void EnableScripts()
    {
        GetComponent<CopyRotation>().enabled = true;
        GetComponent<PlayerInput>().enabled = true;
        GetComponent<MovePlayer>().enabled = true;
        GetComponent<ShootArrow>().enabled = true;
    }

    void SpawnCamera()
    {
        camera = Instantiate(cameraPrefab, transform.position, transform.rotation);

        camera.GetComponent<FreeLookCam>().SetTarget(transform);
        GetComponent<CopyRotation>().SetTarget(camera.transform);
    }

    IEnumerator StartCharacter()
    {
        yield return null;

        if (hasAuthority)
        {
            EnableScripts();
            SpawnCamera();
        }

        gameObject.name = "PlayerCharacter";
        gameObject.name += hasAuthority ? "_LocalAuthority" : "_Other";
        gameObject.name += isServer ? "_Server" : "_Client";
    }

    public void AddScore(int points)
    {
        score += points;
    }
}
