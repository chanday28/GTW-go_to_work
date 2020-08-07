using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstacles;
    [SerializeField] private GameObject player;

    void Start()
    {
        StartCoroutine(WaitAndSpawn());
        StartCoroutine(SelfDestruction());
    }

    private void FixedUpdate()
    {
        //StartCoroutine(WaitAndSpawn());
        //StartCoroutine(SelfDestruction());
    }

    private void Spawning()
    {
        Debug.Log("Spawning!");
        float randomXvalue = Random.Range(-5f, 300f);
        Vector2 spawnPosition = new Vector2(randomXvalue, -7.5f);
        Instantiate(RandomSpawn(), spawnPosition, Quaternion.identity);




        //Instantiate(RandomSpawn(), spawnPosition, Quaternion.identity);


        //float randomX = Random.Range(2f, 10f);
        //Vector2 spawnInFront = Vector2
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

    IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
