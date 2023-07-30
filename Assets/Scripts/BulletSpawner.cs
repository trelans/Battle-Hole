using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    [SerializeField]
    private ObjectPool ammoPool;

    [SerializeField]
    private Navigator navigator;

    private Vector2 holePosition;

    private float yPos;

    private float yOffset = 2;

    [SerializeField]
    private float spawnInterval = 0.5f;

    private float timeAfterSpawn = 0;

    [SerializeField]
    private int spawnLimit = 15;

    private int spawnedCurrent = 0;

    private Vector2 origin;

    [SerializeField]
    private float areaRadius;

    [SerializeField]
    private float holeRadius;

    [SerializeField]
    private float lineOffset;

    // Start is called before the first frame update
    void Start()
    {
       yPos = navigator.transform.position.y;
       origin = new Vector2(navigator.transform.position.x, navigator.transform.position.z); 
    }

    // Update is called once per frame
    void Update()
    {
        holePosition = new Vector2(navigator.transform.position.x, navigator.transform.position.z); 

        if (Mathf.Infinity > spawnedCurrent)
        {
            timeAfterSpawn += Time.deltaTime;

            if (timeAfterSpawn > spawnInterval)
            {
                Spawn();
                spawnedCurrent += 1;
                timeAfterSpawn = 0;
            }
        }

    }

    private void Spawn()
    {
        GameObject spawnObject = ammoPool.GetPooledObject();
        Vector3 spawnPos = GetRandomPos();

        spawnObject.transform.position = spawnPos;
        spawnObject.SetActive(true);

    }


    private Vector3 GetRandomPos()
    {
        Vector2 originToHole = holePosition - origin;
        float randomX = 0;
        float randomZ = 0;
        Vector3 randomPos = Vector3.zero;

        int randomInt = UnityEngine.Random.Range(0, 2);
        if (randomInt == 0)
        {
            randomX = UnityEngine.Random.Range(-areaRadius, originToHole.x - holeRadius);
        }
        else
        {
            randomX = UnityEngine.Random.Range(originToHole.x + holeRadius, areaRadius);
        }

        randomInt = UnityEngine.Random.Range(0, 2);
        if (randomInt == 0)
        {
            randomZ = UnityEngine.Random.Range(-areaRadius, originToHole.y - holeRadius);
        }
        else if (originToHole.y < lineOffset)
        {
            randomZ = UnityEngine.Random.Range(originToHole.y + holeRadius, areaRadius - lineOffset);
        }

        randomX = Mathf.Round(randomX) * 2;
        randomZ = Mathf.Round(randomZ) * 2;

        randomPos = new Vector3(origin.x + randomX, yPos+yOffset, origin.y + randomZ);
        return randomPos;

    }
}
