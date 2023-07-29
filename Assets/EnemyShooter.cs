using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
  
    private Transform shootFromPoint; // The point from where ammo will be shot

    private Queue<GameObject> ammoQueue = new Queue<GameObject>();
    public float shootForce;
    private int ammoCount;
    private bool isFiring;
    private float interval = 0.2f;

    private EnemyGeneratorController enemyGeneratorController;

    private void Awake()
    {
        enemyGeneratorController = FindObjectOfType<EnemyGeneratorController>();
    }

    private void Start()
    {
        shootFromPoint = transform;
        ammoCount = 0;
        isFiring = false;
  
        ammoQueue = new Queue<GameObject>();
    }



    public void AddAmmoToQueue(GameObject gameObject)
    {
    
        ammoQueue.Enqueue(gameObject);
        ammoCount++;
        if (ammoCount > 0 && !isFiring)
        {
            isFiring = true;
            StartCoroutine(Fire());
        }
   
    }


    IEnumerator Fire()
    {
        GameObject ammoToShoot = ammoQueue.Dequeue();
        ammoToShoot.transform.position = shootFromPoint.position;
        ammoToShoot.SetActive(true);
        ammoToShoot.GetComponent<AmmoController>().FindNearestEnemy();
        ammoToShoot.transform.Rotate(90,0,0);
        ammoToShoot.GetComponent<Rigidbody>().useGravity = false;
        if (ammoToShoot.tag == "EnemyAmmo")
        {
            ammoToShoot.GetComponent<Enemy>().UseAmmo(enemyGeneratorController.GetEnemyAmmoTarget().transform.position);
        }
       
        yield return new WaitForSeconds(interval);
        ammoCount--;
        if (ammoCount > 0)
        {
            StartCoroutine(Fire());
        }
        else
        {
            isFiring = false;
        }
    }
}
