using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
TODO: FIX MACARONI WITH ANIMATIONS
 
 */
public class BoarController : Enemy
{
    [SerializeField]
    protected float m_ChargeRange;
    //[SerializeField]
    //protected float m_ChargeDistance;
    [SerializeField]
    protected float m_ChargePreparationTime;
    [SerializeField]
    protected float m_ChargeDuration;
    [SerializeField]
    protected float m_ChargeRecoveryTime;
    [SerializeField]
    protected float m_ChargeDamage;
    protected Vector2 m_ChargeStart;
    protected Vector2 m_ChargeDestination;
    protected float elapsedTime = 0.0f;
    protected bool isCharging = false;

    protected override void Start()
    {
        base.Start();
    }
    
    protected override void Update()
    {
        base.Update();
        elapsedTime += Time.deltaTime;

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void ChacePlayer()
    {
        float distance = (m_Target.position - transform.position).magnitude;
        if(distance > m_ChargeRange && !isCharging)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_Target.position, movementSpeed * Time.deltaTime);
        }
        else if(!isCharging)
        {
            isCharging = true;
            m_ChargeStart = transform.position;
            m_ChargeDestination = m_Target.position;
            //m_Animator.SetBool("isRunning", false);
            
            StartCoroutine(Charge());
        }
    }

    public override void onPlayerSpotted(Transform player)
    {
        m_Target = player;
        m_Animator.SetBool("isRunning", true);
    }
    public override void onPlayerLost()
    {
        m_Target = null;
        m_Animator.SetBool("isRunning", false);
    }
    
    protected IEnumerator Charge()
    {
        m_Animator.SetBool("isPreparingCharge", true);
        yield return new WaitForSeconds(m_ChargePreparationTime);
        if(m_Target != null)
            m_ChargeDestination = m_Target.position;

        m_Animator.SetBool("isPreparingCharge", false);
        elapsedTime = 0.0f;
        while ((elapsedTime / m_ChargeDuration) < 1)
        {
            float progress = elapsedTime / m_ChargeDuration;

            m_Body.MovePosition(Vector2.Lerp(m_ChargeStart, m_ChargeDestination, easeOutQuart(progress)));
            yield return null;
        }

        
        m_Animator.SetBool("isRecovering",true);
        yield return new WaitForSeconds(m_ChargeRecoveryTime);
        m_Animator.SetBool("isRecovering", false);

        //m_Animator.SetTrigger("hasReceovered");
        if(m_Target == null)
        {
            m_Animator.SetBool("isRunning", false);
        }
        else
        {
            m_Animator.SetBool("isRunning", true);
        }
        isCharging = false;
    }

    float easeOutQuart(float x) 
    {
        return 1 - Mathf.Pow(1 - x, 4);
    }
}
