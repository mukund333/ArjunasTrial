using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] FireArrow[] fireArrows;
    [SerializeField] int NextArrowNumber = 0;
    [SerializeField] int count = 3;
    [SerializeField] float fireDelay = 0.5f; // Delay between shots
    private bool canShoot = true;

    [SerializeField] AudioSource fireSound; // Reference to AudioSource
    [SerializeField] TMPro.TMP_Text arrowCountText;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Fish goldFish;

    private void Awake()
    {
        gameOverPanel = GameObject.Find("GameOverPanel");
       
        goldFish = GameObject.Find("GoldenTunaFish").GetComponent<Fish>();
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        fireArrows = new FireArrow[3];

        for (int i = 1; i <= fireArrows.Length; i++)
        {
            fireArrows[i - 1] = transform.GetChild(i).GetComponent<FireArrow>();
            fireArrows[i - 1].gameObject.SetActive(false);
        }
        fireArrows[0].gameObject.SetActive(true);
        fireSound = GetComponent<AudioSource>();

        arrowCountText = GameObject.Find("ArrowCountText").GetComponent<TMP_Text>();


    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && NextArrowNumber < 3 && canShoot)
        {
            // Play Fire Sound
            if (fireSound != null)
                fireSound.Play();
            StartCoroutine(ShootWithDelay(fireArrows[NextArrowNumber]));
        }

        GameLose();
    }



    IEnumerator ShootWithDelay(FireArrow fireArrow)
    {
        canShoot = false;

     

        fireArrow.transform.parent = null;
        fireArrow.Shoot();
        NextArrowNumber++;
        count--;
        arrowCountText.text = count.ToString();

        if (NextArrowNumber < 3)
        {
            yield return new WaitForSeconds(fireDelay);
            fireArrows[NextArrowNumber].gameObject.SetActive(true);
        }

        canShoot = true;
    }


    void GameLose()
    {
        if(goldFish.health>0 && count ==0)
        {
            gameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
