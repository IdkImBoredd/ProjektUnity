using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string theGun;
    private bool collected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.AddGun(theGun);

            Destroy(gameObject);

            collected = true;

            Audio.instance.PlaySFX(4);
        }
    }
}
