using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject playerView;
    Vector2 rot = Vector2.zero;
    Rigidbody rb;
    public float sensitivity = 10f;
    public float speed = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // view
        rot.y += Input.GetAxis("Mouse X") * sensitivity;

        transform.eulerAngles = new Vector2(0, rot.y);

        rot.x += -Input.GetAxis("Mouse Y") * sensitivity;
        if (rot.x < -89) { rot.x = -89; }
        if (rot.x > 89) { rot.x = 89; }
        
        playerView.transform.eulerAngles = rot;

        // move
        rb.velocity = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")) * speed;

        //
        if (Input.GetMouseButtonDown(1))
        {
            GameManager.instance.readHistory();
        }
    }
}
