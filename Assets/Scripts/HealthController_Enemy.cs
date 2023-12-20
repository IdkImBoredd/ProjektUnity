using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController_Enemy : MonoBehaviour
{

    public int health = 5, maxHealth = 5;
    public EnemyController EC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int dmgAmount) 
    {
        health = health - dmgAmount;

        if(EC != null)
        {
            EC.GetShot();
        }

        if(health <= 0)
        {
            Destroy(gameObject);
            Audio.instance.PlaySFX(2);
        }
    }    
}
