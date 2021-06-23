using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private float timer = 0f;
    private float timeToMatchMovementDirection = 2f;
    private Quaternion lookDirection;
    private Vector3 lastShot;

    private CharacterController character;
    public Vector3 moveDirection;
    public Vector3 shotDirection;
    public float speed = 3f;

    private void Start()
    {
        character = GetComponent<CharacterController>();
        moveDirection = new Vector3();
        shotDirection = new Vector3();
    }

    private void Update()
    {
        moveDirection.x = (Input.GetKey(KeyCode.A)) ? -1 : (Input.GetKey(KeyCode.D)) ? 1 : 0;
        moveDirection.z = (Input.GetKey(KeyCode.S)) ? -1 : (Input.GetKey(KeyCode.W)) ? 1 : 0;
        
        shotDirection.x = (Input.GetKey(KeyCode.LeftArrow)) ? -1 : (Input.GetKey(KeyCode.RightArrow)) ? 1 : 0;
        shotDirection.z = (Input.GetKey(KeyCode.DownArrow)) ? -1 : (Input.GetKey(KeyCode.UpArrow)) ? 1 : 0;
        
        // Update move direction
        character.SimpleMove(moveDirection * speed);
        // Update look direction
        timer -= Time.deltaTime;
        if (shotDirection.x != 0 || shotDirection.z != 0) 
        {
            Shoot(shotDirection);
        }

        if (UpdateDirection())
        {
            if (moveDirection.x != 0 || moveDirection.z != 0)
                lookDirection = Quaternion.LookRotation(moveDirection);
        }
        else
        {
            lookDirection = Quaternion.LookRotation(lastShot);
        }
        transform.rotation = lookDirection;
    }

    private bool UpdateDirection()
    {
        if (timer < 0)
        {
            return true;
        }
        return false;
    }

    private void Shoot(Vector3 shotDirection)
    {
        lastShot = shotDirection;
        timer = timeToMatchMovementDirection;
    }
}
