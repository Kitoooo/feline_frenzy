using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageController : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public Vector2 m_FacingDirection = Vector2.left;
    protected Rigidbody2D m_Body;
    protected Animator m_Animator;

    protected float horizontal;
    protected float vertical;
    protected bool m_Moving = false;
    void Start()
    {
        m_Body = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        //Approximately -> move.x == 0.0f
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            m_FacingDirection.Set(move.x, move.y);
            m_FacingDirection.Normalize();
        }
        m_Moving = (move != Vector2.zero);
        m_Animator.SetBool("isMoving", m_Moving);
        m_Animator.SetFloat("facingLeft", move.x);
    }
    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x += horizontal * movementSpeed * Time.deltaTime;
        position.y += vertical * movementSpeed * Time.deltaTime;
        transform.position = position;
        m_Body.MovePosition(position);
    }
}
