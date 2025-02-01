// WanderBehavior.cs
using UnityEngine;

public class WanderBehavior : MonoBehaviour
{
    [SerializeField] float wanderDistance = 10f;
    [SerializeField] float wanderRadius = 5f;
    [SerializeField] float wanderJitter = 1f;

    private float wanderOrientation;

    private void Start()
    {
        wanderOrientation = Random.Range(0f, Mathf.PI * 2); // Random start angle
    }

    public Vector3 CalculateWanderTarget()
    {
        // Get circle center in front of the agent
        Vector3 circleCenter = transform.position + transform.forward * wanderDistance;

        // Apply random jitter
        wanderOrientation += Random.Range(-wanderJitter, wanderJitter);


        /*
         * polar cooridnate formula convert into cartian cooridantes.
         * (r,theta) into (x,y)
         */

        // Calculate displacement on the circle///  
        Vector3 displacement = new Vector3(
            wanderRadius * Mathf.Cos(wanderOrientation),
            0, // Keeps movement on XZ plane
            wanderRadius * Mathf.Sin(wanderOrientation)
        );

        // Final wander target position
        return circleCenter + displacement;
    }

    //private void OnDrawGizmos()
    //{
    //    if (!Application.isPlaying) return;

    //    // Circle center
    //    Vector3 circleCenter = transform.position + transform.forward * wanderDistance;
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(circleCenter, wanderRadius);

    //    // Wander target
    //    Vector3 targetWorld = CalculateWanderTarget();
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(targetWorld, 0.3f);
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(transform.position, targetWorld);
    //}
}
