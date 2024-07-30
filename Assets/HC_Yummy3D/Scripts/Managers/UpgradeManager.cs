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


    [Header(" Pricing ")]
    [SerializeField] private int basePrice;
    [SerializeField] private int priceStep;


    [Header(" Events ")]
    public static Action onTimerPurchased;
    public static Action onSizePurchased;
    public static Action onPowerPurchased;
    public static Action<int, int, int> onDataLoaded; 



    private void Start()
    {
        LoadData();

        InitializeButtons();
    }


    private void InitializeButtons()
    {
        UpdateButtonsVisuals();
    }


    private void UpdateButtonsVisuals()
    {
        timerButton.GetComponent<UpgradeButton>().Configure(timerLevel, GetUpgradePrice(timerLevel));
        sizeButton.GetComponent<UpgradeButton>().Configure(sizeLevel, GetUpgradePrice(sizeLevel));
        powerButton.GetComponent<UpgradeButton>().Configure(powerLevel, GetUpgradePrice(powerLevel));
    }


    public void TimerButtonCallback()
    {
        onTimerPurchased?.Invoke();

        timerLevel++;
        SaveData();

        UpdateButtonsVisuals();
    }


    public void SizeButtonCallback()
    {
        onSizePurchased?.Invoke();

        sizeLevel++;
        SaveData();

        UpdateButtonsVisuals();
    }


    public void PowerButtonCallback()
    {
        onPowerPurchased?.Invoke();

        powerLevel++;
        SaveData();

        UpdateButtonsVisuals();
    }


    private int GetUpgradePrice(int upgradeLevel)
    {
        return basePrice + upgradeLevel * priceStep;
    }


    private void LoadData()
    {
        timerLevel = PlayerPrefs.GetInt(TIMER_KEY);
        sizeLevel = PlayerPrefs.GetInt(SIZE_KEY);
        powerLevel = PlayerPrefs.GetInt(POWER_KEY);

        onDataLoaded?.Invoke(timerLevel, sizeLevel, powerLevel);
    }


    private void SaveData()
    {
        PlayerPrefs.SetInt(TIMER_KEY, timerLevel);
        PlayerPrefs.SetInt(SIZE_KEY, sizeLevel);
        PlayerPrefs.SetInt(POWER_KEY, powerLevel);
    }
}
