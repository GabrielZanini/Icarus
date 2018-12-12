using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MatchManager : NetworkBehaviour
{
    public static MatchManager Instance { get; private set; }

    [SyncVar]
    public float duration = 120f;

    private void Awake()
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

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    IEnumerator StartMatch()
    {
        yield return null;
    }
}
