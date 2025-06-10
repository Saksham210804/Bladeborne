using UnityEngine;

public class Swinging_Obstacle : MonoBehaviour
{
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(target == null)
        {
            var go = GameObject.FindWithTag("Player");
            if (go == null)
            {
                target = go.transform;
            }
            else { 
                Debug.LogWarning("Enemy_AI: No GameObject with tag 'Player' found.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = target.GetComponent<PlayerMovement>();
        if (player != null)
        {
            // Trigger camera shake
            // Trigger camera shake
            player.TakeDamage(100);
            if (CameraShake.Instance != null)
            {
                CameraShake.Instance.ShakeCamera(1.5f);
            }// Assuming you have a TakeDamage method in PlayerMovement
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
