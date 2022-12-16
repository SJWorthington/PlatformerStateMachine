using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingFloor : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private Color fullColor;
    [SerializeField] private float disappearTime = 1;
    [SerializeField] private float reappearTime = 3;
    private bool isDisappearing;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        fullColor = _spriteRenderer.color;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player") || isDisappearing) return;
        isDisappearing = true;
        Invoke(nameof(disappear), disappearTime);
    }

    private void disappear()
    {
        _spriteRenderer.color -= new Color(0, 0, 0, 0.5f);
        _boxCollider2D.enabled = false;
        Invoke(nameof(reappear), reappearTime);
        isDisappearing = false;
    }

    private void reappear()
    {
        _spriteRenderer.color += new Color(0, 0, 0, 0.5f);
        _boxCollider2D.enabled = true;
    }
}