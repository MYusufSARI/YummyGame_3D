using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private float _size;



    public float GetSize()
    {
        return _size;
    }
}
