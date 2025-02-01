using UnityEngine;

public class SeekBehavior : MonoBehaviour
{
    [SerializeField] float maxforce;//4
    [SerializeField] float maxspeed;//10


    [SerializeField] Rigidbody rigid_body;

   

    private void Start()
    {
        
        rigid_body = GetComponent<Rigidbody>();
    }

   
    public Vector3 Seek(Vector3 target)
    {
        Vector3 desired = target - rigid_body.transform.position;
        desired.Normalize();
        desired *= maxspeed;
        Vector3 steer = desired - rigid_body.linearVelocity;
        steer.x = Mathf.Clamp(steer.x, -maxforce, maxforce);
        steer.y = Mathf.Clamp(steer.y, -maxforce, maxforce);
        steer.z = Mathf.Clamp(steer.z, -maxforce, maxforce);

        return steer;

    }


   
}
