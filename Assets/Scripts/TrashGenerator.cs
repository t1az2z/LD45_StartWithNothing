using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGenerator : MonoBehaviour
{
    public float leftBorder = -2.8f;
    public float rightBorder = 2.8f;
    public int bombRate;
    public GameObject[] trash;
    public GameObject[] bombs;
    public void CreateItem()
    {
        Vector3 pos = transform.position.Where(x: Random.Range(leftBorder, rightBorder));
        int rnd = Random.Range(0, 100);
        if (rnd > bombRate)
        {
            var obj = Instantiate(trash[Random.Range(0, trash.Length - 1)]);
        }
        else
        {
            var obj = Instantiate(bombs[Random.Range(0, bombs.Length - 1)]);
        }
    }

    public void Start()
    {
        InvokeRepeating("CreateItem", 3, 2);
    }
}
