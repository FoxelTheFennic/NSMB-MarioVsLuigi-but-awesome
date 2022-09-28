using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDaMissile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0f, 30f * Time.deltaTime, 0f);
    }
}
