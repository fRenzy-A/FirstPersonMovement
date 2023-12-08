using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstPersonController : MonoBehaviour
{
    // Start is called before the first frame update

    CharacterController characterController;

    float horizontalMovement;
    float verticalMovemement;

    [Header("Movement")]
    public float movespeed;
    public float sprintMultiplier;


    public float currentSpeed;
    [Header("Keybinds")]
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Looking")]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    Camera cam;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    float playerHeight = 2f;
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentSpeed = movespeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        CharacterMoving();
        CharacterLooking();
        if (Input.GetKeyDown(sprintKey))
        {
            currentSpeed = currentSpeed * sprintMultiplier;
        }
        else if (Input.GetKeyUp(sprintKey))
        {
            currentSpeed = movespeed;
        }
    }

    void CharacterMoving()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovemement = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.forward * verticalMovemement + transform.right * horizontalMovement;

        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
    }

    public void CharacterLooking()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");


        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    /*public void Looking()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MyInput();

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

    }
    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }*/

    /*void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovemement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovemement + transform.right * horizontalMovement;
    }*/
}
