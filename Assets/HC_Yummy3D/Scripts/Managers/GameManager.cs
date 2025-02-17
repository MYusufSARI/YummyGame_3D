using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MENU, GAME, LEVEL_COMPLETE, GAMEOVER}

public class GameManager : MonoBehaviour
{
    [Header(" Settings ")]
    private GameState gameState;


    [Header(" Events ")]
    public static Action<GameState> onGameStateChanged;



    IEnumerator Start()
    {
        yield return null;

        gameState = GameState.MENU;

        onGameStateChanged?.Invoke(gameState);
    }


    public void SetGameState()
    {
        gameState = GameState.GAME;

        onGameStateChanged?.Invoke(gameState);
    }
}
