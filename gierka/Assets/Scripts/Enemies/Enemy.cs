using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 0.0f;
    [SerializeField] private float maxHealth = 30.0f;
    public float movementSpeed = 3.0f;
    private Transform target;
    [SerializeField] private float attackDamage = 10.0f;
    [SerializeField] private float attackSpeed = 1.0f;
    //atackSpeed is in seconds
    private float attackTimer = 0.0f;
    private CircleCollider2D m_CircleCollider;
    [SerializeField] private float spottingDistance = 4.0f;
    protected Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        m_CircleCollider = GetComponent<CircleCollider2D>();
        m_CircleCollider.radius = spottingDistance;
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attackTimer <= 0.0f)
            {
                collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                attackTimer = attackSpeed;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.gameObject.transform;
            m_Animator.SetBool("isWalking", true);
            Debug.Log("Target acquired");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = null;
            m_Animator.SetBool("isWalking", false);
            Debug.Log("Target Lost");
        }
    }

    public void UpdateHealth(float amount)
    {
        Debug.Log("Enemy took " + amount + " damage");
        health += amount;
        if(amount < 0)
        {
            m_Animator.SetTrigger("Hit");
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        //TODO: deduwa xD;
        health = 0.0f;
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }
}
