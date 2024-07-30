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

        DataManager.onCoinsUpdated += CoinsUpdatedCallback;
    }


    private void OnDestroy()
    {
        DataManager.onCoinsUpdated -= CoinsUpdatedCallback;
    }


    private void CoinsUpdatedCallback()
    {
        UpdateButtonsInteractability();
    }


    private void InitializeButtons()
    {
        UpdateButtonsVisuals();
    }


    private void UpdateButtonsInteractability()
    {
        timerButton.interactable = GetUpgradePrice(timerLevel) <= DataManager.instance.GetCoins();
        sizeButton.interactable = GetUpgradePrice(sizeLevel) <= DataManager.instance.GetCoins();
        powerButton.interactable = GetUpgradePrice(powerLevel) <= DataManager.instance.GetCoins();
    }


    private void UpdateButtonsVisuals()
    {
        timerButton.GetComponent<UpgradeButton>().Configure(timerLevel, GetUpgradePrice(timerLevel));
        sizeButton.GetComponent<UpgradeButton>().Configure(sizeLevel, GetUpgradePrice(sizeLevel));
        powerButton.GetComponent<UpgradeButton>().Configure(powerLevel, GetUpgradePrice(powerLevel));

        UpdateButtonsInteractability();
    }


    public void TimerButtonCallback()
    {
        onTimerPurchased?.Invoke();

        DataManager.instance.Purchase(GetUpgradePrice(timerLevel));

        timerLevel++;
        SaveAndUpdateVisuals();
    }


    public void SizeButtonCallback()
    {
        onSizePurchased?.Invoke();

        DataManager.instance.Purchase(GetUpgradePrice(sizeLevel));

        sizeLevel++;
        SaveAndUpdateVisuals();
    }


    public void PowerButtonCallback()
    {
        onPowerPurchased?.Invoke();

        DataManager.instance.Purchase(GetUpgradePrice(powerLevel));

        powerLevel++;
        SaveAndUpdateVisuals();
    }


    private void SaveAndUpdateVisuals()
    {
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
