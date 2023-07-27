using System;
using UnityEngine;
using UnityEngine.UI;

public class destroyCubes : MonoBehaviour
{

    private float value;
    private void Start()
    {
        value = 100f / GameObject.FindGameObjectsWithTag("block").Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("block"))
        {
            other.gameObject.SetActive(false);


        }
    }
}
