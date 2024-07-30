using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI priceText;


    public void Configure(int level, int price)
    {
        levelText.text = "Level " + (level + 1).ToString();
        priceText.text = price.ToString();
    }
}
