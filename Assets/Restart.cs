using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public GameObject game_pause_UI;
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void restart()
    {
        SceneManager.LoadScene("SampleScene");
        PlayerMovement player = target.GetComponent<PlayerMovement>();
        if(player != null)
        {
            player.HealthBar.sethealth(100);
            player.Spirit_Bar.setspirit(40);// Reset player health to 100
        }

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {

           game_pause(); // Call the game pause method when Escape is pressed
        }   
    }
    public void game_pause()
    {
         // Check if the Escape key is pressed
          game_pause_UI.SetActive(true); // Activate the game pause UI
          
    }
}
