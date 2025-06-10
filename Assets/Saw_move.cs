using UnityEngine;

public class Saw_move : MonoBehaviour
{
    public float stepSize = 1f;
    public float stepInterval = 1f; // 1 second between steps
    public int stepsPerDirection = 5;

    private float timer = 0f;
    private int stepCount = 0;
    private int direction = 1; // 1 = right, -1 = left
    public Transform target;

    private void Start()
    {
        if (target == null)
        {
            var go = GameObject.FindWithTag("Player");
            if (go != null)
                target = go.transform;
            else
                Debug.LogWarning("Enemy_AI: No GameObject with tag 'Player' found.");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= stepInterval)
        {
            transform.position += new Vector3(stepSize * direction, 0, 0);
            stepCount++;
            timer = 0f;
        }

        if (stepCount >= stepsPerDirection)
        {
            direction *= -1;      // Reverse direction
            stepCount = 0;        // Reset step counter
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = target.GetComponent<PlayerMovement>();
        if (player != null)
        {
            // Trigger camera shake
            // Trigger camera shake
            player.TakeDamage(50);
            if (CameraShake.Instance != null)
            {
                CameraShake.Instance.ShakeCamera(1.5f);
            }// Assuming you have a TakeDamage method in PlayerMovement
        }
    }
}
