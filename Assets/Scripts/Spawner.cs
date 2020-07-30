using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstacles;

    void Start()
    {
        StartCoroutine(WaitAndSpawn());
    }

    private void Spawning()
    {
        Debug.Log("Spawning!");
        float randomXvalue = Random.Range(-5f, 300f);
        Vector2 spawnPosition = new Vector2(randomXvalue, -7.5f);
        Instantiate(RandomSpawn(), spawnPosition, Quaternion.identity);
    }

    private GameObject RandomSpawn()
    {
        int randomNumber = Random.Range(0, obstacles.Length);
        GameObject randomizedObstacle = obstacles[randomNumber];
        return randomizedObstacle;
    }

    IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(1f);
        Spawning();
        StartCoroutine(WaitAndSpawn());
    }
}
