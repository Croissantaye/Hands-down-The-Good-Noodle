using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int healthPoints;
    public int HP => healthPoints;
    private int _MaxHealthPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(int maxHP){
        _MaxHealthPoints = maxHP;
        healthPoints = _MaxHealthPoints;
    }

    public void Hit(){
        if(healthPoints > 0){
            healthPoints--;
        }
        else {
            Die();
        }
    }

    public void Hit(int damagePoints){
        if(healthPoints - damagePoints > 0){
            healthPoints -= damagePoints;
        }
        else {
            Die();
        }
    }

    private void Die(){
        Destroy(gameObject);
    }
}
