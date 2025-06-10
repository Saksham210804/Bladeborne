using UnityEngine;

public class BG_VIllain : MonoBehaviour
{
    public bool isactive = false;
    public AudioSource audiosource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(audiosource == null)
        {
            audiosource = GetComponent<AudioSource>();
        }   
    }
    public void Activatemusic()
    {
        isactive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isactive)
        {
            audiosource.Play();
        }
        else
        {
            audiosource.Stop();
        }
    }
}
