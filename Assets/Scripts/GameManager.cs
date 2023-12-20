using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float DeathTime = 2f;
    [HideInInspector]
    public bool ending;

    private void Awake()
    {
        instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }
    public void Death()
    {
        StartCoroutine(DeathCo());
    }

    public IEnumerator DeathCo()
    {
        yield return new WaitForSeconds(DeathTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PauseUnpause()
    {
        if(UIController.instance.pause.activeInHierarchy)
        {
            UIController.instance.pause.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            PlayerController.instance.footsteptFast.Play();
            PlayerController.instance.footstepSlow.Play();
        }
        else
        {
            UIController.instance.pause.SetActive(true);

            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0f;

            PlayerController.instance.footsteptFast.Stop();
            PlayerController.instance.footstepSlow.Stop();
        }
    }
}
