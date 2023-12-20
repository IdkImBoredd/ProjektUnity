using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public static PlayerController instance;
    public float movementSpeed, gravityModifier, jump, sprintSpeed = 12f;
    public CharacterController charCon;
    private Vector3 moveInput;
    public Transform camMov;
    public float mouseSensi;
    public bool invertX, invertY;
    public Transform fireLoc;
    private bool canJump, doubleJump;
    public Transform groundCheck;
    public LayerMask Ground;
    public Animator animation;
    public Gun activeGun;
    public List<Gun> gunList = new List<Gun>();
    public int currentGun;
    public Transform aimPoint, gunSpace;
    public Vector3 gunStartPos;
    public float aimSpeed = 2f;
    public List<Gun> unlockableGuns = new List<Gun>();
    public GameObject Muzzle;
    public AudioSource footsteptFast, footstepSlow;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentGun--;
        SwitchGun();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIController.instance.pause.activeInHierarchy && !GameManager.instance.ending)
        {
            float yStore = moveInput.y;

            Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
            Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

            moveInput = horiMove + vertMove;
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveInput = moveInput * sprintSpeed;
            }
            else
            {
                moveInput = moveInput * movementSpeed;
            }


            moveInput.y = yStore;

            moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

            if (charCon.isGrounded)
            {
                moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
            }
            canJump = Physics.OverlapSphere(groundCheck.position, .25f, Ground).Length > 0;


            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                moveInput.y = jump;

                doubleJump = true;

                Audio.instance.PlaySFX(8);
            }
            else if (doubleJump && Input.GetKeyDown(KeyCode.Space))
            {
                moveInput.y = jump;

                doubleJump = false;
            }

            charCon.Move(moveInput * Time.deltaTime);




            Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensi;

            if (invertX)
            {
                mouseInput.x = -mouseInput.x;
            };
            if (invertY)
            {
                mouseInput.y = -mouseInput.y;
            };

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

            camMov.rotation = Quaternion.Euler(camMov.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

            Muzzle.SetActive(false);
            if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(camMov.position, camMov.forward, out hit, 50f))
                {
                    fireLoc.LookAt(hit.point);
                }
                else
                {
                    fireLoc.LookAt(camMov.position + (camMov.forward * 30f));
                }

                FireShot();
            }
            if (Input.GetMouseButton(0) && activeGun.canAutoFire)
            {
                if (activeGun.fireCounter <= 0)
                {
                    FireShot();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                CameraController.instance.ZoomIn(activeGun.zoom);
            }

            if (Input.GetMouseButtonUp(1))
            {
                CameraController.instance.ZoomOut();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchGun();
            }


            animation.SetFloat("movementSpeed", moveInput.magnitude);
            animation.SetBool("onGround", canJump);
        }
    }
    public void FireShot()
    {
        if (activeGun.currentAmmo > 0)
        {

            activeGun.currentAmmo--;
            Instantiate(activeGun.bullet, fireLoc.position, fireLoc.rotation);

            activeGun.fireCounter = activeGun.fireRate;
            UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmo;

            Muzzle.SetActive(true);
        }
    }
    public void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);
        
        currentGun++;

        if(currentGun >= gunList.Count) 
        {
            currentGun = 0;

        }

        activeGun = gunList[currentGun];
        activeGun.gameObject.SetActive(true);

        UIController.instance.ammoText.text = "AMMO: " + activeGun.currentAmmo;

        fireLoc.position = activeGun.bulletloc.position;
    }
    public void AddGun(string gunToAdd)
    {
        bool gunUnlocked = false;

        if(unlockableGuns.Count > 0)
        {
            for(int i = 0; i < unlockableGuns.Count; i++)
            {
                if (unlockableGuns[i].gunName == gunToAdd)
                {
                    gunUnlocked = true;
                    gunList.Add(unlockableGuns[i]);
                    unlockableGuns.RemoveAt(i);

                    i = unlockableGuns.Count;
                }
            }
       
        }
    if(gunUnlocked)
        {
            currentGun = gunList.Count - 2;
            SwitchGun();
        }
    }
}
