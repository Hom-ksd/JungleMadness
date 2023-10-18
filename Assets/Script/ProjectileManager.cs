using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parent;
    public GameObject FireBoltPrefab;

    Vector2 startposition;

    private void Awake()
    {   

    }

    // Update is called once per frame
    void Update()
    {   
        startposition = transform.position;
    }

    public void Shotmagic(bool direction)
    {
        float FacingDirection = direction ? 1f : -1f;
        GameObject childObject = Instantiate(FireBoltPrefab, parent.transform);
        childObject.transform.position = startposition;
        Vector3 scale = childObject.transform.localScale;
        scale.x = FacingDirection * scale.x;
        childObject.transform.localScale = scale;
    }
}
