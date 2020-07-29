using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    GameIF gameInterface;
    public int triggerscore = 20;
    ProgressBar bar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Tresspassing" + collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<GameIF>().score += triggerscore;
          
            
            Debug.Log(collision.GetComponent<GameIF>().score);
            
        }
      
    }

}
