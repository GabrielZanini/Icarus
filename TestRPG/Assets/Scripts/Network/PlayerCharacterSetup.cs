using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Cameras;

public class PlayerCharacterSetup : NetworkBehaviour {
    
    public GameObject cameraPrefab;

    void Start ()
    {
        StartCoroutine(StartCharacter());        
	}

    
    void OnEnable()
    {
        if (isServer)
        {
            ServerManager.Instance.PlayerCharacters.Add(gameObject);
        }
    }

    void OnDisable()
    {
        if (isServer)
        {
            ServerManager.Instance.PlayerCharacters.Remove(gameObject);
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
        var camera = Instantiate(cameraPrefab, transform.position, transform.rotation);

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
}
