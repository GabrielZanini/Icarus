﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnection : NetworkBehaviour {

    public GameObject characterPrefab;
    public GameObject cameraPrefab;

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
    }
}