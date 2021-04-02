using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPlatformerController player = other.gameObject.GetComponent<PlayerPlatformerController>();
        if(player){
            player.Hurt();
            hit();
        }
        else if(other.gameObject.name == "Tilemap_LevelMap"){
            hit();
        }
    }

    public override bool IsEnemyProjectile()
    {
        return true;
    }
}
