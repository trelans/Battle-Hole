using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShooter : MonoBehaviour
{

    [SerializeField]
    private bool isShooting = false;

    private float magnitude = 8.5f;

    [SerializeField]
    private HealthBar healthBar;

    private float inverse = -1;

    private float timeAfterLanding = 0;
    private float enemySideCooldown = 2;

    private int explosionTime = 8;

    bool stick = false;

    [SerializeField] private GameObject line;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        healthBar = FindObjectOfType<HealthBar>();
        StartCoroutine(Countdown(explosionTime)); 
    }

    // Update is called once per frame
    void Update()
    {

        if (IsOnGround())
        {
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

        Debug.Log(stick);

    }

    IEnumerator Countdown (int seconds)
    {
        yield return new WaitForSeconds(seconds);

        Explode();
    }

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


