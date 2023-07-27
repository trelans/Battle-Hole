using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMover : MonoBehaviour
{

  [SerializeField] private float speed;
  private DynamicJoystick _dynamicJoystick;
  private Rigidbody _rigidbody;


  private bool isRunning;

  private bool isTouched;

 
  private Transform child;

  private bool increasingSpeed;

  private float forceAmount;
  private void Awake()
  {


    _rigidbody = GetComponent<Rigidbody>();
    _dynamicJoystick = FindObjectOfType<DynamicJoystick>();

  }

  private void Start()
  {
    isRunning = false;
    isTouched = false;
    increasingSpeed = false;
    forceAmount = 2;
  }

  private void FixedUpdate()
  {
    if (isRunning )
    {
 
     _rigidbody.velocity = new Vector3(_dynamicJoystick.Horizontal * speed, _rigidbody.velocity.y,
       _dynamicJoystick.Vertical * speed);

    }
    else if (increasingSpeed)
    {
      Vector3 vector = _rigidbody.velocity.normalized;
      vector = new Vector3(0, vector.y, vector.z);
      _rigidbody.AddForce(vector * forceAmount);
     
    }
    else
    {
      _rigidbody.velocity = Vector3.zero;
    }

  

 
 
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Booster")
    {
      forceAmount = 30;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.tag == "Booster")
    {
      forceAmount = 2;
    }
  }

  private void Update()
  {
    if (Input.touchCount > 0)
    {                                       
      Touch finger = Input.GetTouch(0);

      if (finger.phase == TouchPhase.Began)
      {
        increasingSpeed = false;
        isTouched = true;
        DecreaseSpeed();
      }
      if (finger.phase == TouchPhase.Moved)
      {
      

        if ( !isRunning ) 
        {
     
           // _animator.SetTrigger("Running"); sound
            isRunning = true;
        }
        
      }
      if (finger.phase == TouchPhase.Ended)
      {
        isRunning = false;
        isTouched = false;

       
       // _animator.SetTrigger("IdleBall");
      //  IncreaseSpeeed();
       
        
        

      }
    }

  }
  public void DecreaseSpeed()
  {
    speed /= 1.2f;
    
  }
  public void IncreaseSpeed()
  {
    speed *= 1.2f;
    
  }
  
  public void IncreaseSpeeed()
  {
    increasingSpeed = true;
 

  }
  
  
}
