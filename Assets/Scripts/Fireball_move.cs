using UnityEngine;
using System.Collections;

public class Fireball_move : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 moveDirection;
    public Animator animator;
    private bool hasHit = false;
    public AudioSource audiosource; // 🆕 Added AudioSource reference

    private void Start()
    {
        if(audiosource == null)
        {
            audiosource = GetComponent<AudioSource>();
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;

        if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void Update()
    {
        if (!hasHit)
        {
            transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;

        bool didHit = false;

        Villain_AI villain = collision.GetComponent<Villain_AI>();
        if (villain != null)
        {
            villain.TakeDamage(25);
            audiosource.Play();
            didHit = true;
        }

        Enemy_AI enemy = collision.GetComponent<Enemy_AI>();
        if (enemy != null)
        {
            enemy.TakeDamage(40);
            audiosource.Play();
            didHit = true;
        }

        if (didHit)
        {
            hasHit = true;

            Fireball_Explode fireballExplode = GetComponent<Fireball_Explode>();
            if (fireballExplode != null)
            {
                fireballExplode.Explosion_Trigger();
            }
            else
            {
                Debug.LogWarning("Fireball_Explode component not found on fireball prefab.");
            }

            if (CameraShake.Instance != null)
            {
                CameraShake.Instance.ShakeCamera(1.5f);
            }

            StartCoroutine(WaitAndDestroy());
        }
    }

    private IEnumerator WaitAndDestroy()
    {
        animator.SetTrigger("Destroy");
        yield return new WaitForSeconds(0.6f); // Match explosion anim duration
        Destroy(gameObject);
    }
}

