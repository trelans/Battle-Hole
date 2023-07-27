using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hut : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private TextMeshPro tmp_Health;
    private HutDestroyHandler _hutDestroyHandler;
    [SerializeField] private EnemyGeneratorController generator;
    private bool isDestroyed;
    [SerializeField] private ParticleSystem _particleSystem;
   [SerializeField] private bool isEnemyExists;


   
   
    private void Awake()
    {
       _hutDestroyHandler =  GetComponent<HutDestroyHandler>();

    }

    private void Start()
    {
        tmp_Health.SetText(health.ToString());
        isDestroyed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ammo" && generator.GetFinished())
        {
            Debug.Log("game obj: " + gameObject.name);
            DecreaseHealth();
        }
        
    }

    public void DecreaseHealth()
    {
      
        if (health > 0)
        {
            health--;
            if ( _particleSystem != null )
            {
                _particleSystem.Play();
            }
      
            // BURDA BUG OLUO MISSING OBJ
            tmp_Health.SetText(health.ToString());
            StartCoroutine(HandleSize());

            if (health == 0)
            {
            
                HandleDestroy();
            }
        }
        else
        {
      
         HandleDestroy();
      
        }
    
        
    }

    private void HandleDestroy()
    {

        tmp_Health.gameObject.SetActive(false);
        if (!isDestroyed)
        {   
            _hutDestroyHandler.DestroyBuilding();
            isDestroyed = true;
        }
    }
    IEnumerator HandleSize()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(1, 1, 1);
    }
    
    
}
