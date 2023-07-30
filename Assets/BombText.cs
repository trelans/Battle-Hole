using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombText : MonoBehaviour
{
    private TextMeshPro tmp;

    private void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
        
    }
}
