using UnityEngine;

public static class SpherePoints
{
    public static Vector3[] rayDirections;

    static SpherePoints()
    {
        int numPoints = 300; // More points = smoother avoidance
        rayDirections = new Vector3[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            float t = (float)i / (numPoints - 1);
            float inclination = Mathf.Acos(1 - 2 * t);
            float goldenRatio = (Mathf.Sqrt(5f) - 1f) / 2f;
            float azimuth = 2 * Mathf.PI * goldenRatio * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);

            rayDirections[i] = new Vector3(x, y, z);
        }
    }
}
