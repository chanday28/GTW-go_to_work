using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstacles;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform _player;
    Vector2 spawnPosition;
    float randomXvalue, playerX;

    void Start()
    {
        StartCoroutine(WaitAndSpawn());
        
    }
    private void Update()
    {
        StartCoroutine(SelfDestruction());
        //StartCoroutine(WaitAndSpawn());
        //StartCoroutine(JustWait());
    }

    private void FixedUpdate()
    {
        
        //StartCoroutine(SelfDestruction());
        randomXvalue = Random.Range(5f, 100f);
        playerX = _player.position.x;
    }

    public void Spawning()
    {
        Debug.Log("Spawning!");
        //float randomXvalue = Random.Range(-5f, 300f);
        //Vector2 spawnPosition = new Vector2(randomXvalue, -6.5f);
        Vector2 spawnPosition = new Vector2( playerX + randomXvalue, -5f);
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
        yield return new WaitForSeconds(.5f);
        Spawning();
        StartCoroutine(WaitAndSpawn());
    }

 
    IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
