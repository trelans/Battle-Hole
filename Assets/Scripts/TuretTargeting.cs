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
    private LineRenderer rangeIndicator; // LineRenderer to draw the circle range
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
        // Create and set up the LineRenderer
        rangeIndicator = gameObject.AddComponent<LineRenderer>();
        rangeIndicator.material = new Material(Shader.Find("Standard"));
        rangeIndicator.material.color = Color.gray;
        rangeIndicator.loop = true;
        rangeIndicator.startWidth = 0.1f;
        rangeIndicator.endWidth = 0.1f;
        UpdateRangeIndicator();
    }

    void Update()
    {
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
                ammoToShoot.localScale =  new Vector3(ammoToShoot.localScale.x,ammoToShoot.localScale.y,ammoToShoot.localScale.z )* 0.002f;
                // Instantiate the ammo (bullet) at the ammoQueue's position and rotation
                GameObject ammoInstance = Instantiate(ammoToShoot.gameObject, ammoQueue.transform.position,
                    ammoQueue.transform.rotation);

              
           

         
                // Apply force to shoot the ammo towards the target
                Rigidbody ammoRigidbody = ammoInstance.GetComponent<Rigidbody>();
                if (ammoRigidbody != null)
                {
                    Vector3 shootingDirection = (target.position - ammoQueue.transform.position).normalized;
                    Quaternion rotation = Quaternion.LookRotation(shootingDirection);
                    ammoInstance.transform.rotation = rotation;
                    ammoInstance.transform.Rotate(90,0,0); 
                    ammoRigidbody.AddForce(shootingDirection * shootingForce, ForceMode.Impulse);
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
            if (!isFireStarted && cubes.GetCurrentAmmo() > 0)
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

    void OnDrawGizmosSelected()
    {
        UpdateRangeIndicator();
        
    }

    void UpdateRangeIndicator()
    {
        if (rangeIndicator == null)
            return;

        // Draw the half circle range on the XZ plane (center's Y position)
        int segments = 180; // Change this to 180 for half circle
        float[] angles = new float[segments + 1];
        float rotationAngle = -Mathf.PI / 2f; // -90 degrees in radians (opposite direction)
        for (int i = 0; i <= segments; i++)
        {
            // Divide by 2 to get half circle, and add the rotation angle to rotate counterclockwise
            float angle = (Mathf.PI * 1f / segments) * i + rotationAngle; 
            angles[i] = angle;
        }

        Vector3[] points = new Vector3[segments + 1];
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(angles[i]) * range;
            float z = Mathf.Cos(angles[i]) * range;
            points[i] = new Vector3(x + transform.position.x, transform.position.y, z + transform.position.z);
        }

        rangeIndicator.positionCount = segments + 1;
        rangeIndicator.SetPositions(points);
    }



}
