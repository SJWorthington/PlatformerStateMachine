using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private CollectiblesManager _collectiblesManager;
    private void Awake()
    {
        _collectiblesManager = FindObjectOfType<CollectiblesManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _collectiblesManager.collectibleCollected();
        Destroy(gameObject);
    }
}
