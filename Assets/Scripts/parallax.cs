using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public GameObject BG;
    public float speed;

    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + 1f * Time.deltaTime * speed, transform.position.y);
    }

}
