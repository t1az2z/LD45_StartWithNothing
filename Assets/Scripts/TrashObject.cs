using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObject : TObject
{
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("Trash"))
        {
            if (increaseMassInTrashcan)
            {
                rb.mass = 5;
            }
            rb.constraints = RigidbodyConstraints.None;
        }

        if (collider.CompareTag("Player") || collider.CompareTag("Ground"))
            collided = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PlayerColliders"))
        {
            rb.constraints = RigidbodyConstraints.None;
        }

        if (collision.collider.CompareTag("Ground"))
        {
            rb.constraints = RigidbodyConstraints.None;
            GameController.Instance.UpdatePoints(-points);
        }

        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Ground"))
            collided = true;
    }
}
