using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{   
    private float secondsToDestroy = 20f;   
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf() 
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Destroy(gameObject);
    }
}
