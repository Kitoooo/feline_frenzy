using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField]
    protected float m_DespawnTimer = 5.0f;

    //fading animation after death
    [SerializeField]
    protected float m_FadeDuration = 1.0f;
    [SerializeField]
    protected string m_DeathAnimationName = "Death";
    protected SpriteRenderer m_SpriteRenderer;
    protected Animator m_Animator;

    protected void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        var info = m_Animator.GetCurrentAnimatorStateInfo(0);
        if (isDead && info.IsName(m_DeathAnimationName) && info.normalizedTime >= 1)
        {
            m_DespawnTimer -= Time.deltaTime;
            if (m_DespawnTimer <= 0)
            {
                if (m_SpriteRenderer.material.color.a > 0.0f)
                {
                    m_SpriteRenderer.material.color = new Color(1f, 1f, 1f, m_SpriteRenderer.material.color.a - 1 / m_FadeDuration * Time.deltaTime);
                }
                else
                {
                    onDestroy();
                    Destroy(gameObject);
                }
            }
        }
    }

    protected override void onDeath()
    {
        m_Animator.SetBool("isDead", isDead);
        GetComponent<Rigidbody2D>().simulated = false;
        LevelController.instance.addExperience(200);
    }

    protected override void onDamageTaken()
    {
        m_Animator.SetTrigger("Hit");
    }

    protected virtual void onDestroy()
    {
        
    }
}
