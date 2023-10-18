using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Canvas Button;
    public Canvas Victory;
    public PlayerDector playerDector;
    public GameObject Enemies;

    public bool isEnd
    {
        get { return Enemies.transform.childCount <= 0; }
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDector.detectedCollisions.Count > 0 && isEnd)
        {
            Button.enabled = true;
        }
        else
        {
            Button.enabled = false;
        }
    }

    public void victory()
    {
        Victory.enabled = true;
        Time.timeScale = 0;
    }
}
