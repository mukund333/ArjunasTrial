using UnityEngine;

public class StayWithinWalls : MonoBehaviour
{
    public float boundaryDistance = 2f; // How close before steering away
    public float avoidanceStrength = 10f; // Strength of steering force
    public LayerMask wallLayer; // Walls layer

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public Vector3 CalculateWallAvoidance()
    {
        Vector3 avoidanceForce = Vector3.zero;
        Vector3[] directions = { transform.forward, -transform.forward, transform.right, -transform.right, transform.up, -transform.up };

        foreach (Vector3 dir in directions)
        {
            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, boundaryDistance, wallLayer))
            {
                Debug.DrawRay(transform.position, dir * boundaryDistance, Color.red);

                float distanceFactor = 1f - (hit.distance / boundaryDistance); // Closer = stronger force
                Vector3 pushDirection = hit.normal;
                avoidanceForce += pushDirection * avoidanceStrength * distanceFactor;
            }
        }

        return avoidanceForce;
    }

}
