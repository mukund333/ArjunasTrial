using UnityEngine;

public class Fish : MonoBehaviour
{
    public int health = 1;

    Animator animator;

    [SerializeField] bool testAnimFish;
    [SerializeField] Rigidbody rb;

    private void Awake()
    {
        testAnimFish = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
      
        rb.useGravity = false;
    }
    private void Update()
    {
        if(testAnimFish)
        {
            testAnimFish=false;
            TakeDamage(5);

        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
          animator.SetBool("isDead",true); 
            rb.useGravity=true;
            Invoke(nameof(DisableFish), 3);
        }
    }

    private void DisableFish()
    {
        gameObject.SetActive(false);
    }
}
