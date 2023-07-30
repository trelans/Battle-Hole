using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillObject : MonoBehaviour
{
   [SerializeField] private SpriteRenderer spriteRenderer;

   private void Start()
   {
      spriteRenderer.material.SetFloat("_Arc2", 360);
   }



   public void ChangeFillAmount(float amount)
   {
    
      spriteRenderer.material.SetFloat("_Arc2",   360-(360*amount));
   }
}
