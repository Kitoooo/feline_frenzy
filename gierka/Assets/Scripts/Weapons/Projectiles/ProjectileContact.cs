using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class ProjectileContact : MonoBehaviour
{
    public Vector2 lastVelocity { get; set; }

    [SerializeField]
    protected GameObject m_DamageIndicatorPrefab;
    public virtual void OnContact(Projectile self, Collision2D other)
    {
        throw new System.NotImplementedException();
    }
    public virtual void OnContact(Projectile self, Collider2D other)
    {
        throw new System.NotImplementedException();
    }

    protected void CreateDamageIndicator(Projectile self,Transform position)
    {

        GameObject indicator = Instantiate(m_DamageIndicatorPrefab, position.position, Quaternion.identity);
        TextMeshPro text = indicator.GetComponentInChildren<TextMeshPro>();
        text.text = self.OwningWeapon.attackDamage.ToString();
        //https://stackoverflow.com/questions/44437705/textmesh-pro-unity-instantiated-text-prefab-wont-change-its-position
    }
}
