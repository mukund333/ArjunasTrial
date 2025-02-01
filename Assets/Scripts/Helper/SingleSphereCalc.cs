using UnityEngine;

public class SingleSphereCalc: MonoBehaviour
{
    public int numPoints = 100;  // Total points on the sphere
    public float radius = 1f;    // Sphere radius
     Color gizmoColor = Color.green; // Gizmo color

    public Vector3[] points; 

    private void Start()
    {
        points = new Vector3[numPoints];
         
        
    }

    private void Update()
    {
        GetPointOnSphere();
    }



    void GetPointOnSphere()
    {
        for (int i = 0; i < numPoints; i++)
        {
            float t = (float)i / (numPoints - 1);
             float inclination = Mathf.Acos(1 - 2 * t);
            float goldenRatio = (Mathf.Sqrt(5f) - 1f) / 2f;
            float azimuth = 2 * Mathf.PI * goldenRatio * i;

             float x = radius * Mathf.Sin(inclination) * Mathf.Cos(azimuth);
                float y = radius * Mathf.Sin(inclination) * Mathf.Sin(azimuth);
             float z = radius * Mathf.Cos(inclination);

            // Convert to world space
            points[i] = transform.position + new Vector3(x, y, z);
        }
      
    }
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (points == null) return;

        Gizmos.color = gizmoColor;

        foreach (Vector3 point in points)
        {
            Gizmos.DrawSphere(point, 0.05f); // Small spheres for points
        }
    }
}
