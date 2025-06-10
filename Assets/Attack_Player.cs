using UnityEngine;
using System.Collections;

public class Attack_Player : MonoBehaviour
{
    private Collider2D currentEnemyCollider;
    private Collider2D currentVillainCollider;
    private bool isAttacking = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            currentEnemyCollider = collision;
        }
        else if (collision.CompareTag("Villain"))
        {
            currentVillainCollider = collision;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == currentEnemyCollider)
        {
            currentEnemyCollider = null;
        }
        if (collision == currentVillainCollider)
        {
            currentVillainCollider = null;
        }
    }

    void Update()
    {
        if (!isAttacking && (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K)))
        {
            if (currentEnemyCollider != null)
                StartCoroutine(AttackEnemy(currentEnemyCollider));
            else if (currentVillainCollider != null)
                StartCoroutine(AttackVillain(currentVillainCollider));
        }
    }

    IEnumerator AttackEnemy(Collider2D enemyCollider)
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.02f);

        if (enemyCollider != null)
        {
            Enemy_AI enemy = enemyCollider.GetComponent<Enemy_AI>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);
                Debug.Log("Enemy hit for 10 damage!");
            }
        }

        isAttacking = false;
    }

    IEnumerator AttackVillain(Collider2D villainCollider)
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);

        if (villainCollider != null)
        {
            Villain_AI villain = villainCollider.GetComponent<Villain_AI>();
            if (villain != null)
            {
                villain.TakeDamage(15);
                Debug.Log("Villain hit for 15 damage!");
            }
        }

        isAttacking = false;
    }
}
