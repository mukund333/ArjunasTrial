using UnityEngine;

public class ManageSteeringForce : MonoBehaviour
{
    [SerializeField] GameObject target;

    [SerializeField] Rigidbody rigid_body;
    [SerializeField] private SeekBehavior seekBehavior;
    [SerializeField] private WanderBehavior wanderBehavior;
    [SerializeField] private AlignmentBehavior alignmentBehavior;
    [SerializeField] private SeparationBehavior separationBehavior;
    [SerializeField] private CohesionBehavior cohesionBehavior;
    [SerializeField] private ObstacleAvoidance obstacleAvoidance; // New component
    [SerializeField] private StayWithinWalls stayWithinWalls;



    [SerializeField] float separationWeight;
    [SerializeField] float alignmentWeight;
    [SerializeField] float cohesionWeight;
    [SerializeField] float wanderWeight;
    [SerializeField] float seekWeight;
    [SerializeField] float obstacleAvoidanceWeight; // New weight


    


    private void Awake()
    {

        if (target != null)
        {
            target = GameObject.Find("Target");
        }
        else
        {
            Debug.LogWarning("Target missing! Continuing with other behaviors.");
        }

       
        rigid_body = GetComponent<Rigidbody>();
        seekBehavior = GetComponent<SeekBehavior>();
        if (seekBehavior == null) Debug.LogError("SeekBehavior not found");
        wanderBehavior = GetComponent<WanderBehavior>();
        alignmentBehavior = GetComponent<AlignmentBehavior>();
        separationBehavior = GetComponent<SeparationBehavior>();
        cohesionBehavior = GetComponent<CohesionBehavior>();
        obstacleAvoidance = GetComponent<ObstacleAvoidance>(); // Add this line

    }

    private void FixedUpdate()
    {
        Vector3 seekForce = Vector3.zero;
        // Get each behavior's force
        // Check if the target exists before seeking
        if (target != null)
        {
             seekForce = seekBehavior != null ? seekBehavior.Seek(target.transform.position) : Vector3.zero;
        }
     
        Vector3 wanderForce = wanderBehavior.CalculateWanderTarget();
        Vector3 alignmentForce = alignmentBehavior.CalculateAlignment();
        Vector3 separationForce = separationBehavior.CalculateSeparation();
        Vector3 cohesionForce = cohesionBehavior.CalculateCohesion();
        Vector3 obstacleAvoidanceForce = obstacleAvoidance.CalculateAvoidanceForce();

     




        // Combine forces (adjust weight as needed)
        Vector3 totalForce =  seekForce *seekWeight + wanderForce *wanderWeight + alignmentForce * alignmentWeight + 
                                        separationForce * separationWeight+ cohesionForce * cohesionWeight + 
                                        obstacleAvoidanceForce * obstacleAvoidanceWeight; // Add obstacle avoidance


        // Apply the total steering force
        rigid_body.AddForce(totalForce, ForceMode.Acceleration);

        // Ensure the fish always faces its velocity direction
        RotateTowardsVelocity();
    }

    private void RotateTowardsVelocity()
    {
        if (rigid_body.linearVelocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rigid_body.linearVelocity.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
        }
    }
}
