using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerMovement와 유사한 코드
// Superclass?

public class DroneMovement : MonoBehaviour
{
    [Header("Drone Settings")]
    public float forceMagnitude = 12f;
    public float dragMagnitude = 4f;
    public float tiltTimeScale = .5f;
    public float maxTiltAngle = 20f;

    [Header("FOV Settings")]
    public float mouseSensitivity = 60f;
    public float minAngle = -70f;
    public float maxAngle = 70f;
    public Vector3 CameraOffset;

    [Header("Mesh Settings")]
    public Transform cameraModel;

    public int teamID;

    private Rigidbody m_Rigidbody;

    private Camera m_Camera;
    private Vector2 angles;
    private Vector3 tiltAxis;

    private float horizontalInput;
    private float verticalInput;
    private float mouseX;
    private float mouseY;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Camera = GetComponentInChildren<Camera>();
        m_Camera.transform.position = cameraModel.position;

        m_Rigidbody.drag = dragMagnitude;

        // initialization
        angles.x = 0f;
        angles.y = 0f;
        tiltAxis.x = 0f;
        tiltAxis.y = 0f;
        tiltAxis.z = 0f;

        GameManager.instance.droneMovements.Add(this);
    }

    void Update()
    {
        // inputs for motion
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // inputs for camera
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        angles.x += mouseY * Time.deltaTime;
        angles.y += mouseX * Time.deltaTime;
        angles.x = Mathf.Clamp(angles.x, minAngle, maxAngle);
        float weight = Mathf.Exp(-Time.deltaTime / tiltTimeScale);
        Vector3 movement = new Vector3(verticalInput, 0, -horizontalInput);
        if (movement.magnitude > 1f) movement.Normalize();
        tiltAxis = tiltAxis * weight + (1f - weight) * movement;

        transform.rotation = Quaternion.AngleAxis(angles.y, Vector3.up)
            * Quaternion.AngleAxis(tiltAxis.magnitude * maxTiltAngle, tiltAxis);
        cameraModel.transform.localEulerAngles = new Vector3(-angles.x + 270f, 0, 0);
        m_Camera.transform.localEulerAngles = new Vector3(-angles.x, 0, 0);
    }

    void FixedUpdate()
    {
        bool hasInput = !Mathf.Approximately(horizontalInput, 0f) || !Mathf.Approximately(verticalInput, 0f);
        if (hasInput)
        {
            Vector3 force = new Vector3(horizontalInput, 0, verticalInput);
            force.Normalize();
            force *= forceMagnitude;
            m_Rigidbody.AddForce(Quaternion.Euler(0f, angles.y, 0f) * force);
        }
    }
}
