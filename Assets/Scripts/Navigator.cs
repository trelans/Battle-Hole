using System;
using System.Collections;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    private bool MoveByTouch;
    private Vector3 _mouseStartPos, PlayerStartPos;
    [SerializeField] [Range(0f,100f)]private float maxAcceleration;
    
    [Header("Gravity")] 
    
    [Range(1f,1000f)]public float power = 2f; // gravity power
    [Range(-10f, 10f)] public float upOrDown; // direction of gravity
    [Range(1f,10f)]public float forceRange = 1f; // range of gravity

    public ForceMode forceMode; // force type
    public LayerMask layerMask; // determines which layer should be affected by gravity

    private bool isFull;
    // Update is called once per frame
    public float moveSpeed = 10f; // Adjust this value to control the movement speed
    private void Awake()
    {
     
    }

    void Start()
    {
        
    }



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveByTouch = true;

            Plane plane = new Plane(Vector3.up, 0f);

            float Distance;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out Distance))
            {
                _mouseStartPos = ray.GetPoint(Distance);
                PlayerStartPos = transform.position;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            MoveByTouch = false;
        }

        if (MoveByTouch)
        {
            Plane plane = new Plane(Vector3.up, 0f);
            float Distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out Distance))
            {
                Vector3 MousePos = ray.GetPoint(Distance);
                Vector3 move = MousePos - _mouseStartPos;
                Vector3 navigator = PlayerStartPos + move;

                // navigator.x = Mathf.Clamp(navigator.x, -5f, 5f);
                // navigator.z = Mathf.Clamp(navigator.z, -5f, 5f);

                navigator = Vector3.ClampMagnitude(navigator, 30f);

                // Instead of using Lerp, use MoveTowards to achieve constant speed movement
                transform.position = Vector3.MoveTowards(transform.position, navigator, Time.deltaTime * moveSpeed);
            }
        }
    }


    private void FixedUpdate()
    {
        if (!isFull)
        {
            Gravity(transform.position,forceRange,layerMask);
        }
       
    }

    private void Gravity(Vector3 gravitySource, float range, LayerMask layerMask)
    {
        Collider[] objs = Physics.OverlapSphere(gravitySource, range, layerMask);

        for (int i = 0; i < objs.Length; i++)
        {
            Rigidbody rbs = objs[i].GetComponent<Rigidbody>();
            
            // Vector3 forceDirection = new Vector3(gravitySource.x,upOrDown,gravitySource.z) - objs[i].transform.position;

            Vector3 forceDirection = -Vector3.up;
       
            rbs.AddForceAtPosition(power * forceDirection.normalized,gravitySource);

            if (objs[i].tag == "Enemy")
            {
                objs[i].GetComponent<Outline>().enabled = false;
            }

      
        }
    }

    public void SetIsFull(bool b)
    {
        isFull = b;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,forceRange);
    }

    IEnumerator KillEnemy(Collider collider)
    {
        yield return new WaitForSeconds(0.3f);
        collider.gameObject.SetActive(false);
        
    }
}

