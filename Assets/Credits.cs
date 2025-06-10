using UnityEngine;

public class Credits : MonoBehaviour
{
    public float wait = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wait > 8f)
        {

           UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
        else
        {
            wait += Time.deltaTime;
        }
    }
}
