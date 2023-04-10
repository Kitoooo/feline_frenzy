using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    protected override void onDeath()
    {
        Debug.Log("dead");
        //throw new System.NotImplementedException();
        GameOverScreen.instance.Show();
    }

    protected override void onDamageTaken()
    {
        //todo: hit sounds
        //throw new System.NotImplementedException();
        HealthUI.instance.SetValue(m_CurrentHealth/m_MaxHealth);
        //Debug.Log(m_CurrentHealth);
    }
}
