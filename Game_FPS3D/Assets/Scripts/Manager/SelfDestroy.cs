using System;
using System.Collections;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float timeDestruction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroySelf(timeDestruction));
    }

    private IEnumerator DestroySelf(float timeDestruction)
    {
        yield return new WaitForSeconds(timeDestruction);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
