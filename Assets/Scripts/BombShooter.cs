using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShooter : MonoBehaviour
{

    [SerializeField]
    private bool isShooting = false;

    private float magnitude = 25f;

    private float inverse = -1;

    private float timeAfterShoot = 0;
    private float enemySideCooldown = 2;

    private float explosionTime = 6;

    [SerializeField] private GameObject line;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
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
        return transform.position.y <= line.transform.position.y;
    }

    private bool Explode()
    {
        gameObject.SetActive(false);
    }


}


