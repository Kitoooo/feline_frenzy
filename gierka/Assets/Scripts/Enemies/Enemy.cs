using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed = 3.0f;
    [SerializeField] 
    protected float attackDamage = 10.0f;
    [SerializeField]
    protected float attackSpeed = 1.0f;
    //atackSpeed is in seconds
    private float attackTimer = 0.0f;
    protected Transform m_Target;
    protected Rigidbody2D m_Body;
    protected Animator m_Animator;
    protected EnemyHealth health;

    public float m_Facing { get; protected set; }

    protected virtual void Start()
    {
        health = gameObject.GetComponent<EnemyHealth>();
        m_Animator = GetComponent<Animator>();
        m_Body = GetComponent<Rigidbody2D>();
        m_Facing = 1;
    }

    protected virtual void Update()
    {
        
    }
    protected virtual void FixedUpdate()
    {
        if(!health.isDead)
        {
            if (m_Target != null)
            {
                ChacePlayer();
                //transform.position = Vector2.MoveTowards(transform.position, m_Target.position, movementSpeed * Time.deltaTime);
            }
        }
    }

    protected virtual void ChacePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, m_Target.position, movementSpeed * Time.deltaTime);
    }

    public virtual void onPlayerSpotted(Transform player)
    {
        m_Target = player;
        m_Animator.SetBool("isWalking", true);
        Debug.Log("Target acquired");
    }
    public virtual void onPlayerLost()
    {
        m_Target = null;
        m_Animator.SetBool("isWalking", false);
        Debug.Log("Target Lost");
    }
    public virtual void onPlayerStay()
    {
        m_Facing = (m_Target.position.x - transform.position.x) > 0 ? 0 : 1;
        transform.rotation = Quaternion.Euler(0, 180 * m_Facing, 0);
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attackTimer <= 0.0f)
            {
                collision.gameObject.GetComponent<Health>().UpdateHealth(-attackDamage);
                attackTimer = attackSpeed;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }
}
//https://answers.unity.com/questions/327412/prevent-rigidbody-to-be-pushed-away-by-bullet.html