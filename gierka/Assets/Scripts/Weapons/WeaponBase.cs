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
    public Transform firePoint;

    [SerializeField]
    protected WeaponTriggerType m_TriggerType;

    public PlayerController Owner
    {
        get { return m_Owner; }
        set { m_Owner = value; }
    }

    public float orbitRadius = 1;
    public bool pointAtCursor = false;
    public bool attacked { get; protected set; }

    protected virtual void Start()
    {
        attacked = false;
    }

    protected virtual void Update()
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
        if (m_AttackSpeedTimer <= 0.0f)
        {
            attacked = false;
            switch (m_TriggerType)
            {
                case WeaponTriggerType.Auto:
                    if (Input.GetMouseButton(0))
                    {
                        Attack();
                        attacked = true;
                    }
                    break;
                case WeaponTriggerType.Burst:
                    if (Input.GetMouseButtonDown(0))
                    {
                        float delay = Mathf.Min(attackSpeed / 3, 0.1f);
                        StartCoroutine(BurstFire(delay));
                        attacked = true;
                    }
                    break;
                case WeaponTriggerType.SemiAuto:
                    if (Input.GetMouseButtonDown(0))
                    {
                        Attack();
                        attacked = true;
                    }
                    break;
                case WeaponTriggerType.Hold:
                    if (Input.GetMouseButton(0))
                    {
                        Attack();
                        attacked = true;
                    }
                    else
                    {
                        attacked = false;

                    }
                    break;
            }
            if (attacked)
            {
                m_AttackSpeedTimer = 1 / attackSpeed;
            }
        }
    }

    public abstract void Attack();

    protected void OrbitOwner()
    {
        if (m_Owner)
        {
            Vector2 ownerPosition = m_Owner.transform.position;
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 direction = (mousePosition - ownerPosition).normalized * orbitRadius;
            //Debug.DrawRay(mousePosition, m_Owner.transform.position,Color.green);
            var renderer = m_Owner.GetComponent<Renderer>();
            Vector2 offset = new Vector2(0, renderer.bounds.size.y / 4);
            transform.position = ownerPosition + offset + direction;

            if (pointAtCursor)
            {
                float dot = Vector2.Dot(Vector2.right, direction);
                float det = Vector2.right.x * direction.y - Vector2.right.y * direction.x;
                float rotationZ = Mathf.Atan2(det, dot);
                rotationZ = rotationZ * Mathf.Rad2Deg;
                rotationZ += 90;

                transform.eulerAngles = new Vector3(
                    transform.eulerAngles.x,
                    transform.eulerAngles.y,
                    rotationZ
                );
            }
            else
            {
                float angle = Vector3.Angle(Vector3.right, direction);
                if (angle < 90)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            Vector2 fp = firePoint.position;
            m_AttackDirection = (mousePosition - fp).normalized;
        }
    }

    private IEnumerator BurstFire(float delay)
    {
        for (int i = 0; i < 3; i++)
        {
            Attack();
            yield return new WaitForSeconds(delay);
        }
    }

    public enum WeaponTriggerType
    {
        Auto,
        Burst,
        SemiAuto,
        Hold
    }
}
