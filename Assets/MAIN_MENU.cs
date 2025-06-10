using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;




public class MAIN_MENU : MonoBehaviour
{
   
    public void Play_Game()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Load_Credits()
    {
        SceneManager.LoadScene("Main_Credit");
    }

    public void Exit_Game()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }

   
}
