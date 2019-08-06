﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    public float idleSpeed = 0.3f;
    public float walkingSpeed = 1f;
    public float runningSpeed = 2f;
    public float transitionSpeed = 4f;

    [Header("Rotation Settings")]
    public float rotationThreshold = 50f;
    public float rotationSpeed = 90f;

    [Header("FOV Settings")]
    public float mouseSensitivity = 60f;
    public float minAngle = -70f;
    public float maxAngle = 70f;
    public Vector3 CameraOffset;

    private Rigidbody m_Rigidbody;
    private Animator m_Animator;
    private Vector3 currentMovement;

    private Camera m_Camera;
    private Vector2 angles;

    private float horizontalInput;
    private float verticalInput;
    private float mouseX;
    private float mouseY;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        m_Camera = GetComponentInChildren<Camera>();

        // initialization
        angles.x = 0f;
        angles.y = 0f;
        currentMovement.x = 0f;
        currentMovement.y = 0f;
        currentMovement.z = 0f;
    }

    void Update()
    {
        // inputs for motion
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // inputs for camera
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        Vector3 eulerRotation = transform.eulerAngles;
        angles.x += mouseY * Time.deltaTime;
        angles.y += mouseX * Time.deltaTime;
        angles.x = Mathf.Clamp(angles.x, minAngle, maxAngle);
        eulerRotation.x = -angles.x;
        eulerRotation.y = angles.y;
        m_Camera.transform.position = (m_Animator.GetBoneTransform(HumanBodyBones.LeftEye).position
            + m_Animator.GetBoneTransform(HumanBodyBones.RightEye).position) / 2f
            + Quaternion.Euler(0, angles.y, 0) * CameraOffset;
        m_Camera.transform.eulerAngles = eulerRotation;
    }

    void FixedUpdate()
    {
        // animation update
        bool hasHorizontalInput = !Mathf.Approximately(horizontalInput, 0f);
        bool hasVerticalInput = !Mathf.Approximately(verticalInput, 0f);
        bool isMoving = hasHorizontalInput || hasVerticalInput;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving;

        Vector3 targetMovement = new Vector3(horizontalInput, 0, verticalInput);
        targetMovement.Normalize();
        targetMovement *= isMoving ? (isRunning ? runningSpeed : walkingSpeed) : 0f;
        currentMovement = Vector3.MoveTowards(currentMovement, targetMovement, transitionSpeed * Time.fixedDeltaTime);

        float direction = Mathf.DeltaAngle(transform.eulerAngles.y, m_Camera.transform.eulerAngles.y);
        bool isRotatingLeft = direction < -rotationThreshold && currentMovement.magnitude < idleSpeed;
        bool isRotatingRight = direction > rotationThreshold && currentMovement.magnitude < idleSpeed;

        m_Animator.SetFloat("Horizontal", currentMovement.x);
        m_Animator.SetFloat("Vertical", currentMovement.z);
        m_Animator.SetBool("RotateLeft", isRotatingLeft);
        m_Animator.SetBool("RotateRight", isRotatingRight);
    }

    
    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Animator.rootPosition);

        // rotation management
        if (currentMovement.magnitude < idleSpeed)
        {
            m_Rigidbody.MoveRotation(m_Animator.rootRotation);
        }
        else
        {
            float targetAngle = m_Camera.transform.eulerAngles.y;
            float currentAngle = m_Rigidbody.transform.eulerAngles.y + mouseX * Time.fixedDeltaTime;

            // SLOW error correction (without ruining animation)
            float towardAngle = Mathf.DeltaAngle(currentAngle, targetAngle) * 2.0f * Time.fixedDeltaTime;

            m_Rigidbody.MoveRotation(Quaternion.Euler(0f, currentAngle + towardAngle + 
                Mathf.Rad2Deg * m_Animator.angularVelocity.y * Time.fixedDeltaTime, 0f));
        }
    }

    void OnAnimatorIK(int layerIndex)
    {
        // Modify head direction
        m_Animator.SetLookAtWeight(.7f);
        m_Animator.SetLookAtPosition(transform.position + m_Camera.transform.forward * 1000f);
    }
}