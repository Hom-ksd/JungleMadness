using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDrop : MonoBehaviour
{
    public List<GameObject> listdropObjectPrefab;

    public float droprate = 0.25f;

    public GameObject parent;

    private void OnDestroy()
    {
        float randFloat = Random.value;
        int randInt = Random.Range(0, listdropObjectPrefab.Count);
        Debug.Log(randInt);
        Debug.Log(randFloat <= droprate);
        if (listdropObjectPrefab != null)
        {
            if(randFloat <= droprate)
            {
                GameObject healthPickup = Instantiate(listdropObjectPrefab[randInt],parent.transform);
                healthPickup.transform.position = gameObject.transform.position;
            }
        }
    }
}
