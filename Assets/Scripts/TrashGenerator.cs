using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGenerator : MonoBehaviour
{
    public float leftBorder = -2.8f;
    public float rightBorder = 2.8f;
    public int startBombRate;
    public float bombChanceIncreaseRatePerSecond;
    public GameObject[] trash;
    public GameObject[] bombs;

    public float timeBetweenGenerations = 3f;
    public float timeBetweenGenerationsMin = 1.3f;
    public float timeBetweenGenerationsRate = 500f;
    public void CreateItem(float timeSinceStart)
    {
        float bombChance = startBombRate + timeSinceStart * bombChanceIncreaseRatePerSecond;
        Vector3 pos = transform.position.Where(x: Random.Range(leftBorder, rightBorder));
        float rnd = Random.Range(0, 100);
        var obj = Instantiate(rnd>bombChance? trash[Random.Range(0, trash.Length - 1)]: bombs[Random.Range(0, bombs.Length - 1)], transform);
        obj.transform.position = pos;
        obj.transform.rotation = Random.rotation;
    }
}
