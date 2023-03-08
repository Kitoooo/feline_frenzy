using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class WeaponBase : MonoBehaviour
{
    //xddddddddddddddddddddddddddddddddddddddddddddddd
    public abstract GameObject AbstractProjectilePrefab { get; }
    //holds reference to weapon owner
    protected PlayerController m_Owner;
    //used by projectiles to determine where to move
    protected Vector3 m_AttackDirection = Vector3.left;

    //attack speed / fire rate in seconds
    [SerializeField]
    protected float attackSpeed;
    protected float m_AttackSpeedTimer = 0.0f;

    [SerializeField]
    public float attackDamage;

    [SerializeField]
    protected WeaponTriggerType m_TriggerType;

    public PlayerController Owner
    {
        get { return m_Owner; }
        set { m_Owner = value; }
    }

    public float orbitRadius = 1;
    public bool pointAtCursor = false;

    protected void Update() 
    {
        if (m_AttackSpeedTimer > 0.0f)
        {
            m_AttackSpeedTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        OrbitOwner();
    }

    //to allow enemies to use weapons add || Owner.tags.contains(enemy) to every if
    public void checkTrigger()
    {
        if (m_AttackSpeedTimer<=0.0f)
        {
            bool attacked = false;
            switch(m_TriggerType)
            {
                case WeaponTriggerType.Auto:
                    if(Input.GetMouseButton(0))
                    {
                        Attack();
                        attacked = true;
                    }
                    break;
                case WeaponTriggerType.Burst:
                    if (Input.GetMouseButtonDown(0))
                    {
                        for(int i = 0; i < 2; i++) //this sucks
                            Attack();
                        attacked = true;
                    }
                    break;
                case WeaponTriggerType.Semi_Auto:
                    if (Input.GetMouseButtonDown(0))
                    {
                        Attack();
                        attacked = true;
                    }
                    break;
            }
            if(attacked)
            {
                m_AttackSpeedTimer = 1 / attackSpeed;
            }
        }
    }

    public abstract void Attack();

    protected void OrbitOwner()
    {
        if(m_Owner)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.x -= Screen.width / 2;
            mousePosition.y -= Screen.height / 2;
            Vector3 direction = mousePosition - m_Owner.transform.position;
            direction = direction.normalized * new Vector2(orbitRadius, orbitRadius);

            var renderer = m_Owner.GetComponent<Renderer>();
            Vector3 offset = new Vector3(0, renderer.bounds.size.y / 4, 0);
            transform.position = m_Owner.transform.position + direction + offset;

            if (pointAtCursor)
            {
                float dot = Vector2.Dot(Vector2.left, direction);
                float det = Vector2.left.x * direction.y - Vector2.left.y * direction.x;
                float rotationZ = Mathf.Atan2(det, dot);
                rotationZ = rotationZ * Mathf.Rad2Deg;
                rotationZ += 90;

                transform.eulerAngles = new Vector3(
                    transform.eulerAngles.x,
                    transform.eulerAngles.y,
                    rotationZ
                );
            }
            m_AttackDirection = direction.normalized;
        }
    }

    public enum WeaponTriggerType
    {
        Auto,
        Burst,
        Semi_Auto
    }
}
