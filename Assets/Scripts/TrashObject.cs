using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObject : TObject
{
    public bool pointsRemoved = false;
    public bool pointsAdded = false;
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
        if (collider.CompareTag("Player") && !pointsAdded)
        {
            GameController.Instance.UpdatePoints(points);
            pointsAdded = true;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            collided = false;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            GameController.Instance.UpdatePoints(Mathf.CeilToInt(-points));
            pointsRemoved = true;
            pointsAdded = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PlayerColliders"))
        {
            rb.constraints = RigidbodyConstraints.None;
        }

        if (collision.collider.CompareTag("Ground") && !pointsRemoved)
        {
            rb.constraints = RigidbodyConstraints.None;
            GameController.Instance.UpdatePoints(Mathf.CeilToInt(-points));
            pointsRemoved = true;
            pointsAdded = false;
        }

        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Ground"))
            collided = true;
    }
}
