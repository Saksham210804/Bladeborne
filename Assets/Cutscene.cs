using UnityEngine;
using UnityEngine.Playables;

public class Cutscene : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject bossObject;
    public GameObject VillainUI;

    private Villain_AI bossScript;
    private Spawn_Health_spirit spawnHealthSpirit;
    public GameObject Music;
    public GameObject Music_Villain;

    private void Start()
    {
        if (bossObject != null)
        {
            bossScript = bossObject.GetComponent<Villain_AI>();
            if (bossScript == null)
                Debug.LogWarning("Boss object does not have Villain_AI script.");
        }
        if(Music == null)
        {
            GameObject.FindWithTag("BG_Music");
        }
        spawnHealthSpirit = FindFirstObjectByType<Spawn_Health_spirit>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Cutscene triggered!");
            if (Music != null)
            {
                Music.SetActive(false);
            }
            timeline.Play();
            timeline.stopped += OnCutsceneFinished;
            if (VillainUI != null)
            {
                VillainUI.SetActive(true);
                Debug.Log("Villain UI activated.");
            }
            spawnHealthSpirit.ActiveSpawn();
            Destroy(gameObject);
           

        }
    }

    private void OnCutsceneFinished(PlayableDirector director)
    {
        Debug.Log("Cutscene finished!");

        if (bossScript != null)
        {
            bossScript.ActivateBoss();
            if(Music_Villain != null)
            {
                Music_Villain.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Music_Villain GameObject is not assigned.");
            }



        }
        else
        {
            Debug.LogWarning("No boss script found on bossObject.");
        }
      
        timeline.stopped -= OnCutsceneFinished;
    }
}
