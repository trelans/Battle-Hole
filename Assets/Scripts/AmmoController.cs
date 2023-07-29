using System;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    public float speed = 10f; // Speed at which the ammo moves towards the enemy
    public string enemyTag = "Enemy"; // Tag of the enemy GameObjects

    private Transform targetEnemy;
    private EnemyGeneratorController enemyGeneratorController;

    private void Awake()
    {
        enemyGeneratorController = FindObjectOfType<EnemyGeneratorController>();
    }

    private void Start()
    {
     
    }

    private void Update()
    {
        if (targetEnemy != null)
        {
            MoveTowardsEnemy();
        }
    }

    private void MoveTowardsEnemy()
    {

        Vector3 direction = Vector3.forward;
        if (targetEnemy.gameObject.activeSelf == true)
        {
             direction = (targetEnemy.position - transform.position).normalized;
        }

       
        transform.position += direction * speed * Time.deltaTime;

        // Check if the ammo has reached the enemy
        float distanceToEnemy = Vector3.Distance(transform.position, targetEnemy.position);
        if (distanceToEnemy < 0.3f)
        {
            Enemy enemy =  targetEnemy.GetComponent<Enemy>();
            if (enemy != null)
            {
               enemy.DieEnemy(false); 
            }

        }
    }

    public void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        if (enemies.Length > 0)
        {
            Transform closestEnemy = enemies[0].transform;
            float closestDistance = Vector3.Distance(transform.position, closestEnemy.position);

            foreach (GameObject enemy in enemies)
            {
                if (!enemy.GetComponent<Enemy>().GetWillDie() )
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = enemy.transform;
                        closestEnemy.GetComponent<Enemy>().SetWillDie(true);
                    }
                }
                
            }

            targetEnemy = closestEnemy;
        }
    }
}