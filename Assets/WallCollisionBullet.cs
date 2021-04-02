using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisionBullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        Projectile bullet = other.gameObject.GetComponent<Projectile>();
        PlayerPlatformerController player = other.gameObject.GetComponent<PlayerPlatformerController>();
        Debug.Log(player);
        if(bullet){
            bullet.hit();
        }
    }
}
