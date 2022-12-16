using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayerController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private int blueLayer = 9;
    private int yellowLayer = 10;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void swapLayer()
    {
        if (gameObject.layer == blueLayer)
        {
            _spriteRenderer.color = Color.yellow;
            gameObject.layer = yellowLayer;
        }
        else
        {
            _spriteRenderer.color = Color.blue;
            gameObject.layer = blueLayer;
        }
    }
}
