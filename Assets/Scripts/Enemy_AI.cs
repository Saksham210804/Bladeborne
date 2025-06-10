using TMPro;
using System.Collections;
using UnityEngine;
using Unity.Mathematics;

public class Enemy_AI : MonoBehaviour
{
    private Vector3 _originalScale;

    [Header("Components")]
    public CapsuleCollider2D boxCollider;
    public Animator animator;
    public Transform target;
    public GameObject Health;// Player
    public enemy_H_Bar HealthBar;
    [Header("Bounds for Patrol")]
    public Transform leftBound;
    public Transform rightBound;
    public AudioSource audiosource;

    [Header("Stats")]
    public float speed = 2f;
    public float attackCooldown = 1f;
    public float health = 60f;
    private float CurrentHealth;
   

    private bool _playerInRange = false;
    private float _cooldownTimer = Mathf.Infinity;
    private bool _isDead = false;
    private bool _movingRight = true;

    void Start()
    {
        _originalScale = transform.localScale;
        CurrentHealth = health;
        HealthBar.setmaxhealth_enemy((int)health);
        if (animator == null)
            animator = GetComponent<Animator>();

        if (target == null)
        {
            var go = GameObject.FindWithTag("Player");
            if (go != null)
                target = go.transform;
            else
                Debug.LogWarning("Enemy_AI: No GameObject with tag 'Player' found.");

        }
        if(rightBound == null)
        {
            var bounds = GameObject.FindWithTag("Obstacle_R");
        }
        if(leftBound == null)
        {
            var bounds = GameObject.FindWithTag("Obstacle_L");
        }
        if (HealthBar != null)
        {
            HealthBar.setmaxhealth_enemy((int)health);
        }
    }

    void Update()
    {
        if (_isDead) return;

        _cooldownTimer += Time.deltaTime;



        if (_playerInRange)
        {
            AttackLogic();
        }
        else if(!_playerInRange)
        {
            PatrolBetweenBounds();
        }
        
        
        
    }

    void PatrolBetweenBounds()
    {
        animator.SetFloat("Speed", speed);

        Vector3 targetPos = _movingRight ? rightBound.position : leftBound.position;
        float newX = Mathf.MoveTowards(transform.position.x, targetPos.x, speed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        float dir = _movingRight ? 1f : -1f;
        transform.localScale = new Vector3(_originalScale.x * dir, _originalScale.y, _originalScale.z);

        // Flip direction only when really close to the edge
        if (Mathf.Abs(transform.position.x - targetPos.x) < 0.05f)
        {
            _movingRight = !_movingRight;
        }
    }

    


    void AttackLogic()
    {
        animator.SetFloat("Speed", 0f);

        // Face the player during attack
        if (target != null)
        {
            float direction = target.position.x - transform.position.x;
            float dir = (direction < 0) ? -1f : 1f;
            transform.localScale = new Vector3(_originalScale.x * dir, _originalScale.y, _originalScale.z);
        }

        if (_cooldownTimer >= attackCooldown)
        {
            _cooldownTimer = 0f;
            animator.SetTrigger("IsAttack");
            audiosource.Play();
            PlayerMovement player = target.GetComponent<PlayerMovement>();
            if (player != null)
            {
                if (player.IsBlocking())
                {
                    player.TakeDamage(0);
                }
                else
                {
                    player.TakeDamage(15f);
                }
            }

        }
    }

    void ChasePlayer()
    {
        animator.SetFloat("Speed", speed);
        float dir = (target.position.x < transform.position.x) ? -1f : 1f;
        transform.localScale = new Vector3(_originalScale.x * dir, _originalScale.y, _originalScale.z);

        float newX = Mathf.MoveTowards(transform.position.x, target.position.x, speed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }

    public void TakeDamage(float damage)
    {
        if (_isDead) return;

        CurrentHealth -= damage;
        if (HealthBar != null)
        {
            HealthBar.SetHealth_enemy((int)CurrentHealth);
        }
        animator.SetTrigger("GetHIt");

        if (CurrentHealth <= 0)
        {
            Die();
            
        }
    }

    private void Die()
    {
        _isDead = true;
        animator.SetTrigger("Die");

        // Offset the spawn position slightly upward (e.g., by 1 unit)
        Vector3 spawnPosition = transform.position + new Vector3(0f, 1f, 0f);

        // Spawn the health prefab
        if (Health != null)
        {
            Instantiate(Health, spawnPosition, Quaternion.identity);
        }

        // Destroy enemy after 2 seconds
        Destroy(gameObject, 2f);
    }

}
