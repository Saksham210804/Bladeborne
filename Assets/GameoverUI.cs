using UnityEngine;

public class GameoverUI : MonoBehaviour
{
    public Transform target; // Reference to the player object
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void restart()
    {
        // Assuming you have a method to reset the game state
        // Reset player health and spirit
        PlayerMovement player = target.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.HealthBar.sethealth(100);
            player.Spirit_Bar.setspirit(40); // Reset player health to 100
        }
        
        // Load the main scene again
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void quittodesktop()
    {

       Debug.Log("Exiting Game...");
        Application.Quit();
    }
    public void exittomainmenu()
    {

       // Load the main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
