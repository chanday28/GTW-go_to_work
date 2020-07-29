using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 camPos;
    public float speed;
    public float xMin = 0f;
    Vector3 velocity = Vector3.zero;


    private void FixedUpdate()
    {
        Vector3 playerPos = player.position + camPos;
        Vector3 clampedPos = new Vector3(Mathf.Clamp(playerPos.x, xMin, float.MaxValue), Mathf.Clamp(transform.position.y, -5f, 9f), playerPos.z);
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, clampedPos, ref velocity, speed * Time.deltaTime);

        transform.position = smoothPos;
    }


}
