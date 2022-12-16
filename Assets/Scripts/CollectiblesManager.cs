using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    [SerializeField] private int collectiblesCount;

    private void Awake()
    {
        collectiblesCount = FindObjectsOfType<Collectible>().Length;
        Debug.Log($"Collectible count is {collectiblesCount}");
    }

    public void collectibleCollected()
    {
        collectiblesCount--;
        Debug.Log($"{collectiblesCount} collectibles remaining");
    }
}