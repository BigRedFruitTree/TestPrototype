using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public GameObject PlayerObject;
    private Rigidbody myRB;
    public Camera playerCam;
    Transform cameraHolder;

    Vector2 camRotation;
    public Transform weaponSlot;

    private LinkedList weaponList;

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
   
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        playerCam = Camera.main;
        cameraHolder = transform.GetChild(0);
        camRotation = Vector2.zero;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weaponList = new LinkedList();
        weaponList.Main(1);
        weaponList.Main(2);
        weaponList.Main(3);
        melee.gameObject.transform.SetPositionAndRotation(weaponSlot.position, weaponSlot.rotation);
        melee.gameObject.transform.SetParent(weaponSlot);
        ranged.gameObject.transform.SetPositionAndRotation(weaponSlot.position, weaponSlot.rotation);
        ranged.gameObject.transform.SetParent(weaponSlot);
        sheild.gameObject.transform.SetPositionAndRotation(weaponSlot.position, weaponSlot.rotation);
        sheild.gameObject.transform.SetParent(weaponSlot);
        CycleWeapons();
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            if (stamina > 0)
            {

                sprintMode = true;
            }
            else
                sprintMode = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprintMode = false;
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
            temp.y = jumpHeight;
            jumps--;
        }

        myRB.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);

        if (health < 0)
            health = 0;

        if (health == 0)
            GameOver = true;

    }

    public void SetWeapon()
    {
        Node first = new Node(1);
        first.next = new Node(2);
        first.next.next = new Node(3);
        Node last = first.next.next;
        last.next = first;



        if (weaponList.Search(last, 1) == true)
        {
            melee.SetActive(true);
            StartCoroutine("Wait2");
           melee.SetActive(false);
            Debug.Log("weapon 1");
        }
        if (weaponList.Search(last, 2) == true)
        {
            ranged.SetActive(true);
            StartCoroutine("Wait2");
            ranged.SetActive(false);
            Debug.Log("weapon 2");
        }
        if (weaponList.Search(last, 3) == true)
        {
            sheild.SetActive(true);
            StartCoroutine("Wait2");
            sheild.SetActive(false);
            Debug.Log("weapon 3");
        }
    }

    public void CycleWeapons()
    {
       StartCoroutine("Wait");
       SetWeapon();
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

    IEnumerator Wait()
    {
        Debug.Log("YAY");
        yield return new WaitForSeconds(1);
    }

    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(5);
    }

    IEnumerator Wait3()
    {
        yield return new WaitForSeconds(5);
    }
}