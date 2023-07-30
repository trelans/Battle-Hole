using System;
using UnityEngine;

public class TuretTargeting : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public Transform turretHead;
    public GameObject ammoQueue;
    public float rotationSpeed = 5f;
    public float shootingInterval = 1f;
    public float shootingForce = 100f;
    public float range = 10f; // The range of the turret's sphere detection

    private Transform target;
    private float lastShotTime;

    [SerializeField] private ProgressBar progressBar;
    private bool isFireStarted;
    private destroyCubes cubes;
    private int ammoCount;
    private int maxAmmo;
    private void Awake()
    {
        cubes = FindObjectOfType<destroyCubes>();
    }

    void Start()
    {

    }

    void Update()
    {
        Debug.Log(turretHead.rotation.eulerAngles);
        if (target != null)
        {
            Vector3 targetDirection = target.position - turretHead.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            turretHead.rotation = Quaternion.Slerp(turretHead.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Shoot at the target if enough time has passed since the last shot
            if (Time.time - lastShotTime >= shootingInterval)
            {
                Shoot();
                lastShotTime = Time.time;
            }
        }
    }

    void Shoot()
    {
        if (isFireStarted)
        {

            ammoCount = ammoQueue.transform.childCount;
            if (ammoQueue != null && ammoCount > 0)
            {
                Transform ammoToShoot = ammoQueue.transform.GetChild(0);
                ammoToShoot.gameObject.SetActive(true);

                float adjust = 0.002f;
                
                if (progressBar.isBomb)
                {
                    adjust = adjust * 8; 
                }
                    ammoToShoot.localScale =  new Vector3(ammoToShoot.localScale.x,ammoToShoot.localScale.y,ammoToShoot.localScale.z )* adjust;


                    GameObject ammoInstance = Instantiate(ammoToShoot.gameObject, ammoQueue.transform.position,
                                ammoQueue.transform.rotation);
                // Instantiate the ammo (bullet) at the ammoQueue's position and rotation

              
           

         
                // Apply force to shoot the ammo towards the target
                Rigidbody ammoRigidbody = ammoInstance.GetComponent<Rigidbody>();
                if (ammoRigidbody != null)
                {
                    Vector3 shootingDirection = (target.position - ammoQueue.transform.position).normalized;
                    Quaternion rotation = Quaternion.LookRotation(shootingDirection);
                    ammoInstance.transform.rotation = rotation;
                    ammoInstance.transform.Rotate(90,0,0); 

                    if (progressBar.isBomb)
                    {
                        Vector3 direction = shootingDirection + Vector3.up; 
                        float force = 25;
                        ammoRigidbody.velocity = Vector3.zero;
                        ammoRigidbody.AddForce(direction * force, ForceMode.Impulse);
                    }
                    else
                    {
                        ammoRigidbody.AddForce(shootingDirection * shootingForce, ForceMode.Impulse);
                    }
                }

                // Remove the ammo from the queue (you may need to handle ammo depletion in a more sophisticated way)
                Destroy(ammoToShoot.gameObject);
                ammoCount--;
            }
            else
            {
                SetIsFireStarted(false);
            }
        }
    }

    public void SetIsFireStarted(bool b)
    {
        isFireStarted = b;
        if (b)
        {
            maxAmmo = ammoQueue.transform.childCount;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            target = other.transform;
        }
        
        if (other.CompareTag("Player"))
        {
            if (!isFireStarted && cubes.GetCurrentAmmo(progressBar.isBomb) > 0)
            {
                progressBar.ResetTime();
                isFireStarted = true;
            }
        
            
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            if (other.transform == target)
            {
                target = null;
            }
        }
    }



}
