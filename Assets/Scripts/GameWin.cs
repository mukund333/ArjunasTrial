using UnityEngine;

public class GameWin : MonoBehaviour
{
      Fish fish;
    [SerializeField] GameObject gameWinPanel;
   
    private void Awake()
    {
        fish = GetComponent<Fish>();
        gameWinPanel = GameObject.Find("GameWinPanel");
    
    }

    private void Start()
    {
        gameWinPanel.SetActive(false);
    }

    private void Update()
    {
        if (fish.health <= 0)
        {
            gameWinPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
