using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerManager : NetworkBehaviour
{
    
    public static ServerManager Instance { get; private set; }


    public List<GameObject> PlayerCharacters = new List<GameObject>();



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

}
