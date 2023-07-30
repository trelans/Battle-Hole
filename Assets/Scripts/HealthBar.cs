using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  [SerializeField] private Image slider;
  private float currentHealth;
  private GameHandler gameHandler;

  private void Awake()
  {
    gameHandler = FindObjectOfType<GameHandler>();
    
  }

  private void Start()
  {
    currentHealth = 1;
  }

  public void DecreaseHealth()
  {
    currentHealth -= 0.1f;
    slider.fillAmount = currentHealth;
    if (currentHealth < 0)
    {
      //GameOver
      gameHandler.GameOver();
    }
  }
  
}
