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
    private float scaleValue;



    private void Start()
    {
        fillImage.fillAmount = 0;
    }


    private void IncreaseScale()
    {
        transform.localScale += scaleStep * Vector3.one;
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
        fillImage.fillAmount = scaleValue / scaleIncreaseThreshold;
    }
}
