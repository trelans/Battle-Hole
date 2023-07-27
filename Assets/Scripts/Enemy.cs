using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private Animator _animator;

    private bool isDead;

    private float speed;

    [SerializeField] private bool stationary;
    private bool isShootable;
    
    // Movement
    private bool isMoving;

    private Ink ink;

    private bool isTimeStarted;

    private bool isLastEnemyOfWave;


    private bool isPositionCheckOn;
    
    private float minDistanceToSlow;

    private EnemyGeneratorController _enemyGeneratorController;
    private bool enemyGeneratedWithGenerator;

    private bool isHandled;
    private GameObject tank;
    private float startPosX;
    private float pushForceMagnitude;
    private bool isHut;

    private bool isIncreasedProgress;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        ink = transform.parent.GetChild(1).GetComponent<Ink>();
        
        _rigidbody = GetComponent<Rigidbody>();
        
    }

    private void Start()
    {
        
        startPosX = gameObject.transform.parent.position.x;
        isIncreasedProgress = false;
        minDistanceToSlow = 15;
        isDead = false;
        isMoving = true;
        isShootable = false;
        isTimeStarted = false;
        isPositionCheckOn = false;
        isHandled = false;
        pushForceMagnitude = 250;
        if (stationary)
            SetShootable();
    }

    private void Update()
    {
        if (isTimeStarted)
        {
            if (isMoving)
            {
                transform.position += -Vector3.forward * speed * Time.deltaTime;
            }
   
        }

/*
        if (enemyGeneratedWithGenerator && isTimeStarted && !isHut)
        {
            float currentDistance 
        
            Debug.Log("Distance " + currentDistance);
                if (currentDistance <= minDistanceToSlow)
                {
                   _enemyGeneratorController.SlowPlayer();
                }

                if (currentDistance <= 0 )
                {
                    if (!isHandled)
                    { _enemyGeneratorController.IncrementHandledEnemies();
                        isHandled = true;
                    } 
                 
                }
        
        }
      */

      
    }


    public void SetIsTimeStarted(bool b)
    {
        isTimeStarted = b;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("sdf");
        if (other.gameObject.tag == "Ammo" && isShootable)
        {
            Debug.Log("ssdfdf");
            DieEnemy(false);
        
            other.gameObject.SetActive(false);
        }

   
        if (other.gameObject.tag == "Player" && isShootable)
        {
            PushEnemy();
            DieEnemy(true);
        }
    }

    public void DieEnemy(bool isFly)
    {
        if (enemyGeneratedWithGenerator && !isHandled && !isHut)
        {
            isHandled = true;
            _enemyGeneratorController.IncrementHandledEnemies();
        }
        
     
       

        gameObject.GetComponent<BoxCollider>().enabled = false;
        isMoving = false;
        if (!isDead)
        {
          
           HandleDeathAnim(isFly);
            StartCoroutine(WaitInk());
         
            isDead = true;
     
            if (!isIncreasedProgress)
            {
            
                isIncreasedProgress = true;
            }
            ink.ActivateBlood(transform.position);
        }
       
    }

    private void HandleDeathAnim(bool isFly)
    {
        if (!isFly)
        {
            int random = UnityEngine.Random.Range(0, 5);
            if (random == 0)
            {
                _animator.SetTrigger("Death");
            } 
            else if (random == 1)
            {
                _animator.SetTrigger("Death2");
            }
            else if (random == 2)
            {
                _animator.SetTrigger("Death3");
            }
            else if (random == 3)
            {
                _animator.SetTrigger("Death4");
            
            }
            else if (random == 4)
            {
                _animator.SetTrigger("Death5");
            }
        }
        else
        {
            _animator.SetTrigger("Flyy");
        }
      
      
    }
  

    IEnumerator WaitInk()
    {
           
        yield return new WaitForSeconds(0.3f);
        ink.Paint(transform.position);
    }
    public void SetSpeed(float s)
    {
        speed = s;
    }

    public void SetIsMoving(bool b)
    {
        isMoving = b;
    }

    public void SetEnemyGenerator(EnemyGeneratorController e)
    {
        enemyGeneratedWithGenerator = true;
        _enemyGeneratorController = e;
    }

    public void SetShootable() {
        isShootable = true;
    }

    public void SetIsLastEnemyOfWave(bool b)
    {
        isLastEnemyOfWave = b;
    }
    
    private void OnCollisionEnter(Collision other) {
    
    }
    

    public void PushEnemy()
    {
        Vector3 direction = Vector3.forward + new Vector3(gameObject.transform.parent.position.x + startPosX, 0, 0);
        _rigidbody.AddForce(direction * pushForceMagnitude);
        _rigidbody.AddForce(Vector3.up * (pushForceMagnitude/ 3));
    }

    public void SetIsHut(bool b)
    {
        isHut = b;
    }
}
