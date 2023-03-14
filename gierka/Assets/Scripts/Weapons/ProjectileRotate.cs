using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected float m_RotationSpeed;
    protected Rigidbody2D m_Body;

    void Start()
    {
        m_Body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Body.transform.Rotate(new Vector3(0, 0, m_RotationSpeed));
    }
}
