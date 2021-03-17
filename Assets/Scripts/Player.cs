using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IsAttackable
{
    [Header("Movement Fields")]
    [SerializeField] private Transform model;
    [SerializeField] private Animator anim;

    [SerializeField] private float speed = 3.0f;
    private CharacterController controller;


    [Header("Camera Fields")]
    [SerializeField] private Transform focus;
    [SerializeField] private float camSpeed = 6.0f;

    [SerializeField] private float minClampX = -40.0f;
    [SerializeField] private float maxClampX = 60.0f;

    private Vector3 currentRotation;
    private Camera cam;
    private float camDistance;

    [Header("Combat Fields")]
    [SerializeField] private float maxHP = 100.0f;
    private float currentHP;

    // Start is called before the first frame update
    void Start()
    {
        // Movement
        controller = GetComponent<CharacterController>();

        // Camera
        Cursor.lockState = CursorLockMode.Locked;
        currentRotation = focus.eulerAngles;

        cam = Camera.main;
        camDistance = 5.0f;

        // Combat
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdateCamera();
    }

    // -------------------------
    // GENERAL MOVEMENT & CAMERA
    // -------------------------
    void Move()
    {
        float moveX = Input.GetAxisRaw("HorizontalMovement");
        float moveZ = Input.GetAxisRaw("VerticalMovement");
        Vector3 movement = new Vector3(moveX, 0.0f, moveZ);

        movement = cam.transform.TransformDirection(movement);
        movement.y = 0.0f;

        if (movement.magnitude > 0.1f)
        {
            controller.Move(movement * speed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.Lerp(model.rotation, Quaternion.LookRotation(movement), 10.0f * Time.deltaTime);
            model.rotation = targetRotation;
        }

        anim.SetFloat("speed", movement.magnitude);
    }

    void UpdateCamera()
    {
        float rotX = Input.GetAxisRaw("VerticalCamera");
        float rotY = Input.GetAxisRaw("HorizontalCamera");

        currentRotation += new Vector3(-rotX, rotY, 0.0f) * camSpeed * Time.deltaTime;
        currentRotation.x = Mathf.Clamp(currentRotation.x, minClampX, maxClampX);
        focus.eulerAngles = currentRotation;

        Vector3 targetLocation = cam.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(focus.transform.position, -cam.transform.forward, out hit, camDistance))
        {
            targetLocation = focus.transform.position - (cam.transform.forward * hit.distance);
        }
        else
        {
            targetLocation = focus.transform.position - (cam.transform.forward * camDistance);
        }

        cam.transform.position = targetLocation;
    }

    // ------
    // COMBAT
    // ------
    public void TakeDamage(float damage)
    {
        Debug.Log("Damage received: " + damage);
        currentHP -= damage;
        Debug.Log("Current HP" + currentHP);
    }
}