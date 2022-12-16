using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    //If this was a bigger demo, this would use an object pool
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private bool firesRight;
    [SerializeField] private float x;
    [SerializeField] private float y;
    
    private void Start()
    {
        InvokeRepeating(nameof(FireProjectile), 0f, 4f);
    }

    void FireProjectile()
    {
        var projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectileObject.GetComponent<ProjectileController>().launch(new Vector2(x, y));
    }
}