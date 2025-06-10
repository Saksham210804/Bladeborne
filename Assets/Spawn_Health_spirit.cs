using System.Collections;
using UnityEngine;

public class Spawn_Health_spirit : MonoBehaviour
{
    public GameObject health;
    public GameObject spirit;
    private float time = 0f;
    private bool isActive;// Delay before spawning health and spirit orbs
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(health == null)
        {
            GameObject.FindWithTag("Health");
        }
        if(spirit == null)
        {

            GameObject.FindWithTag("Spirit");
        }
    }
    public void ActiveSpawn()
    {
        isActive = true;
        Debug.Log("Spawn_Health_spirit: ActiveSpawn called, spawning will start now.");
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (time >= 10f)
            {

                Vector3 offset = new Vector3(Random.Range(830f, 900f), Random.Range(-5.8f, -4f), 0f);

                SpawnOrb(offset);// Spawn orbs at random positions around the object
                time = 0f;

            }
            else
            {
                time += Time.deltaTime;
            }
        }
       
    } 
    private void SpawnOrb(Vector3 pos)
    {
        Instantiate(health, pos, Quaternion.identity);
        Debug.Log("Spawned Health Orb at position: " + pos);
        Instantiate(spirit, pos, Quaternion.identity);
        Debug.Log("Spawned Spirit Orb at position: " + pos);
    }
}
