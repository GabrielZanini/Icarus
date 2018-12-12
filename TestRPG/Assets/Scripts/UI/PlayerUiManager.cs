using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUiManager : MonoBehaviour {

    [Header("Player")]
    public PlayerCharacter character;

    [Header("UI")]
    public Text score;

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        score.text = character.score.ToString();
    }
}
