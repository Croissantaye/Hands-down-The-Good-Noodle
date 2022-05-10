using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPlatformerController player = other.gameObject.GetComponent<PlayerPlatformerController>();
        if(player){
            player.hurt();
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

    public override void Setup(Vector3 dir, float s){
        speed = s;
        this.direction = dir;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(direction));
        Destroy(gameObject, 3f);
    }
}
