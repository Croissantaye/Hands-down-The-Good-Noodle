using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthSystem : MonoBehaviour{
    private int health;
    private int maxHealth;

    public HealthSystem(int h){
        health = h;
        maxHealth = h;
    }

    public int getHealth(){
        return health;
    }

    public void setHealth(int h){
        if(health < maxHealth)
            health = h;
    }

    public void damage(int damage){
        health -= damage;
        healthClamp();
    }

    public void heal(int heal){
        health += heal;
        healthClamp();
    }

    public void decrement(){
        health -= 1;
        healthClamp();
    }

    public void increment(){
        health += 1;
        healthClamp();
    }

    private void healthClamp(){
        if(health < 0)
            health = 0;
        if(health > maxHealth)
            health = maxHealth;
    }
}