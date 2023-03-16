using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileContact : MonoBehaviour
{
    public Vector2 lastVelocity { get; set; }
    public abstract void OnContact(Projectile self, Collision2D other);
}
