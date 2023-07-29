using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void OnCollisionStay(Collision collisionInfo)
    {
        Debug.Log(    collisionInfo.gameObject.name);
    
    }
}
