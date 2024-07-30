using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTimer : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private int timerDuration;
    private int _timer;
    private bool timerIsOn;


    [Header(" Settings ")]
    [SerializeField] private int additionTimePerLevel;


    [Header(" Actions ")]
    public static Action onTimerOver;



    private void Awake()
    {
        UpgradeManager.onDataLoaded += UpgradeDataLoadedCallback;
    }



    private void Start()
    {
        Initialize();

        GameManager.onGameStateChanged += GameStateChangedCallback;

        UpgradeManager.onTimerPurchased += TimerPurchasedCallback;
    }


    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;

        UpgradeManager.onTimerPurchased -= TimerPurchasedCallback;

        UpgradeManager.onDataLoaded -= UpgradeDataLoadedCallback;
    }


    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAME:
                StartTimer();
                break;
        }
    }


    private void Update()
    {
        
    }


    public void Initialize()
    {
        _timer = timerDuration;
        timerText.text = FormatSeconds(_timer);
    }


    public void StartTimer()
    {
        if (timerIsOn)
            return;

        Initialize();

        timerIsOn = true;

        StartCoroutine(TimerCoroutine());
    }


    IEnumerator TimerCoroutine()
    {
        while (timerIsOn)
        {
            yield return new WaitForSeconds(1);

            _timer--;
            timerText.text = FormatSeconds(_timer);

            if (_timer ==0)
            {
                timerIsOn = false;

                StopTimer();
            }
        }
    }


    public void StopTimer()
    {
        Debug.Log("Timer is over ! ");

        onTimerOver?.Invoke();
    }


    private void TimerPurchasedCallback()
    {
        timerDuration += additionTimePerLevel;
        Initialize();
    }


    private void UpgradeDataLoadedCallback(int timerLevel, int sizeLevel, int powerLevel)
    {
        timerDuration += additionTimePerLevel * timerLevel;

        Initialize();
    }


    private string FormatSeconds(int totalseconds)
    {
        int minutes = totalseconds / 60;

        int seconds = totalseconds % 60;

        return minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }
}
