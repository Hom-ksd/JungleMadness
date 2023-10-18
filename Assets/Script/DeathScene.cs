using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScene : MonoBehaviour
{
    public Damageable damageable;
    Canvas canvas;
    // Start is called before the first frame update
    void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!damageable.isAlive) {
            canvas.enabled = true;
            Time.timeScale = 0f;
        }
        else
        {
            canvas.enabled = false;
        }
    }
}
