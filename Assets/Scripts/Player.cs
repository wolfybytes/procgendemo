using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private float timer = 0f;
    private float timeToMatchMovementDirection = .5f;
    private Quaternion lookDirection;
    private Vector3 lastShot;

    [Header("Movement")]
    private CharacterController character;
    public Vector3 moveDirection;
    public float speed = 3f;

    [Header("Bullet")]
    public Vector3 shotDirection;
    public List<GameObject> bullets;
    public int bulletIndex = 0;
    private GameObject currentBullet;
    public Transform bulletSource;
    public float bulletSpeed = 20f;
    public float frequency = .5f;
    private float bulletTimer;
    private bool bulletIsQueued;

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

        // Update bullet shots
        bulletTimer -= Time.deltaTime;
        if (shotDirection.x != 0 || shotDirection.z != 0)
        {
            if (bulletTimer < 0)
                QueueShot(shotDirection);
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

        if (bulletIsQueued)
            Shoot(shotDirection);
    }

    private bool UpdateDirection()
    {
        if (timer < 0)
        {
            return true;
        }
        return false;
    }

    private void QueueShot(Vector3 shotDirection)
    {
        lastShot = shotDirection;
        timer = timeToMatchMovementDirection;
        bulletIsQueued = true;
    }

    private void Shoot(Vector3 shotDirection)
    {
        bulletTimer = frequency;
        currentBullet = bullets[bulletIndex];
        currentBullet.SetActive(true);
        currentBullet.transform.SetPositionAndRotation(bulletSource.position, new Quaternion(0f, 0f, 0f, 0f));
        currentBullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        currentBullet.GetComponent<Rigidbody>().AddForce(lastShot * bulletSpeed, ForceMode.Impulse);
        bulletIsQueued = false;
        bulletIndex = (bulletIndex + 1 >= bullets.Count) ? 0 : bulletIndex + 1;
    }
}
