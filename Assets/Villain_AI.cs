using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
public class Villain_AI : MonoBehaviour
{
    private Vector3 _originalScale;

    [Header("Components")]
    public CapsuleCollider2D boxCollider;
    public Animator animator;
    public Transform target;
    public Villain_Health HealthBar;
    public GameObject HealthOrb;
    public GameObject SpiritOrb;
    public Rigidbody2D rb; // 🆕 Added Rigidbody2D reference
    public Transform groundCheck; // 🆕 For jump detection
    public LayerMask groundLayer;
    public CinemachineImpulseSource impulsesource;
    public AudioSource audiosource;
    public AudioClip attackSound;
    public AudioClip DieSound;
    public AudioClip BG;
    public Camera_Shake camerashake;
    public bool enemy_die = false; // 🆕 Track if the enemy has died

    [Header("Stats")]
    public float health = 300f;
    public float attackCooldown = 2f;
    public float speed = 3f;
    private bool _playerInRange = false;
    private float _cooldownTimer = Mathf.Infinity;
    private float currentHealth;
    private bool isJumping = false;
    [SerializeField] private float jumpForce = 15f;
    private bool isActive = false;// 🆕 Wait time for scene change
    private int dropCount = 0; // 🆕 Prevent repeated drops

    public void ActivateBoss()
    {
        isActive = true;
        Debug.Log("Boss activated!");
    }

    void Start()
    {

       

        currentHealth = health;
        _originalScale = transform.localScale;
        PlayerMovement players = target.GetComponent<PlayerMovement>();

        impulsesource = GetComponent<CinemachineImpulseSource>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (rb == null)
            rb = GetComponent<Rigidbody2D>(); // 🆕 Ensure Rigidbody2D exists

        if (HealthBar != null)
        {
            HealthBar.setmaxhealth_enemy((int)health);
        }

        if (target == null)
        {
            var go = GameObject.FindWithTag("Player");
            if (go != null)
                target = go.transform;
            else
                Debug.LogWarning("Villain_AI: No GameObject with tag 'Player' found.");
        }
    }

    void Update()
    {
        if (!isActive || target == null) return;
       
        _cooldownTimer += Time.deltaTime;

        Vector3 direction = (target.position - transform.position);
        float horizontalDir = direction.x;

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        float distance = Vector2.Distance(transform.position, target.position);
        if (horizontalDir != 0 && distance > 1f) // ✅ Prevent flipping if too close
        {
            transform.localScale = new Vector3(
                _originalScale.x * Mathf.Sign(horizontalDir),
                _originalScale.y,
                _originalScale.z
            );
        }


        if (_playerInRange)
        {
            rb.linearVelocity = Vector2.zero; // ✅ Stop boss when attacking
            animator.SetFloat("Speed", 0f);

            if (_cooldownTimer >= attackCooldown)
            {
                _cooldownTimer = 0f;

                if (Random.value > 0.5f)
                {
                    animator.SetTrigger("IsAttack_1");

                    StartCoroutine(camerashake.Shake(.25f, 1.4f));
                    audiosource.clip = attackSound;
                    audiosource.Play();
                    PlayerMovement player = target.GetComponent<PlayerMovement>();
                    if (player != null)
                    {
                        if (player.IsBlocking())
                        {
                            player.TakeDamage(5);
                        }
                        else
                        {
                            player.TakeDamage(20f);
                        }
                    }
                }
                else
                {       animator.SetTrigger("IsAttack_2");

                    StartCoroutine(camerashake.Shake(.25f, 1.4f));
                    audiosource.clip = attackSound;
                    audiosource.Play();
                    PlayerMovement player = target.GetComponent<PlayerMovement>();
                    if (player != null)
                    {
                        if (player.IsBlocking())
                        {
                            player.TakeDamage(5);
                        }
                        else
                        {
                            player.TakeDamage(20f);
                        }
                    }
                }

               
            }
        }
        else
        {
            Vector3 move = new Vector3(horizontalDir, 0f, 0f).normalized;
            rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y); // ✅ Fixed
            animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        }
    }


  
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                _playerInRange = true;

            // ✅ Handle fireball damage
            if (other.CompareTag("FireBall"))
            {
                TakeDamage(20f); // Or whatever value your fireball does

                // Optional: play explosion effect
                 // Destroy explosion after 1 second

                Destroy(other.gameObject); // Destroy fireball
            }
        }

    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _playerInRange = false;
    }

    public void TakeDamage(float damage)
    {
        if (!isActive) return;

        float actualDamage = damage * 0.2f; // ✅ Reduce damage to boss
        currentHealth -= actualDamage;

        HealthBar.SetHealth_enemy((int)currentHealth);
        // animator.SetTrigger("Hurt"); // Optional: disable if it causes slowdowns

        if (currentHealth <= 240 && dropCount < 1)
        {
            
            dropCount = 1;
        }
        if (currentHealth <= 200)
        {
            speed = 7f;
        }
        if (currentHealth <= 150 && dropCount < 2)
        {
            
            JumpTowardsPlayer();
            dropCount = 2;
        }
        if (currentHealth <= 100)
        {
            speed = 15f;

            if (_cooldownTimer >= attackCooldown)
            {
                _cooldownTimer = 0f;
                StartCoroutine(ComboAttack());
            }

            if (dropCount < 3)
            {
                dropCount = 3;
            }
        }

        if (currentHealth <= 0)
            Die();
    }


    private IEnumerator ComboAttack()
    {
        PlayerMovement player = target.GetComponent<PlayerMovement>();
        if (player != null)
        {
            if (player.IsBlocking())
            {
                player.TakeDamage(5);
            }
          
        }
        animator.SetTrigger("IsAttack_1");
        StartCoroutine(camerashake.Shake(.25f,1.4f));
        player.TakeDamage(25f);
        yield return new WaitForSeconds(0.8f);
        animator.SetTrigger("IsAttack_2");

        StartCoroutine(camerashake.Shake(.25f, 1.4f));
        player.TakeDamage(25f);
    }

    private void JumpTowardsPlayer()
    {
        if (isJumping || !IsGrounded()) return;

        isJumping = true;
        animator.SetTrigger("Jump");

        if (rb != null)
        {
            Vector2 jumpDir = new Vector2(Mathf.Sign(target.position.x - transform.position.x), 1f).normalized;
            rb.linearVelocity = new Vector2(jumpDir.x * 10f, jumpForce);
        }

        Invoke("ResetJump", 1.5f);
    }

    private bool IsGrounded() // 🆕 Ground check
    {
        if (groundCheck == null) return true;
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void ResetJump()
    {
        isJumping = false;
    }

    

    private void Die()
    {  
        animator.SetBool("Die", true);
        audiosource.clip = DieSound;
        audiosource.Play();
        Destroy(gameObject, 2f);
        Destroy(HealthBar.gameObject, 2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Credit");
       }

    

}

