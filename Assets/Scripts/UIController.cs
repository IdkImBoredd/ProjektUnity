using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public Text healthText, ammoText;

    public Image hitEffect;
    public float dmgAlpha = .30f, Fade = 2f;
    public GameObject pause;
    public Image fade;
    public float fadeSpeed = 2f;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(hitEffect.color.a != 0)
        {
            hitEffect.color = new Color(hitEffect.color.r, hitEffect.color.g, hitEffect.color.b, Mathf.MoveTowards(hitEffect.color.a, 0f, Fade * Time.deltaTime));
        }

        if (!GameManager.instance.ending)
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, Mathf.MoveTowards(fade.color.a, 0f, fadeSpeed * Time.deltaTime));
        }
        else
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, Mathf.MoveTowards(fade.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
    }

    public void FlashDmg()
    {
        hitEffect.color = new Color(hitEffect.color.r, hitEffect.color.g, hitEffect.color.b, .30f);
    }
}
