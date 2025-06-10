using UnityEngine;

public class Fireball_Explode : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    public void Explosion_Trigger()
    {
        anim.SetBool("Hit", true);
    }
}

