using UnityEngine;
using UnityEngine.SceneManagement;

public class game_over : MonoBehaviour
{
    public Transform target;
    public GameObject game_over_UI;
    public GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {


            game_over_UI.SetActive(true); // Activate the game over UI
            PlayerMovement player = target.GetComponent<PlayerMovement>();
            player.HealthBar.sethealth(0); // Set player health to 0
            player.Spirit_Bar.setspirit(0); // Set player spirit to 0
            player.animator.SetTrigger("Die");
            Player.SetActive(false);// Destroy the player GameObject
        }
    }
}
