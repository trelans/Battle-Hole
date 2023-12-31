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

    [SerializeField]
    public bool isBomb;
    [SerializeField] private SpriteRenderer[] sprites;
    private destroyCubes d;
    [SerializeField] private FillObject fillObject;
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
                d.ResetAmmoCount(isBomb);
                
            }
        
            

            // Increment elapsed time by the time passed since the last frame.
            elapsedTime += Time.deltaTime;

            // Calculate the percentage of time passed relative to the total duration.
            float t = Mathf.Clamp01(elapsedTime / duration);

            spriteRenderer.size = new Vector2(t, spriteRenderer.size.y);
            fillObject.ChangeFillAmount(t);
        }
      
    }


    public void FillBarUpdate(float amount)
    {
        fillObject.ChangeFillAmount(amount);
    }
    public void ResetTime()
    {
        SetSprites(true);
        spriteRenderer.size = new Vector2(0, spriteRenderer.size.y);
        elapsedTime = 0;
        on = true;
        fillObject.ChangeFillAmount(0);
    }
    public void CloseTime()
    {
        SetSprites(false);
        spriteRenderer.size = new Vector2(0, spriteRenderer.size.y);
        elapsedTime = 0;
        on = false;
        fillObject.ChangeFillAmount(0);
    }

    private void SetSprites(bool b)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].enabled = b;
        }
    }

    public bool IsReady()
    {
        return ((elapsedTime / duration) < 1);
    }
}
