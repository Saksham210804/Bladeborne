using Unity.VisualScripting;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public Transform target;
    public GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Pause the audio listener
        if (target == null)
        {
            var go = GameObject.FindWithTag("Player");
            if (go != null)
            {
                target = go.transform;
            }
            else
            {
                Debug.LogError("Player GameObject with tag 'Player' not found.");
            }
        }
    }
    public void restart()
    {
        player.SetActive(true);// Activate the player GameObject
        PlayerMovement players = target.GetComponent<PlayerMovement>();
        if (players != null)
        {
            players.HealthBar.sethealth(100);
            players.Spirit_Bar.setspirit(40); // Reset player health to 100
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
    public void Quittodesktop()
    {
        player.SetActive(true); // Activate the player GameObject
        Debug.Log("Exiting Game...");
        Application.Quit();
    }

    public void exittomainmenu()
    {
        player.SetActive(true);
        // Activate the player GameObject
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void Resume()
    {
        player.SetActive(true); // Resume the audio listener
        gameObject.SetActive(false); // Deactivate the pause menu UI
    }

    // Update is called once per frame
    void Update()
    {
        player.SetActive(false); // Deactivate the player GameObject
        
    }
}
