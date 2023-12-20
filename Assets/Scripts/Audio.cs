using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance;

    public AudioSource bgm, victory;

    public AudioSource[] sfx;
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
        
    }
    public void stopBGM()
    {
        bgm.Stop();
    }
    public void PlayVictory()
    {
        stopBGM();
        victory.Play();
    }
    public void PlaySFX(int sfxNumber)
    {
        sfx[sfxNumber].Stop();
        sfx[sfxNumber].Play();
    }

    public void StopSFX(int sfxNumber)
    {
        sfx[sfxNumber].Stop();
    }
}
