using UnityEngine;

public class TObject: MonoBehaviour
{
    protected Rigidbody rb;
    public float maxFallSpeed;
    public bool increaseMassInTrashcan;
    public int points;
    public bool collided;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y > maxFallSpeed)
            rb.velocity = rb.velocity.Where(y: maxFallSpeed);
    }
}