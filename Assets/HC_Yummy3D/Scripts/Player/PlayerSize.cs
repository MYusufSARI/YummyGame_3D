using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSize : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Image fillImage;


    [Header(" Settings ")]
    [SerializeField] private float scaleIncreaseThreshold;
    [SerializeField] private float scaleStep;
    [SerializeField] private AnimationCurve sizeCurve;
    private float scaleValue;


    [Header(" Power ")]
    private float powerMultiplier;


    [Header(" Events ")]
    public static Action<float> onIncreased;



    private void Awake()
    {
        UpgradeManager.onDataLoaded += UpgradeDataLoadedCallback;
    }


    private void Start()
    {
        fillImage.fillAmount = 0;

        UpgradeManager.onSizePurchased += SizePurchasedCallback;
        UpgradeManager.onPowerPurchased += PowerPurchasedCallback;
    }


    private void OnDestroy()
    {
        UpgradeManager.onSizePurchased -= SizePurchasedCallback;
        UpgradeManager.onPowerPurchased -= PowerPurchasedCallback;
        UpgradeManager.onDataLoaded -= UpgradeDataLoadedCallback;
    }


    private void IncreaseScale()
    {
        float targetScale = transform.localScale.x + scaleStep;

        UpdateScale(targetScale);
    }


    private void UpdateScale(float targetScale)
    {
        LeanTween.scale(transform.gameObject, targetScale * Vector3.one, 0.5f * Time.deltaTime * 60)
            .setEase(LeanTweenType.easeInOutBack);

        onIncreased?.Invoke(targetScale);
    }


    public void CollectibleCollected(float objectSize)
    {
        scaleValue += objectSize * (1 + powerMultiplier);

        if (scaleValue >= scaleIncreaseThreshold)
        {
            IncreaseScale();

            scaleValue = scaleValue % scaleIncreaseThreshold;
        }

        UpdateFillDisplay();
    }


    private void UpdateFillDisplay()
    {
        float targetFillAmount = scaleValue / scaleIncreaseThreshold;

        LeanTween.value(fillImage.fillAmount, targetFillAmount, 0.2f * Time.deltaTime * 60)
            .setOnUpdate((value) => fillImage.fillAmount = value);

    }


    private void SizePurchasedCallback()
    {
        IncreaseScale();
    }


    private void PowerPurchasedCallback()
    {
        powerMultiplier++;
    }


    private void UpgradeDataLoadedCallback(int timerLevel, int sizeLevel, int powerLevel)
    {
        float targetScale = transform.localScale.x + scaleStep * sizeLevel;

        UpdateScale(targetScale);

        powerMultiplier = powerLevel;
    }
}
