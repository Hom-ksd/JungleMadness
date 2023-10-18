using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    Vector2 startingPostition;

    float startingZ;

    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPostition;

    float distanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (distanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxfactor => Mathf.Abs(distanceFromTarget) / clippingPlane; 

    // Start is called before the first frame update
    void Start()
    {
        startingPostition = transform.position;
        startingZ = transform.position.z;    
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPostition + camMoveSinceStart * parallaxfactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
