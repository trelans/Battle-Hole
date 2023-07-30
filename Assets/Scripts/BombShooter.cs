using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombShooter : MonoBehaviour
{


    [SerializeField]
    private bool isShooting = false;

    public float magnitude = 50.5f;

    [SerializeField]
    private HealthBar healthBar;

    private GameManager manager;
    private float inverse = -1;

    private float timeAfterLanding = 0;
    private float enemySideCooldown = 6;

    private int explosionTime = 30;

    bool stick = false;

    [SerializeField] private GameObject line;

    private Rigidbody rb;

    [SerializeField] private TextMeshPro bombText;

    [SerializeField] private GameObject particle;
    [SerializeField] private GameObject explosions;
    
    // Start is called before the first frame update
    private void Awake()
    {
        healthBar = FindObjectOfType<HealthBar>();
        line = GameObject.FindWithTag("Line");
        manager = FindObjectOfType<GameManager>();
        explosions = GameObject.FindWithTag("Explosions");;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (!manager.ticking)
        {
            manager.explostionTime = explosionTime;
            manager.StartRoutine();
        }
     
    }

    // Update is called once per frame
    void Update()
    {

        if (IsOnGround())
        {
            if (manager.exploded)
            {
                Explode();
            }
            else
            {
                bombText.SetText(manager.explostionTime.ToString());
            }

            if (stick)
            {
                rb.velocity = Vector3.zero;
                stick = false;
            }

            if (!IsOnPlayerSide() && timeAfterLanding > enemySideCooldown)
            {
                Shoot();
                timeAfterLanding = 0;
            }


            timeAfterLanding += Time.deltaTime;
        }
        else
        {
            stick = true;
        }

    

    }

// Bana doğru gelen
    private void Shoot()
    {
            Vector3 direction = Vector3.up + Vector3.forward;
            direction = direction * magnitude;
            rb.velocity = Vector3.zero;
            rb.AddForce(direction.x, direction.y, -direction.z, ForceMode.Impulse);
            isShooting = false;
    }

    private bool IsOnPlayerSide()
    {
        return transform.position.z <= line.transform.position.z; 
    }

    private bool IsOnGround()
    {
        return transform.position.y <= line.transform.position.y + 4;
    }

    private void Explode()
    {
        particle.transform.SetParent(explosions.transform);
        particle.SetActive(true); 
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2);
        if (!gameObject.activeSelf || IsOnPlayerSide())
        {
            healthBar.DecreaseHealth();
            gameObject.SetActive(false);
            return;
        }
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == 7)
            {
                hitCollider.gameObject.GetComponent<Enemy>().DieEnemy(false);
            }
        }
    
        gameObject.SetActive(false);
       
    }


}


