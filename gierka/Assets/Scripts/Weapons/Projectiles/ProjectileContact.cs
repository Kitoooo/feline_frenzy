using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileContact : MonoBehaviour
{
    public abstract void OnContact(Projectile self,Collider2D other);
}
