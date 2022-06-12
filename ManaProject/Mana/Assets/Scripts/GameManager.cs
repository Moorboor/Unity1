using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state;

    void Awake()
    {
        Instance = this;
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.Menu:
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.HostileTurn:
                break;
            case GameState.EndScreen:
                break;
            default:
                break;
                //throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}
public enum GameState
{
    Menu,
    PlayerTurn,
    HostileTurn,
    EndScreen,
}

