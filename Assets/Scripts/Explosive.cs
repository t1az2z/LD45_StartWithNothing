using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : TObject
{
    public float timeBeforeExplosion = 6f;
    public float explosionForce = 15000f;
    public GameObject explosionEffect;
    private bool exploded;
    public float explosionRaidus = 3;
    private void Update()
    {
        timeBeforeExplosion -= Time.deltaTime;
        if (timeBeforeExplosion <= 0)
            Explode();
    }
    private void Explode()
    {
        if (!exploded)
        {
            exploded = true;
            var explosion = Instantiate(explosionEffect, transform.parent);
            explosion.transform.position = transform.position;
            Collider[] overlaps = Physics.OverlapSphere(transform.position, explosionRaidus);
            for (int i = 0; i < overlaps.Length; i++)
            {
                if (overlaps[i].gameObject.tag == "Trash")
                {
                    var oRb = overlaps[i].GetComponent<Rigidbody>();
                    print(oRb.gameObject.name);
                    if (oRb)
                        oRb.AddExplosionForce(explosionForce, transform.position.Where(y: transform.position.y-2), explosionRaidus);
                }
            }
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            transform.GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
            Destroy(gameObject, 3);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Trash"))
        {
            if (increaseMassInTrashcan)
            {
                rb.mass = 5;
            }
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, explosionRaidus);
    }
}
