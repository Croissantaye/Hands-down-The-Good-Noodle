using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        PlayerPlatformerController player = other.gameObject.GetComponent<PlayerPlatformerController>();
        if(player){
            player.Hurt();
            hit();
        }
        else{
            hit();
        }
    }

    public override bool IsEnemyProjectile()
    {
        return true;
    }
}
