using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public static HealthController instance;

    public int maxhp, hp;

    public float invincibleLength = 1f;
    private float invincCounter;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;

        UIController.instance.healthSlider.maxValue = maxhp;
        UIController.instance.healthSlider.value = hp;
        UIController.instance.healthText.text = "HEALTH: " + hp + "/" + maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invincCounter <= 0 && !GameManager.instance.ending)
        {
            //Audio.instance.PlaySFX(7);
            hp -= damageAmount;

            UIController.instance.FlashDmg();

            if (hp <= 0)
            {
                gameObject.SetActive(false);

                hp = 0;

                GameManager.instance.Death();
                Audio.instance.stopBGM();
                Audio.instance.PlaySFX(6);
                Audio.instance.StopSFX(7);


            
            
            
            }



            invincCounter = invincibleLength;



            UIController.instance.healthSlider.value = hp;
            UIController.instance.healthText.text = "HEALTH: " + hp + "/" + maxhp;
        }
    }

    public void HealPlayer(int healAmount)
    {
        hp += healAmount;

        if (hp > maxhp)
        {
            hp = maxhp;
        }
        UIController.instance.healthSlider.value = hp;
        UIController.instance.healthText.text = "HEALTH: " + hp + "/" + maxhp;
    }

    public void Heal(int healAmount)
    {
        hp += healAmount;
        if(hp > maxhp)
        {
            hp = maxhp;
        }
        UIController.instance.healthSlider.value = hp;
        UIController.instance.healthText.text = "HEALTH: " + hp + "/" + maxhp;
    }
}
