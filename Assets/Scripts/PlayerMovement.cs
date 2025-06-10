using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float HorizontalSpeed = 0f;
    public float runspeed = 140f;
    public bool Jump = false;
    public bool Crouch = false;
    public Transform firePoint;
    public GameObject fireball_01;
    private float MaxHealth = 200f;
    private float CurrentHealth;
    public HealthBar HealthBar;
    public AudioSource audiosource_run;
    public AudioSource audiosource_Attack;
    // stamina points recovered per second when crouch-running
    // stamina points recovered per second when not crouch-running
    
    public Transform target;
    private float MaxSpirit = 80f;
    private float CurrentSpirit = 0f; // Initial spirit points
    public Spirit_Bar Spirit_Bar;
    private bool facingRight = true;// To keep track of the player's facing direction
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        CurrentSpirit = MaxSpirit; // Initialize current spirit points to max
        CurrentHealth = MaxHealth;
        if(target == null)
        {
            var go = GameObject.FindWithTag("Enemy");
            if (go != null)
                target = go.transform;
            else
                Debug.LogWarning("PlayerMovement: No GameObject with tag 'Enemy' found.");

        }
        HealthBar.setmaxhealth((int)MaxHealth);
        Spirit_Bar.setmaxspirit((int)MaxSpirit);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0) facingRight = true;
        else if (Input.GetAxisRaw("Horizontal") < 0) facingRight = false;
        HorizontalSpeed = Input.GetAxisRaw("Horizontal")*runspeed;

        animator.SetFloat("Speed", Mathf.Abs(HorizontalSpeed));
        if ((HorizontalSpeed > 0.01f || HorizontalSpeed < -0.01f))
        {
            if (!audiosource_run.isPlaying)
                audiosource_run.Play();
        }
        else
        {
            if (audiosource_run.isPlaying)
                audiosource_run.Stop();
        }



        if (Input.GetKeyDown (KeyCode.Space))
        {
            Jump = true;
            animator.SetBool("IsJump", true);

            audiosource_run.Stop() ;
            
        }
      
        
        if (Input.GetKeyDown(KeyCode.J) && !animator.GetCurrentAnimatorStateInfo(0).IsName("IsAttack"))
        {
            animator.SetTrigger("IsAttack");
            
            audiosource_Attack.Play();
           
        }
        if (Input.GetKeyDown(KeyCode.K) && !animator.GetCurrentAnimatorStateInfo(0).IsName("IsAttack(1)"))
        {
            animator.SetTrigger("IsAttack(1)");
            audiosource_Attack.Play();

        }
        if (Input.GetKeyDown(KeyCode.L) && !animator.GetCurrentAnimatorStateInfo(0).IsName("IsBlock"))
        {
            animator.SetTrigger("IsBlock");
            
        }
        if (Input.GetKeyDown(KeyCode.I) )
        {
            CastFireball();
           
        }
       

    }
    public bool IsBlocking()
    {
        return Input.GetKey(KeyCode.L); // or use a flag if blocking is time-based
    }

    void CastFireball()
    {
        if (CurrentSpirit > 0)
        {
            animator.SetTrigger("IsCast");

            GameObject newFireball = Instantiate(fireball_01, firePoint.position, Quaternion.identity);
            Fireball_move fbScript = newFireball.GetComponent<Fireball_move>();
            fbScript.SetDirection(facingRight ? Vector2.right : Vector2.left);

            CurrentSpirit -= 10f;
            Spirit_Bar.setspirit((int)CurrentSpirit);
        }
    }


    public void OnLand()
    {
        animator.SetBool("IsJump", false);
    }

    
    private void FixedUpdate()
    {
        controller.Move(HorizontalSpeed*Time.fixedDeltaTime,Crouch, Jump);
        Jump = false;
    }

    
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        HealthBar.sethealth((int)CurrentHealth);
        animator.SetTrigger("hurt");// Trigger the hit animation
        if (CurrentHealth <= 0)
        {
            animator.SetTrigger("Die");
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene"); // Load the Game Over scene
            
        }
    }
    public void HPincrease(float HP)
    {
        CurrentHealth += HP;
        HealthBar.sethealth((int)CurrentHealth);
    }
    public void SpiritIncrease(float Spirit) {
        CurrentSpirit += Spirit;
        Spirit_Bar.setspirit((int)CurrentSpirit);
    }
    
}
