using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : Enemy
{
    //protected override void ChacePlayer() 
   // {
     //   transform.position = Vector2.MoveTowards(transform.position, m_Target.position,movementSpeed * Time.deltaTime);
    //}

    public override void onPlayerSpotted(Transform player)
    {
        base.onPlayerSpotted(player);
    }
    public override void onPlayerLost()
    {
        base.onPlayerLost();
    }

}
