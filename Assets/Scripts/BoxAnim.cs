using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAnim : MonoBehaviour
{
    private Animator box;
    private PlayerController Player;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<Animator>();
        box.SetBool("isDust", false);
        box.SetBool("isKnocked", false);
    }


    IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }

    public void Dust()
    {
        box.SetBool("isDust", true);
        box.SetBool("isKnocked", false);
        Debug.Log("Dust!");
        StartCoroutine(SelfDestruction());
    }

    public void FlyAwayBoxes()
    {        
        box.SetBool("isKnocked", true);
        box.SetBool("isDust", false);
        Debug.Log("Fly me to the moon!");
        StartCoroutine(SelfDestruction());               
            
    }

}
