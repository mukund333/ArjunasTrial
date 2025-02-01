using System.Collections.Generic;
using UnityEngine;

public class FireArrowPool : MonoBehaviour
{
    public static FireArrowPool Instance;

    [SerializeField] private GameObject fireArrowPrefab;
    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> arrowPool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Initialize the pool with pre-instantiated arrows
        for (int i = 0; i < poolSize; i++)
        {
            GameObject arrow = Instantiate(fireArrowPrefab);
            arrow.SetActive(false);
            arrowPool.Enqueue(arrow);
        }
    }

    // Get an arrow from the pool
    public GameObject GetArrow()
    {
        GameObject arrow;
        if (arrowPool.Count > 0)
        {
            arrow = arrowPool.Dequeue();
        }
        else
        {
            // Expand the pool if needed
            arrow = Instantiate(fireArrowPrefab);
        }

        // Reset arrow before use
        arrow.SetActive(true);
        arrow.transform.position = Vector3.zero; // Reset position
        arrow.transform.rotation = Quaternion.identity; // Reset rotation

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero; // Reset velocity
            rb.angularVelocity = Vector3.zero; // Reset rotation force
        }

        return arrow;
    }

    // Return the arrow to the pool
    public void ReturnArrow(GameObject arrow)
    {
        arrow.SetActive(false);
        arrowPool.Enqueue(arrow);
    }
}
