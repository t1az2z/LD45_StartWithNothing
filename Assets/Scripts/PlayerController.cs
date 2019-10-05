using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody rb;
    private float xInput;
    public bool controllsEnabled = true;

    public int points = 0;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            points += other.GetComponent<TObject>().points;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            points -= other.GetComponent<TObject>().points;
        }
    }

}