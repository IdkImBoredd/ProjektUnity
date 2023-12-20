using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public int healAmount;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" )
        {
            HealthController.instance.Heal(healAmount);

            Destroy(gameObject);

            Audio.instance.PlaySFX(5);
        }
    }
}
