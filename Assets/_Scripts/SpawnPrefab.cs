using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    //random po mapi gde je layer what is ground ili bilo koj koj ti izaberes pravi zombije
    public GameObject prefab;
    public int[] layers;
    public float spawnInterval = 5f;
    private float spawnTimer = 0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            
            int randomLayer = layers[Random.Range(0, layers.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            GameObject spawnedPrefab = Instantiate(prefab, spawnPosition, Quaternion.identity);
            spawnedPrefab.layer = randomLayer;
            spawnTimer = 0f;
        }
    }
}
