using UnityEngine;

public class BG_Music : MonoBehaviour
{
    public AudioSource bg_music;
    public bool isactive = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(bg_music == null)
        {
            bg_music = GetComponent<AudioSource>();
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        if(isactive)
        {
            bg_music.Play();
        }
        
    }
}
