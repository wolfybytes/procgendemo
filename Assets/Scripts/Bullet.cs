using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 targetRotation;

    public float spinSpeed = 8f;

    private void Start()
    {
        targetRotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        targetRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(targetRotation.x, targetRotation.y + spinSpeed, targetRotation.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
