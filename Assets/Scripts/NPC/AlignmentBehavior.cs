using UnityEngine;
using System.Collections.Generic;

public class AlignmentBehavior : MonoBehaviour
{
    [SerializeField] float neighborRadius = 5f;  // Radius to detect nearby fish
    [SerializeField] float maxforce = 5f;        // Maximum steering force
    [SerializeField] Rigidbody rigid_body;

    private void Start()
    {
        rigid_body = GetComponent<Rigidbody>();
    }

    public Vector3 CalculateAlignment()
    {
        Vector3 averageVelocity = Vector3.zero;
        int neighborCount = 0;

        // Find all nearby fish
        Collider[] neighbors = Physics.OverlapSphere(transform.position, neighborRadius);

        foreach (Collider neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject) // Ignore self
            {
                Rigidbody neighborRb = neighbor.GetComponent<Rigidbody>();
                if (neighborRb != null)
                {
                    averageVelocity += neighborRb.linearVelocity;
                    neighborCount++;
                }
            }
        }

        if (neighborCount > 0)
        {
            averageVelocity /= neighborCount; // Compute the average velocity

            // Convert to steering force
            Vector3 steer = averageVelocity - rigid_body.linearVelocity;
            steer = Vector3.ClampMagnitude(steer, maxforce); // Limit steering force

            return steer;
        }

        return Vector3.zero; // No neighbors, no steering force
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, neighborRadius); // Show neighbor detection range
    //}
}
