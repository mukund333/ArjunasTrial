using UnityEngine;

public class RandomTargetMover : MonoBehaviour
{
    public float xMin = -50f, xMax = 50f;
    public float yMin = 10f, yMax = 40f;
    public float zMin = -50f, zMax = 50f;
    public float moveInterval = 4f; // Time between moves

    private void Start()
    {
        InvokeRepeating(nameof(MoveToRandomPosition), 0f, moveInterval);
    }

    private void MoveToRandomPosition()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(xMin, xMax),
            Random.Range(yMin, yMax),
            Random.Range(zMin, zMax)
        );

        transform.position = randomPosition;
    }
}
