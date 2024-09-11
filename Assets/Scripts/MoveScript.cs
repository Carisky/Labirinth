using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

    public float moveSpeed = 5f;

    public float lookSpeed = 2f;

    public float upperLookLimit = 10f;
    public float lowerLookLimit = -20f;


    private float xRotation = 0f;


    public Transform playerCamera;


    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {

        if (PauseMenu.isPaused)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


        xRotation -= mouseY * lookSpeed;
        xRotation = Mathf.Clamp(xRotation, lowerLookLimit, upperLookLimit);


        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


        Vector3 forward = playerCamera.forward;
        Vector3 right = playerCamera.right;


        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();


        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;


        transform.Translate(moveSpeed * Time.deltaTime * moveDirection, Space.World);


        transform.Rotate(lookSpeed * mouseX * Vector3.up);
    }
}
