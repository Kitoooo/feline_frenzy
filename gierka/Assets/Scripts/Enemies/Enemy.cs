using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 0.0f;
    [SerializeField] 
    private float maxHealth = 30.0f;
    public float movementSpeed = 3.0f;
    [SerializeField] 
    private float attackDamage = 10.0f;
    [SerializeField] 
    private float attackSpeed = 1.0f;
    //atackSpeed is in seconds
    private float attackTimer = 0.0f;
    protected Transform m_Target;
    protected Rigidbody2D m_Body;
    protected Animator m_Animator;

    [SerializeField] 
    protected float m_DespawnTimer = 5.0f;
    protected bool m_IsDead = false;

    //fading animation after death
    [SerializeField]
    protected float m_FadeDuration = 1.0f;
    protected SpriteRenderer m_SpriteRenderer;

    void Start()
    {
        health = maxHealth;
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(m_IsDead)
        {
            m_DespawnTimer -= Time.deltaTime;
            if(m_DespawnTimer <= 0)
            {
                if (m_SpriteRenderer.material.color.a > 0.0f)
                {
                    m_SpriteRenderer.material.color = new Color(1f, 1f, 1f, m_SpriteRenderer.material.color.a - 1/m_FadeDuration * Time.deltaTime);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

    }
    private void FixedUpdate()
    {
        if(!m_IsDead)
        {
            if (m_Target != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, m_Target.position, movementSpeed * Time.deltaTime);
            }
        }
    }

    public void onPlayerSpotted(Transform player)
    {
        m_Target = player;
        m_Animator.SetBool("isWalking", true);
        Debug.Log("Target acquired");
    }
    public void onPlayerLost()
    {
        m_Target = null;
        m_Animator.SetBool("isWalking", false);
        Debug.Log("Target Lost");
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
    
    public void UpdateHealth(float amount)
    {
        Debug.Log("Enemy took " + amount + " damage");
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0.0f)
        {
            m_IsDead = true;
            Die();
        }
        else if (amount < 0)
        {
            m_Animator.SetTrigger("Hit");
        }
    }

    private void Die()
    {
        m_Animator.SetBool("isDead", m_IsDead);
        m_Body.simulated = false;
        //TODO: deduwa xD;
        Debug.Log("Enemy died");
    }
}
//https://answers.unity.com/questions/327412/prevent-rigidbody-to-be-pushed-away-by-bullet.html