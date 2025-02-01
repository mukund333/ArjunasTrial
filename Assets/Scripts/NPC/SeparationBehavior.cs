using UnityEngine;
using System.Collections.Generic;

public class SeparationBehavior : MonoBehaviour
{
    [SerializeField] float neighborRadius = 3f;  // Radius to detect nearby fish
    [SerializeField] float maxforce = 10f;       // Strength of separation force
    [SerializeField] Rigidbody rigid_body;

    private void Start()
    {
        rigid_body = GetComponent<Rigidbody>();
    }

    public Vector3 CalculateSeparation()
    {
        Vector3 separationForce = Vector3.zero;
        int neighborCount = 0;

        Collider[] neighbors = Physics.OverlapSphere(transform.position, neighborRadius);

        foreach (Collider neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject) // Ignore self
            {
                Rigidbody neighborRb = neighbor.GetComponent<Rigidbody>();
                if (neighborRb != null)
                {
                    // Get vector pointing away from neighbor
                    Vector3 away = transform.position - neighbor.transform.position;
                    float distance = away.magnitude;

                    if (distance > 0)
                    {
                        // Inverse weighting: closer neighbors have stronger repulsion
                        float forceStrength = Mathf.Clamp(1 / distance, 0, maxforce); // Avoid infinity at very close distances
                        separationForce += away.normalized * forceStrength;
                    }
                    neighborCount++;
                }
            }
        }

        if (neighborCount > 0)
        {
            separationForce /= neighborCount; // Average force
            separationForce = Vector3.ClampMagnitude(separationForce, maxforce);
        }

        return separationForce;
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, neighborRadius); // Shows separation detection range

    //    Vector3 separation = CalculateSeparation();
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(transform.position, transform.position + separation * 2f); // Draw separation force
    //}

}
