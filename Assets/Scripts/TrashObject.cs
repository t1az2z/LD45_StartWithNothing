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
    }
}
