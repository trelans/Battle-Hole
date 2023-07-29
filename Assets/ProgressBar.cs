using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float duration = 2f; // Total duration in seconds for the width to increase from 0 to 1.
    private float elapsedTime = 0f;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private TuretTargeting turret;
    private bool on;
    [SerializeField] private SpriteRenderer[] sprites;
    private destroyCubes d;

    private void Awake()
    {
        d = FindObjectOfType<destroyCubes>();
      
    }

    void Update()
    {
        if (on)
        {
            // Check if the width has already reached 1, if so, there's no need to proceed.
            if (spriteRenderer.size.x >= 1f)
            {
                turret.SetIsFireStarted(true);
                on = false;
                SetSprites(false);
                d.ResetAmmoCount();
                
            }
        
            

            // Increment elapsed time by the time passed since the last frame.
            elapsedTime += Time.deltaTime;

            // Calculate the percentage of time passed relative to the total duration.
            float t = Mathf.Clamp01(elapsedTime / duration);

            spriteRenderer.size = new Vector2(t, spriteRenderer.size.y);
        }
      
    }

    public void ResetTime()
    {
        SetSprites(true);
        spriteRenderer.size = new Vector2(0, spriteRenderer.size.y);
        elapsedTime = 0;
        on = true;
    }

    private void SetSprites(bool b)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].enabled = b;
        }
    }
}
