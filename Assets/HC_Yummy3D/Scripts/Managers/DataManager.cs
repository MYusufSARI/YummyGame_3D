using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header(" Data ")]
    private int _coins;
    private const string COINS_KEY = "Coins";

    [Header(" Events ")]
    public static Action onCoinsUpdated;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    private void Start()
    {
        LoadData();
    }


    public void AddCoins(int amount)
    {
        _coins += amount;
        SaveData();

        onCoinsUpdated?.Invoke();
    }


    public void Purchase(int price)
    {
        _coins -= price;
        SaveData();

        onCoinsUpdated?.Invoke();
    }


    public int GetCoins()
    {
        return _coins;
    }


    private void LoadData()
    {
        _coins = PlayerPrefs.GetInt(COINS_KEY, 150);

        onCoinsUpdated?.Invoke();
    }


    private void SaveData()
    {
        PlayerPrefs.SetInt(COINS_KEY, _coins);
    }
}
