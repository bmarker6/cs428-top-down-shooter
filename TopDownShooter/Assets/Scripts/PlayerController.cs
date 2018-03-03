﻿using UnityEngine;

[RequireComponent( (typeof(CharacterController)))]
public class PlayerController : MonoBehaviour
{
    // handling
    public float rotationSpeed = 450f;
    public float walkSpeed = 10;
    public float runSpeed = 16;
    private float acceleration = 5;
	
    // sytstem
    private Quaternion targetRotation;
    private Vector3 currentVelocityModifier;
	
    // components
    public Gun gun;
    private CharacterController controller;
    private Camera cam;

    void Start ()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }
	
    void Update () {
        ControlMouse();

        if (Input.GetButtonDown("Shoot"))
        {
            gun.Shoot();
        }
        else if (Input.GetButton("Shoot"))
        {
            gun.ShootContinuous();
        }
    }
	
    void ControlMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
        targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0, transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        
		
        var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        currentVelocityModifier = Vector3.MoveTowards(currentVelocityModifier, input, acceleration * Time.deltaTime);
        Vector3 motion = currentVelocityModifier;
        motion *= Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1 ? .7f : 1;
        motion *= Input.GetButton("Run") ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;

        controller.Move(motion * Time.deltaTime);
    }

    void ControlWASD()
    {
        var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (input != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y,
                                        rotationSpeed * Time.deltaTime);
        }
        
        currentVelocityModifier = Vector3.MoveTowards(currentVelocityModifier, input, acceleration * Time.deltaTime);
        Vector3 motion = currentVelocityModifier;
        motion *= Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1 ? .7f : 1;
        motion *= Input.GetButton("Run") ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;

        controller.Move(motion * Time.deltaTime);
    }


}