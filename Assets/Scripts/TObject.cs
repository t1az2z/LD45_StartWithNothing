using UnityEngine;

public class TObject: MonoBehaviour
{
    protected Rigidbody rb;
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
        if (collision.collider.CompareTag("PlayerColliders"))
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}