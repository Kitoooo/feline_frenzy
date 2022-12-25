using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private CircleCollider2D m_CircleCollider;
    [SerializeField]
    protected float spottingDistance = 4.0f;
    [SerializeField]
    protected GameObject Owner;
    protected Enemy m_Enemy;


    void Start()
    {
        m_CircleCollider = GetComponent<CircleCollider2D>();
        m_CircleCollider.radius = spottingDistance;
        m_Enemy = Owner.GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_Enemy.onPlayerSpotted(collision.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_Enemy.onPlayerLost();
        }
    }
}
