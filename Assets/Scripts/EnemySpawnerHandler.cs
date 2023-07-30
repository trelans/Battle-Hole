using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerHandler : MonoBehaviour
{
   private List<EnemySpawnerHandler> enemySpawnerHandlers;
   public GameObject enemySpawnerPrefab;
   public int count;
   private int currentSpawnCount;
   public float interval;
   private float currentTime;

   private void Start()
   {
        CreateSpawner();
   }

   private void Update()
   {
       currentTime += Time.deltaTime;

       if (currentTime >= interval && currentSpawnCount < count)
       {    
           currentSpawnCount++;
           currentTime = 0;
           CreateSpawner();
       }
       
   }

   private void CreateSpawner()
   {
      GameObject obj =  Instantiate(enemySpawnerPrefab, enemySpawnerPrefab.transform.position, enemySpawnerPrefab.transform.rotation);
      obj.transform.SetParent(transform);
      obj.SetActive(true);
   }
}
