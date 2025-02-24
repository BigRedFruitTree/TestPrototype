using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public GameObject PlayerObject;
    private Rigidbody myRB;
    public Camera playerCam;
    Transform cameraHolder;

    Vector2 camRotation;
    public Transform weaponSlot;
    public GrapplingGun grGun;

    private LinkedList weaponList;
    public int timer = 0;

    [Header("Weapons")]
    public GameObject melee;
    public GameObject ranged;
    public GameObject sheild;
    public int weaponID = 1;

    [Header("Player Stats")]
    public int health = 5;
    public int maxHealth = 5;
    public int healthRestore = 5;

    [Header("Movement Settings")]
    public float speed = 10.0f;
    public float sprintMultiplier = 2f;
    public float jumpHeight = 10.0f;
    public float groundDetectDistance = 1.5f;
    public int jumps = 2;
    public int jumpsMax = 2;
    public bool sprintMode = false;
    public bool isGrounded = true;
    public float stamina = 150;
    public float maxStamina = 150;


    [Header("User Settings")]
    public float mouseSensitivity = 2.0f;
    public float Xsensitivity = 2.0f;
    public float Ysensitivity = 2.0f;
    public float camRotationLimit = 90f;
    public bool GameOver = false;

    [Header("Input System")]
    public InputActionAsset playerCntrols;
    public InputHandler inputHandler;
    private InputAction moveAction;
    private bool canPress;
    

   
    // Start is called before the first frame update
    void Start()
    {
        canPress = true;
        moveAction = playerCntrols.FindActionMap("Player").FindAction("Move");
        inputHandler = InputHandler.Instance;
        myRB = GetComponent<Rigidbody>();
        grGun = GameObject.Find("weapon 1").GetComponent<GrapplingGun>();
        playerCam = Camera.main;
        cameraHolder = transform.GetChild(0);
        camRotation = Vector2.zero;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weaponList = new LinkedList();
        weaponList.Main(1);
        melee.gameObject.transform.SetPositionAndRotation(weaponSlot.position, weaponSlot.rotation);
        melee.gameObject.transform.SetParent(weaponSlot);
        ranged.gameObject.transform.SetPositionAndRotation(weaponSlot.position, weaponSlot.rotation);
        ranged.gameObject.transform.SetParent(weaponSlot);
        sheild.gameObject.transform.SetPositionAndRotation(weaponSlot.position, weaponSlot.rotation);
        sheild.gameObject.transform.SetParent(weaponSlot);
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        playerCam.transform.position = cameraHolder.position;

        if (GameOver == false)
        {

            camRotation.x += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
            camRotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

            camRotation.y = Mathf.Clamp(camRotation.y, -camRotationLimit, camRotationLimit);


            playerCam.transform.rotation = Quaternion.Euler(-camRotation.y, camRotation.x, 0);
            transform.localRotation = Quaternion.AngleAxis(camRotation.x, Vector3.up);

        }

        Vector3 temp = myRB.velocity;

        float verticalMove = Input.GetAxisRaw("Vertical");
        float horizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 || inputHandler.SprintTriggered && stamina > 0)
        {
            if (stamina > 0)
            {

                sprintMode = true;
            }
            else
                sprintMode = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || !inputHandler.SprintTriggered)
        {
            sprintMode = false;
        }

        if (Input.GetKeyUp(KeyCode.E) && grGun.isGrappling == false)
        {
            StartCoroutine("Wait");
            weaponList.CycleWeapons();
            ChangeWeapon();
        }

        if (inputHandler.SwitchTriggered && grGun.isGrappling == false && canPress == true)
        {
            canPress = false;
            StartCoroutine("Wait");
            weaponList.CycleWeapons();
            ChangeWeapon();
            canPress = true;
        }

        if (sprintMode == true)
            stamina--;

        if (stamina < 0)
            stamina = 0;

        if (stamina == 0)
            sprintMode = false;

        if (sprintMode == false)
        {
            stamina++;
        }

        if (stamina > maxStamina)
            stamina = maxStamina;

        if (!sprintMode)
            temp.x = verticalMove * speed;

        if (sprintMode)
        {
           temp.x = verticalMove * speed * sprintMultiplier;
           temp.z = horizontalMove * speed * sprintMultiplier;
        } else
        {
            temp.x = verticalMove * speed;
            temp.z = horizontalMove * speed;
        }
            
        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0 && GameOver == false)
        {
            canPress = false;
            temp.y = jumpHeight;
            jumps--;
            StartCoroutine("Wait");
            canPress = true;
        }

        if (inputHandler.JumpTriggered && jumps > 0 && GameOver == false)
        {
            temp.y = jumpHeight;
            jumps--;

        }

        myRB.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);

        if (health < 0)
            health = 0;

        if (health == 0)
            GameOver = true;
    }

    public void ChangeWeapon()
    {
            if(weaponList.Search() == 1)
            {
                melee.SetActive(true);
                ranged.SetActive(false);
                sheild.SetActive(false);
                weaponID = 1;
            }
            else if(weaponList.Search() == 2)
            {
                ranged.SetActive(true);
                melee.SetActive(false);
                sheild.SetActive(false);
                weaponID = 2;
            }
            else if (weaponList.Search() == 3)
            {
                sheild.SetActive(true);
                ranged.SetActive(false);
                melee.SetActive(false);
                weaponID = 3;
            }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumps = jumpsMax;
            isGrounded = true;
        } 
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(10f);
    }
}