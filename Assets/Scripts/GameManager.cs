using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int explostionTime;

    public bool ticking = false;
    public bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Countdown ()
    {
        while (explostionTime > 0)
        {
            yield return new WaitForSeconds(1);
            explostionTime--;
        }
        exploded = true;
        ticking = false;
    }

    public void StartRoutine()
    {
        ticking = true;
        StartCoroutine(Countdown());
    }
}
