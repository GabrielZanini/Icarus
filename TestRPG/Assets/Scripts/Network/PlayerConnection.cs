using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnection : NetworkBehaviour {

    public GameObject characterPrefab;

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
        GameObject character = Instantiate(characterPrefab);

        NetworkServer.SpawnWithClientAuthority(character, connectionToClient);

        //ServerManager.Instance.PlayerCharacters.Add(character);
    }
}
