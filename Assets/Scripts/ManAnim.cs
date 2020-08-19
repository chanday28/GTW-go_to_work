using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManAnim : MonoBehaviour
{
    private Animator man;
   
    void Start()
    {
        man = GetComponent<Animator>();
        man.SetBool("isHit", false);

    }

    public void FlyAwayMan()
    {        
        man.SetBool("isHit", true);
        Debug.Log("Team Rocket");
        StartCoroutine(SelfDestruction());
        
    }

    IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
