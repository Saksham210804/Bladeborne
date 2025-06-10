using UnityEngine;

public class Spirit_Increase : MonoBehaviour
{
    public BoxCollider2D boxcollider;
    public GameObject Spirit;
    public Transform Player_target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Player_target == null)
        {
            var go = GameObject.FindWithTag("Player");
            if (go != null)
                Player_target = go.transform;
            else
                Debug.LogWarning("Enemy_AI: No GameObject with tag 'Player' found.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.SpiritIncrease(10);
            Destroy(Spirit);
        }
    }
}
