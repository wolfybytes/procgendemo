using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBob : MonoBehaviour
{
    public float walkingBobbingSpeed = 14f;
    public float bobbingAmount = 0.05f;
    public float timeOffset = 0f;
    public CharacterController controller;

    protected float startYPos = 0f;
    protected float timer = 0f;

    private void Start()
    {
        startYPos = transform.localPosition.y;
    }

    private void Update()
    {
        if (Mathf.Abs(controller.velocity.x) > 0.1f || Mathf.Abs(controller.velocity.z) > 0.1f)
        {
            // Player is moving
            timer += Time.deltaTime * walkingBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, startYPos + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        }
        else
        {
            // Player is idle
            timer = 0 + timeOffset;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, startYPos, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
        }
    }
}
