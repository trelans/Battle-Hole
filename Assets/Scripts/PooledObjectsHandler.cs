using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjectsHandler : MonoBehaviour
{
    private static PooledObjectsHandler Instance = null;
    private void Awake() 
    { 
      HandleSingleton();
    }

    private void HandleSingleton()
    {
        int count = FindObjectsOfType(GetType()).Length;
        if (count > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject); 
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
