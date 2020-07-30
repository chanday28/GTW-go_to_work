using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    //public GameObject BG;
    //public float speed;

    //private void FixedUpdate()
    //{
    //    transform.position = new Vector2(transform.position.x + 1f * Time.deltaTime * speed, transform.position.y);
    //}
    private float length, startPos;
    [SerializeField]
    private GameObject cam;
    public float parallaxFX;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxFX));
        float distance = (cam.transform.position.x * parallaxFX);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
