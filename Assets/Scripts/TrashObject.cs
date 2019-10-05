using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObject : MonoBehaviour
{
    private Rigidbody rb;
    public float maxFallSpeed;
    public bool increaseMassInTrashcan;
    public int points;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y > maxFallSpeed)
            rb.velocity = rb.velocity.Where(y: maxFallSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (increaseMassInTrashcan)
            {
                rb.mass = 100;
            }
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}
