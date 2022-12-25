using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    protected Vector2 m_FacingDirection = Vector2.left;
    protected Rigidbody2D m_Body;
    protected Animator m_Animator;

    protected float horizontal;
    protected float vertical;
    protected bool m_Moving = false;
    protected WeaponBase m_Weapon;
    public GameObject WeaponPrefab;
    void Start()
    {
        m_Body = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        rotatePlayerBasedOnHorizontalInput();

        Vector2 move = new Vector2(horizontal, vertical);

        //Approximately -> move.x == 0.0f
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            m_FacingDirection.Set(move.x, move.y);
            m_FacingDirection.Normalize();
        }
        m_Moving = (move != Vector2.zero);
        m_Animator.SetBool("isMoving", m_Moving);

        if (Input.GetKeyDown(KeyCode.E) && m_Weapon == null)
        {
            EquipWeapon();
        }
        if (m_Weapon)
        {
            m_Weapon.checkTrigger();
        }

    }
    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x += horizontal * movementSpeed * Time.deltaTime;
        position.y += vertical * movementSpeed * Time.deltaTime;
        transform.position = position;
        m_Body.MovePosition(position);
    }
    private void rotatePlayerBasedOnHorizontalInput()
    {
        if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public void EquipWeapon()
    {
        GameObject newWeapon = Instantiate(WeaponPrefab, m_Body.position, Quaternion.identity);
        m_Weapon = newWeapon.GetComponent<WeaponBase>();
        m_Weapon.Owner = this;
        Debug.Log("Weapon Equiped");
    }
}

