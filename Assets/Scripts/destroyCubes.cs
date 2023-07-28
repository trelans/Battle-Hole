using System;
using UnityEngine;
using UnityEngine.UI;

public class destroyCubes : MonoBehaviour
{

    private float value;
    private EnemyShooter enemyShooter;

    private void Awake()
    {
        enemyShooter = FindObjectOfType<EnemyShooter>();
    }

    private void Start()
    {
        value = 100f / GameObject.FindGameObjectsWithTag("block").Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("block"))
        {
            other.gameObject.SetActive(false);
            
        }
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<Enemy>().SetIsMoving(false);
            other.gameObject.tag = "EnemyAmmo";
            

        }
        
        enemyShooter.AddAmmoToQueue(  other.gameObject);

    }
}
