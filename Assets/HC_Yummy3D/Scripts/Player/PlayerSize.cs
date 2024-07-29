using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private float scaleIncreaseThreshold;
    [SerializeField] private float scaleStep;
    private float scaleValue;



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
    }
}
