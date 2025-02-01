using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Jump : MonoBehaviour
{
    [SerializeField] PlayerLookAround playerLookAround;
    [SerializeField] Rigidbody rb;
    [SerializeField] float jumpForce;
    bool isReady;
    [SerializeField] float delay;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerLookAround = GetComponent<PlayerLookAround>();
    }

    private void Start()
    {
        rb.useGravity = false;
        isReady = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isReady)
        {
            isReady = false;
            playerLookAround.enabled = false;
            
      
            rb.AddForce(transform.forward*jumpForce);
            rb.useGravity = true;
            StartCoroutine(scene_loader());
        }
        
    }

    IEnumerator scene_loader()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Scene2");
    }

   

}
