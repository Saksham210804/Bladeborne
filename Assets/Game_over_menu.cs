using UnityEngine;

public class Game_over_menu : MonoBehaviour
{
    public Transform target;    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
     PlayerMovement player = target.GetComponent<PlayerMovement>();
            if(player != null)
        {
            player.HealthBar.sethealth(100);
            player.Spirit_Bar.setspirit(40); // Reset player health to 100
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
    public void Quittodesktop()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }

    public void exittomainmenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
