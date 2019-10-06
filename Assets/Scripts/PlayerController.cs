using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody rb;
    private float xInput;
    public bool controllsEnabled = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
#if UNITY_ANDROID
        xInput = Input.gyro.rotationRate.magnitude*70f;
#else

        xInput = Input.GetAxisRaw("Horizontal");
#endif
    }

    private void FixedUpdate()
    {
        if (controllsEnabled)
        {
            if (xInput != 0)
            {
                rb.velocity = rb.velocity.Where(x: xInput * moveSpeed * Time.fixedDeltaTime);
            }
            else
                rb.velocity = Vector3.zero;
        }
    }
}