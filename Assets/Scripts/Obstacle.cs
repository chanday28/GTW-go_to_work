using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    void Start()
    {
        //dust =  GetComponent<ParticleSystem>();
        
       //StartCoroutine(SelfDestruction());
    }

   

    IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }
}
