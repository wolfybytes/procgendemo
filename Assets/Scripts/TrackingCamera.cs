using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed = 8f;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, speed * Time.smoothDeltaTime);
    }
}
