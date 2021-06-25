using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float timer = 0f;
    private float timeToMatchMovementDirection = .5f;
    private Quaternion lookDirection;
    private Vector3 lastShot;

    [Header("Movement")]
    private CharacterController character;
    public Vector3 moveDirection;
    public float speed = 3f;
    public GameObject target;

    private void Start()
    {
        character = GetComponent<CharacterController>();
        moveDirection = new Vector3();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (!isTargetReady())
            return;

        // Update move direction
        moveDirection = target.transform.position - transform.position;
        character.SimpleMove(moveDirection * speed);
        lookDirection = Quaternion.LookRotation(moveDirection);
        transform.rotation = lookDirection;
    }

    private bool isTargetReady()
    {
        if (target != null)
            return true;

        target = GameObject.FindGameObjectWithTag("Player");
        return false;
    }
}
