using UnityEngine;

public class FireArrow : MonoBehaviour
{
    private Rigidbody rb;
    private float lifetime = 3f;
    public float arrowForce = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot()
    {
        rb.AddForce ( transform.right * arrowForce,ForceMode.Force);
        Invoke(nameof(DisableArrow), lifetime);
    }

    private void DisableArrow()
    {
        gameObject.SetActive (false);
    }


    private void OnTriggerEnter(Collider other) // Use Trigger detection
    {
        Fish fish = other.GetComponent<Fish>();
        if (fish != null) {
            fish.TakeDamage(1);
        }

        DisableArrow(); // Arrow disappears on hit
    }
}
