using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public Sprite laserOnSprite;
    public Sprite laserOffSprite;
    public float rotationSpeed;

    private SpriteRenderer laserSpriteRenderer;

    private Collider2D _collider2D;

    private bool isLaserOn;

    public float laserCoolDown =.5f;
    private float lastTimeLaserOn;

    private void Start()
    {
        laserSpriteRenderer = GetComponent<SpriteRenderer>();   
        _collider2D = GetComponent<Collider2D>();  
        lastTimeLaserOn =laserCoolDown;

    }

    private void Update()
    {
        lastTimeLaserOn -= Time.deltaTime;

        if(lastTimeLaserOn < 0)
        {
            ToggleLaser();
            lastTimeLaserOn = laserCoolDown;
        }

        transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);  // Rotate z axi(obviously)
    }

    private void ToggleLaser()
    {
        isLaserOn = !isLaserOn;
        _collider2D.enabled = isLaserOn;

        if (isLaserOn)
        {
            laserSpriteRenderer.sprite = laserOnSprite;
        }
        else
        {
            laserSpriteRenderer.sprite = laserOffSprite;
        }
    }
}
