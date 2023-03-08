using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Buff", menuName = "Powerup Effects/Damage Buff")]
public class DamageBuff : PowerupEffect
{
    public float amount;

    public override void ApplyEffect(GameObject target)
    {
        target.GetComponent<PlayerController>().m_EquippedWeapon.attackDamage += amount;
        Debug.Log("Buff applied to " + target);
    }

    public override void RemoveEffect(GameObject target)
    {
        target.GetComponent<PlayerController>().m_EquippedWeapon.attackDamage -= amount;
        Debug.Log("Buff removed from " + target);
    }
}
