using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float lifeTime,speed;

    public Rigidbody rb;
    public GameObject impactEffect;
    public int damage = 10;

    public bool dmgEnemy, dmgPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;

        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy" && dmgEnemy)
        {
            other.gameObject.GetComponent<HealthController_Enemy>().Damage(damage);
        }

        if(other.gameObject.tag == "Player" && dmgPlayer)
        {
            HealthController.instance.DamagePlayer(damage);

        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
