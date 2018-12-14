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

    
    public void GetResults()
    {
        int biggerScore = 0;
        bool draw = false;
        
        for (int i = 0; i < PlayerCharacters.Count; i++)
        {
            if (i == 0)
            {
                biggerScore = PlayerCharacters[i].score;
            }
            else
            {
                if (PlayerCharacters[i].score > biggerScore)
                {
                    biggerScore = PlayerCharacters[i].score;
                    draw = false;
                }
                else if (PlayerCharacters[i].score == biggerScore)
                {
                    draw = true;
                }
            }

        }

        for (int i = 0; i < PlayerCharacters.Count; i++)
        {
            if (PlayerCharacters[i].score >= biggerScore)
            {
                if (draw)
                {
                    PlayerCharacters[i].draw = true;
                }
                else
                {
                    PlayerCharacters[i].win = true;
                }
            }
            else
            {
                PlayerCharacters[i].lose = true;
            }
        }
    }
}


