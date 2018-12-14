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

    public int maxPlayers = 2;


    public int PlayersCount {
        get { return PlayerConnections.Count; }
    }

    public bool IsFull {
        get { return PlayerConnections.Count >= maxPlayers; }
    }


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

    public void UpdateMaxPlayerCount()
    {
        maxPlayers = PlayersCount;
    }
}


