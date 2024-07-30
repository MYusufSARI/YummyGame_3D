using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button timerButton;
    [SerializeField] private Button sizeButton;
    [SerializeField] private Button powerButton;


    [Header(" Data ")]
    private int timerLevel;
    private int sizeLevel;
    private int powerLevel;


    private const string TIMER_KEY = "TimerLevel";
    private const string SIZE_KEY = "SizeLevel";
    private const string POWER_KEY = "PowerLevel";


    [Header(" Events ")]
    public static Action onTimerPurchased;
    public static Action onSizePurchased;
    public static Action onPowerPurchased;



    private void Awake()
    {
        LoadData();

        InitializeButtons();
    }


    private void InitializeButtons()
    {
        int timerPrice = 100;
        int sizePrice = 150;
        int powerPrice = 200;

        timerButton.GetComponent<UpgradeButton>().Configure(timerLevel, timerPrice);
        sizeButton.GetComponent<UpgradeButton>().Configure(sizeLevel, sizePrice);
        powerButton.GetComponent<UpgradeButton>().Configure(powerLevel, powerPrice);
    }


    public void TimerButtonCallback()
    {
        onTimerPurchased?.Invoke();
    }


    public void SizeButtonCallback()
    {
        onSizePurchased?.Invoke();
    }


    public void PowerButtonCallback()
    {
        onPowerPurchased?.Invoke();
    }


    private void LoadData()
    {
        timerLevel = PlayerPrefs.GetInt(TIMER_KEY);
        sizeLevel = PlayerPrefs.GetInt(SIZE_KEY);
        powerLevel = PlayerPrefs.GetInt(POWER_KEY);
    }


    private void SaveData()
    {
        PlayerPrefs.SetInt(TIMER_KEY, timerLevel);
        PlayerPrefs.SetInt(SIZE_KEY, sizeLevel);
        PlayerPrefs.SetInt(POWER_KEY, powerLevel);
    }
}
