using UnityEngine;
using System.Collections.Generic;

public class CohesionBehavior : MonoBehaviour
{
    [SerializeField] float neighborRadius = 5f;  // Radius to detect nearby fish
    [SerializeField] float maxforce = 5f;        // Strength of cohesion force
    [SerializeField] Rigidbody rigid_body;

    private void Start()
    {
        rigid_body = GetComponent<Rigidbody>();
    }

    public Vector3 CalculateCohesion()
    {
        Vector3 centerOfMass = Vector3.zero;
        int neighborCount = 0;

        Collider[] neighbors = Physics.OverlapSphere(transform.position, neighborRadius);

        foreach (Collider neighbor in neighbors)
        {
            if (neighbor.gameObject != gameObject) // Ignore self
            {
                centerOfMass += neighbor.transform.position;
                neighborCount++;
            }
        }

        if (neighborCount > 0)
        {
            centerOfMass /= neighborCount; // Find the average position

            // Create a seek force to move towards the center
            Vector3 cohesionForce = centerOfMass - transform.position;
            cohesionForce = cohesionForce.normalized * maxforce;

            return cohesionForce;
        }

        return Vector3.zero;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, neighborRadius); // Show cohesion detection range
    //}
}
