using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour
{
    [SerializeField] private int horizontalAmount;
    [SerializeField] private int verticalAmount;
    [SerializeField] private float verticalDistanceBetweenObjs;
    private float horizontalDistanceBetweenObjs;
    [SerializeField] private float speedOfObjects; 
    private ObjectPool objectPool;
    private List<float> leavingSpeeds;
    private float leavingPeriod;
    private float leavingSpeed;
    
    private float leftSecondsAgo;
    private int currentOrder;
    private bool isFinished;

    private List<Enemy> activeObjectsEnemies;
    private List<GameObject> activeObjects;

    private float time;
    private bool isTimeStarted;

    private int h_enemyCount;
    private int enemyCount;

    private bool isSlowed;

   [SerializeField] private bool isHut;

   private GameObject enemyPooler;
   
    private void Awake()
    {
        enemyPooler = GameObject.FindGameObjectWithTag("EnemyPooler");
        objectPool = enemyPooler.GetComponent<ObjectPool>();
    
    }

    void Start()
    {
        isTimeStarted = true;
        isSlowed = false;
        horizontalDistanceBetweenObjs = 0;
        leavingSpeeds = new List<float> {1, 3.5f, 1.9f, 1.25f, 0.92f, 0.8f, 0.65f, 0.5f};
        horizontalAmount = horizontalAmount + 1;
        leftSecondsAgo = 0;
        currentOrder = 1;
        isFinished = false;
        Vector3 positionSave;
        Vector3 curPosition = gameObject.transform.position;
        leavingSpeed = leavingSpeeds[(horizontalAmount - 1) / 2];
        leavingSpeed = leavingSpeed * speedOfObjects / 2;
        // leavingPeriod = 0.5f / (speedOfObjects / 2);
        leavingPeriod = 0.5f;
        activeObjects = new List<GameObject>();
        activeObjectsEnemies = new List<Enemy>();
        for (int i = 0; i < horizontalAmount; i++)
        {
            positionSave = curPosition;
            for (int x = 0; x < verticalAmount; x++)
            {
                GameObject curObject = objectPool.GetPooledObject();
               
                curObject.transform.position = curPosition;
                curObject.SetActive(true);
                curObject.transform.localEulerAngles = new Vector3(0, 180, 0);
                activeObjects.Add(curObject);
                Enemy curEn = curObject.transform.GetChild(0).GetComponent<Enemy>();
                activeObjectsEnemies.Add(curEn);
                curEn.SetShootable();
                curEn.SetSpeed(speedOfObjects);
                curEn.SetEnemyGenerator(this);
                curEn.SetIsHut(isHut);
                curPosition += new Vector3(0, 0, verticalDistanceBetweenObjs);
                curObject.name = (activeObjects.Count).ToString();
            
            }
            curPosition = positionSave;
            curPosition += new Vector3(horizontalDistanceBetweenObjs, 0, 0);
            
        }
        activeObjects[activeObjects.Count / 2].SetActive(false);
        
        h_enemyCount = 0;
        enemyCount = activeObjects.Count-4;
        isHut = false;
    }   
    

    void Update() 
    {
        float randomFloat1 = 0;
        float randomFloat2 = 0; 
        if (isTimeStarted)
        {
            if (currentOrder < verticalAmount + 1)
            {
                if (leftSecondsAgo > leavingPeriod)
                {
                    
                    currentOrder = currentOrder + 1;
                    leftSecondsAgo = 0;
                    randomFloat1 = UnityEngine.Random.Range(50f, 350f);
                    randomFloat2 = UnityEngine.Random.Range(50f, 350f);
                    
                }


                leftSecondsAgo = leftSecondsAgo + Time.deltaTime;
                MoveEnemiesToSides(currentOrder, randomFloat1, randomFloat2);
                ActivateTime();

                

            }
            else
            {
                isFinished = true;
            }
        }

    }

    public void SetIsTimeStarted(bool b)
    {
        isTimeStarted = b;
    }

    public void IncrementHandledEnemies()
    {
        h_enemyCount++;
   
    }

    public GameObject GetEnemyAmmoTarget()
    {
        GameObject r = activeObjects[activeObjects.Count-1];
        activeObjects.Remove(activeObjects[activeObjects.Count-1]);
        return r;
    }
    public void RemoveActive(GameObject g)
    {
        activeObjects.Remove(g);
    }

    public int GetEnemyCount()
    {
        return activeObjects.Count;
    }
    private void ActivateTime()
    {
        foreach (var enemy in activeObjectsEnemies)
        {
            enemy.SetIsTimeStarted(true);
        }
    }
    private void MoveEnemiesToSides(int order, float random1, float random2)
    {
     
        for (int i = 0; i < horizontalAmount / 2; i++)
        {
            int curIndex = order + i * verticalAmount - 1;
            if (curIndex < activeObjects.Count && curIndex > 0)
            {
                activeObjects[curIndex].transform.position += new Vector3(random1 * Time.deltaTime, 0, 0); 
                activeObjects[curIndex].transform.GetChild(0).GetComponent<Enemy>().SetShootable();
            }
        }

        for (int i = horizontalAmount / 2; i < horizontalAmount; i++)
        {
            int curIndex = order + i * verticalAmount - 1;
            if (curIndex < activeObjects.Count && curIndex > 0)
            {
                activeObjects[curIndex].transform.position += new Vector3(-random2 * Time.deltaTime, 0, 0); 
                activeObjects[curIndex].transform.GetChild(0).GetComponent<Enemy>().SetShootable();
            }
        }
        activeObjects[0].transform.GetChild(0).GetComponent<Enemy>().SetShootable();
        
    }


    public bool GetFinished()
    {
        return isFinished;
    }



}
