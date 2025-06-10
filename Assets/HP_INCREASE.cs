using UnityEngine;

public class HP_INCREASE : MonoBehaviour
{
    public BoxCollider2D boxcolloder;
    private float HP = 20;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player!= null)
            {
                player.HPincrease(HP);
                Destroy(gameObject); // Destroy the health pickup after use
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
