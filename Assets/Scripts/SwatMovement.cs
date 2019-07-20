using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatMovement : MonoBehaviour
{

    public float turnSpeed = 5f;
    public Camera mainCamera;

    int m_AnimationState;
    Animator m_Animator;
    Vector3 m_Movement;
    Rigidbody m_Rigidbody;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        //bool isMoving = hasHorizontalInput || hasVerticalInput;

        int horizontalState = !hasHorizontalInput ? 1 : (horizontal > 0 ? 2 : 0);
        int verticalState = !hasVerticalInput ? 1 : (vertical > 0 ? 2 : 0);
        m_AnimationState = verticalState * 3 + horizontalState;
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && m_AnimationState == 7;

        //m_Movement.Set(horizontal, 0f, vertical);
        //m_Movement.Normalize();
        float angle = Mathf.Deg2Rad * mainCamera.transform.eulerAngles.y;
        m_Movement = Vector3.RotateTowards(transform.forward, 
            new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)), turnSpeed * Time.fixedDeltaTime, 0f);

        float deltaAngle = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, mainCamera.transform.eulerAngles.y);
        bool needTurnLeft = deltaAngle < -30;
        bool needTurnRight = deltaAngle > 30;


        m_Animator.SetInteger("AnimationState", m_AnimationState);
        m_Animator.SetBool("IsSprinting", isSprinting);
        m_Animator.SetBool("NeedTurnLeft", needTurnLeft);
        m_Animator.SetBool("NeedTurnRight", needTurnRight);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Animator.rootPosition);
        if (m_AnimationState == 4)
        {
            m_Rigidbody.MoveRotation(m_Animator.rootRotation);
        }
        else
        {
            m_Rigidbody.MoveRotation(Quaternion.LookRotation(m_Movement));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hello");
    }



    void OnAnimatorIK(int layerIndex)
    {
        m_Animator.SetLookAtWeight(.5f);
        m_Animator.SetLookAtPosition(transform.position + mainCamera.transform.forward * 1000f);
        //m_Animator.SetIKRotation(AvatarIKGoal.Left, Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0));
    }
}
