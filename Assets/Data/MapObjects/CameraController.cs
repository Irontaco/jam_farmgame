using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject trackedObject = null;

    public float distanceFromTarget = 8f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trackedObject is null) { return; }
        var thisTransform = transform;
        thisTransform.position = trackedObject.transform.position - thisTransform.forward * distanceFromTarget;
    }
}
