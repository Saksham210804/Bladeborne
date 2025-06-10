using UnityEngine;

public class MainCredit : MonoBehaviour
{
    public float wait = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(wait > 5)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
        }
        else
        {
            wait += Time.deltaTime;
        }
    }
}
