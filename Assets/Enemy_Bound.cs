using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Bound : MonoBehaviour
{

    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reverse the direction of the obstacle when it collides with the ground
          
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
