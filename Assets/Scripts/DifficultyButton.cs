using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public int difficulty;
    Button button;
    GameManager gameManager;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void SetDifficulty()
    {
        Debug.Log(gameObject.name + " was clicked.");
        gameManager.StartGame(difficulty);
    }
}
