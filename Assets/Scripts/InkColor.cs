using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkColor : MonoBehaviour
{ 
    
    private Color colorInk;   
    private float time;
    
   // [SerializeField] private String c_Color = "#FF3335";
    private SpriteRenderer _spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        
        _spriteRenderer.material.color = colorInk;
        /*
        if (ColorUtility.TryParseHtmlString(c_Color, out colorInk))
        {
            _spriteRenderer.material.color = colorInk;
        }
        */
      

    }

    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
      
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        colorInk.a = (1.5f - time) / 1.5f;
        _spriteRenderer.color = colorInk;
        if (time > 1.5f)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetColor(Color color)
    {
        colorInk = color;
  
    }
}
