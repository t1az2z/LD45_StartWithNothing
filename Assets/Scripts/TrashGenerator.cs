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
        var obj = Instantiate(rnd>bombRate? trash[Random.Range(0, trash.Length - 1)]: bombs[Random.Range(0, bombs.Length - 1)], transform);
        obj.transform.position = pos;
        obj.transform.rotation = Random.rotation;
    }

    public void Start()
    {
        InvokeRepeating("CreateItem", 3, 2);
    }
}
