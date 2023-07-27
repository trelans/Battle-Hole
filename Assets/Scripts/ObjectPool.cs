using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

[SerializeField] 
private List<GameObject> pooledObjects;
[SerializeField] 
private GameObject objectToPool;
[SerializeField] 
private int amountToPool = 200;

private int latestIndex = 0;

private bool isGenerated;
private PooledObjectsHandler _pooledObjectsHandler;

private void Awake()
{
   
        
    _pooledObjectsHandler = FindObjectOfType<PooledObjectsHandler>();
    pooledObjects = new List<GameObject>();
    GameObject tmp;
    for (int i = 0; i < amountToPool; i++)
    {
        tmp = Instantiate(objectToPool);
        tmp.SetActive(false);
        tmp.transform.parent = gameObject.transform;
        pooledObjects.Add(tmp);
    }
}


    public GameObject GetPooledObject()
    {
        int index = latestIndex;
        UpdateIndex();
        return pooledObjects[index];
    }

    private void UpdateIndex()
    {
        latestIndex = latestIndex + 1;
        latestIndex = latestIndex % amountToPool;
    }

    public void ChangeObject(GameObject newObject)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            pooledObjects[i].GetComponent<MeshFilter>().mesh = newObject.GetComponent<MeshFilter>().sharedMesh;
            pooledObjects[i].GetComponent<MeshRenderer>().material = newObject.GetComponent<MeshRenderer>().sharedMaterial;
        }
        
    }
}
