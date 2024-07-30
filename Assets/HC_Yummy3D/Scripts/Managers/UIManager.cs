using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;


    [Header(" Coins ")]
    [SerializeField] private TextMeshProUGUI menuCoinsText;



    private void Awake()
    {
        DataManager.onCoinsUpdated += UpdateCoins;
    }


    private void Start()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }


    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;

        DataManager.onCoinsUpdated -= UpdateCoins;
    }


    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                SetMenu();
                break;

            case GameState.GAME:
                SetGame();
                break;
        }
    }


    private void SetMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
    }


    private void SetGame()
    {
        gamePanel.SetActive(true);
        menuPanel.SetActive(false);
    }


    private void UpdateCoins()
    {
        menuCoinsText.text = DataManager.instance.GetCoins().ToString();
    }
}
