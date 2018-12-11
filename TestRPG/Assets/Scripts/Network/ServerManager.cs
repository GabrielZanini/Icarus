using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerManager : NetworkBehaviour
{    
    public static ServerManager Instance { get; private set; }

    public List<Transform> PlayerSpawnpoints = new List<Transform>();

    public List<PlayerConnection> PlayerConnections = new List<PlayerConnection>();
    public List<PlayerCharacter> PlayerCharacters = new List<PlayerCharacter>();




    void Awake()
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

    public Transform GetNextSpawnpoint(PlayerConnection connection)
    {
        return PlayerSpawnpoints[PlayerConnections.IndexOf(connection)];
    }

    public Transform AddPlayer(PlayerConnection connection)
    {
        PlayerConnections.Add(connection);
        return GetNextSpawnpoint(connection);
    }

    public void RemovePlayer(PlayerConnection connection)
    {
        PlayerConnections.Remove(connection);
        UpdateCharactersList();
    }

    public void UpdateCharactersList()
    {
        PlayerCharacters.Clear();

        for (int i=0; i< PlayerConnections.Count; i++)
        {
            PlayerCharacters.Add(PlayerConnections[i].character);
        }
    }
}


