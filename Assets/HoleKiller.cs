using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleKiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
        }
    }
}
