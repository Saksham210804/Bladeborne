using UnityEngine;

public class Move_Obstacle : MonoBehaviour
{
    public BoxCollider2D boxcollider_tile;
    public float speed = 2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

 void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            // Reverse the direction of the obstacle when it collides with the ground
            speed = -speed;
         
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
       
    }
}
