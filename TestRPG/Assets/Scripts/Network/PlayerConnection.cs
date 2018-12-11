using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnection : NetworkBehaviour {

    public GameObject characterPrefab;
    public PlayerCharacter character;

    [SyncVar]
    public Transform spawnpoint;

    [SyncVar]
    public int Score = 0;

    private void Start()
    {
        if (isLocalPlayer)
        {
            CmdSpawnCharacter();
        }
        else
        {

        }
    }


    [Command]
    private void CmdSpawnCharacter()
    {
        spawnpoint = ServerManager.Instance.AddPlayer(this);
        
        GameObject newCharacter = Instantiate(characterPrefab, spawnpoint.position, spawnpoint.rotation);

        NetworkServer.SpawnWithClientAuthority(newCharacter, connectionToClient);

        character = newCharacter.GetComponent<PlayerCharacter>();
        character.connection = this;

        ServerManager.Instance.UpdateCharactersList();
    }

    private void OnEnable()
    {
        Debug.Log(gameObject.name + ": OnEnable");
    }

    private void OnDisable()
    {
        Debug.Log(gameObject.name + ": OnDisable");
        ServerManager.Instance.RemovePlayer(this);
    }

    private void OnDestroy()
    {
        Debug.Log(gameObject.name + ": OnDestroy");
    }
}
