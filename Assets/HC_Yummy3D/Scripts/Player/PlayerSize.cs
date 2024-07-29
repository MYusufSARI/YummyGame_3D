using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    public void CollectibleCollected(float objectSize)
    {
        Debug.Log("Increase the size by : " + objectSize);
    }
}
