using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform focus;
    [SerializeField] private float camSpeed = 6.0f;

    [SerializeField] private float minClampX = -40.0f;
    [SerializeField] private float maxClampX = 60.0f;

    private Vector3 currentRotation;
    private Camera cam;
    private float camDistance;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentRotation = focus.eulerAngles;

        cam = Camera.main;
        camDistance = 5.0f;
    }

    // Update is called once per frame
    void Update()
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
}
