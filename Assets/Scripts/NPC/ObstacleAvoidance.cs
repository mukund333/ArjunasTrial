using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    public float radius = 0.5f;      // SphereCast radius
    public float viewRadius = 5f;    // How far ahead to check for obstacles
    public LayerMask obstacleMask;   // Layer of obstacles
    public float avoidanceStrength = 10f; // Strength of avoidance force
    [SerializeField] private bool[] hitResults; // Stores which directions hit obstacles
    private Rigidbody rb;

    private void Awake()
    {
        hitResults = new bool[SpherePoints.rayDirections.Length];
        rb = GetComponent<Rigidbody>(); // Ensure Rigidbody is used for movement
    }

    private void Update()
    {
        // Reset hitResults at the start of each update
        for (int i = 0; i < hitResults.Length; i++)
        {
            hitResults[i] = false;
        }

      
    }



    public Vector3 CalculateAvoidanceForce()
    {
        if (IsHeadingForCollision())
        {
            Vector3 newDir = FindUnobstructedDirection();
            Debug.DrawRay(transform.position, newDir,Color.blue);
            return newDir * avoidanceStrength;

        }
        return Vector3.zero;
    }



    // Check if the fish is heading into an obstacle
    public bool IsHeadingForCollision()
    {
        RaycastHit hit;
        bool isColliding = Physics.SphereCast(transform.position, radius, transform.forward, out hit, viewRadius, obstacleMask);

        if (isColliding)
        {
            Debug.Log($"Obstacle detected: {hit.collider.gameObject.name} at distance {hit.distance}");
        }

        return isColliding;
    }


    // Returns a new direction if an obstacle is detected
    public Vector3 FindUnobstructedDirection()
    {
        Vector3 bestDir = transform.forward;
        float furthestUnobstructedDst = 0;
        RaycastHit hit;
        for (int i = 0; i < SpherePoints.rayDirections.Length; i++)
        {
            Vector3 dir = transform.TransformDirection(SpherePoints.rayDirections[i]);
            bool hitObstacle = Physics.SphereCast(transform.position, radius, dir, out hit, viewRadius, obstacleMask);
            hitResults[i] = hitObstacle; // Store hit status for Gizmos
            if (!hitObstacle) return dir; // Return first clear direction
            if (hit.distance > furthestUnobstructedDst)
            {
                bestDir = dir;
                furthestUnobstructedDst = hit.distance;
            }
        }
        return bestDir; // Return the least obstructed direction
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying || SpherePoints.rayDirections == null) return;

        for (int i = 0; i < SpherePoints.rayDirections.Length; i++)
        {
            // Only draw if the direction hit an obstacle (which is now reset each frame)
            if (hitResults[i])
            {
                Vector3 worldDir = transform.TransformDirection(SpherePoints.rayDirections[i]);
                Vector3 endPoint = transform.position + worldDir * viewRadius;
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, endPoint);
                Gizmos.DrawWireSphere(endPoint, 0.05f);
            }
        }
    }
}