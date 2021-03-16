using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private Animator anim;

    [SerializeField] private float speed = 3.0f;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("HorizontalMovement");
        float moveZ = Input.GetAxisRaw("VerticalMovement");
        Vector3 movement = new Vector3(moveX, 0.0f, moveZ);

        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0.0f;

        if (movement.magnitude > 0.1f)
        {
            controller.Move(movement * speed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.Lerp(model.rotation, Quaternion.LookRotation(movement), 10.0f * Time.deltaTime);
            model.rotation = targetRotation;
        }

        anim.SetFloat("speed", movement.magnitude);
    }
}
