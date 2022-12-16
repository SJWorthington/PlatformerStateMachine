using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private int moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void launch(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * moveSpeed);
    }

    public void bashLaunch(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(direction * moveSpeed * 4);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}