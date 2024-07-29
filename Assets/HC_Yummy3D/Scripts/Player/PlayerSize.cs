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



    private void Start()
    {
        fillImage.fillAmount = 0;
    }


    private void IncreaseScale()
    {
        float targetScale = transform.localScale.x + scaleStep;

        LeanTween.scale(transform.gameObject, targetScale * Vector3.one, 0.5f * Time.deltaTime * 60)
            .setEase(LeanTweenType.easeInOutBack);
    }


    public void CollectibleCollected(float objectSize)
    {
        scaleValue += objectSize;

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
}
