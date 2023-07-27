using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutDestroyHandler : MonoBehaviour
{
    [SerializeField] private MeshDestroy[] _meshDestroys;
 
    

    private float tranparencyValue;
    private float interpolationValue;
    
    private bool willFade;
   [SerializeField]  private float fadeSpeed;

   private List<Material> _materialsFade;

   [SerializeField] private GameObject particle;
    private void Start()
    {
        willFade = false;
        tranparencyValue = 1;
        interpolationValue = 0;
      _materialsFade = new List<Material>();
      
    }

    private void FixedUpdate()
    {
        if (willFade)
        {
            interpolationValue += (Time.deltaTime * fadeSpeed) * 0.1f;
           tranparencyValue =  1 - interpolationValue;
           
           if (tranparencyValue <= 0)
           {
               tranparencyValue = 0;
               willFade = false;
           }
           
           HandleFade();
        }
        
    }

    public void AddMaterialsToList(Material mat)
    {
        _materialsFade.Add(mat);
    }
    public void DestroyBuilding()
    {

        particle.SetActive(true);
        for (int i = 0; i < _meshDestroys.Length; i++)
        {
            if (_meshDestroys[i] != null)
            {
                _meshDestroys[i].DestroyMesh();  
            }
          
        }

        // Coroutine can be put
        willFade = true;

    }

  
    public void HandleFade()
    {
        for (int i = 0; i < _materialsFade.Count; i++)
        {
             Color oldColor = _materialsFade[i].color;
             _materialsFade[i].color = new Color(oldColor.r,oldColor.g,oldColor.b,tranparencyValue);
        }
    
       
    }
}
