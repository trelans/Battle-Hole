using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ink : MonoBehaviour
{
    [SerializeField] private GameObject ink;
    [SerializeField] private GameObject blood;
    [SerializeField] private SkinnedMeshRenderer enemyMesh;
    private Material _material;
   [SerializeField] private InkColor inkColor;
    
    
    private float numberRandomX;
    private float numberRandomZ;

    private float numberRandomLocalScale;


    // Start is called before the first frame update
    void Start()
    {


        numberRandomZ = Random.Range(0, 360);
        numberRandomX = Random.Range(0, 360);
        numberRandomLocalScale = Random.Range(2, 5);
        numberRandomLocalScale /= 10;

        HandleColor();
       
    }

    private void HandleColor()
    {
        _material = enemyMesh.material;
        Color colorInk = _material.color;
        inkColor.SetColor(colorInk);

    }
    // Update is called once per frame
    void Update()
    {

    }

 
    public void Paint(Vector3 pos)
    {
        ink.SetActive(true);
        ink.transform.position = new Vector3(pos.x, 0.52f, pos.z);
        ink.transform.rotation = Quaternion.Euler(90, numberRandomX, numberRandomZ);
        ink.transform.localScale = new Vector3(numberRandomLocalScale, numberRandomLocalScale, numberRandomLocalScale);
    // GameObject a = Instantiate(ink, new Vector3(pos.x , 0.52f ,pos.z), Quaternion.Euler(90 , numberRandomX , numberRandomZ));
    
    // a.transform.localScale = new Vector3(numberRandomLocalScale, numberRandomLocalScale, numberRandomLocalScale);
    
    }


    public void ActivateBlood(Vector3 pos)
    {
        transform.position = pos - Vector3.forward * 2 + Vector3.up * 0.7f ;

        blood.SetActive(true);
    }
}