using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CharacterMover : PlayerBehavior
{
    public float moveSpeed;
    //public DroneMover d;
    public Camera head;

    GameObject quitText;
    CharacterController character;
    CollisionFlags collisionFlags;
    Vector3 movement;
    MouseLook mouseLook = new MouseLook();
    /*
    bool isControlling = true;
    bool isAcquiredInstantly = false;
    */

    protected override void NetworkStart()
    {
        base.NetworkStart();
        //head = GetComponentInChildren<Camera>();
        character = GetComponent<CharacterController>();
        quitText = GameObject.Find("QuitText");

        if (!networkObject.IsOwner)
        {
            head.gameObject.SetActive(false);
        }
        mouseLook.Init(GetComponent<Transform>(), head.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (networkObject == null) return;
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            return;
        }
        /*
        if (!isControlling)
        {
            movement = new Vector3(0f, 0f, 0f);
            character.Move(movement);
            return;
        }
        else if (isAcquiredInstantly)
        {
            isAcquiredInstantly = false;
            movement = new Vector3(0f, 0f, 0f);
            character.Move(movement);
            return;
        }
        */

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        // 이동
        Moving(v, h);
        mouseLook.LookRotation(GetComponent<Transform>(), head.transform);
        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NetworkManager.Instance.Disconnect();
            quitText.GetComponent<Text>().text = "종료하는 중...";
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    void Moving(float v, float h = 0f)
    {
        // X축, Z축 방향의 속도
        movement = GetComponent<Transform>().forward * v + GetComponent<Transform>().right * h;
        if (movement.magnitude > 1f) movement = movement.normalized;
        movement *= moveSpeed;                  // 속도는 앞에서 변환했음
        if (v < 0f) movement /= 2f;                 // 뒷걸음질 칠 때

        // Y축 아래 방향으로 중력 작용
        if (character.isGrounded) movement += Physics.gravity * Time.fixedDeltaTime;
        else movement += Physics.gravity * Time.fixedDeltaTime;

        // 이동, 충돌 감지
        collisionFlags = character.Move(movement * Time.fixedDeltaTime);
    }

    /*
    /// <summary>
    /// 플레이어 캐릭터가 조종권을 가져갑니다.
    /// </summary>
    public void AcquireControl()
    {
        //Debug.Log("Character Acquire");
        isControlling = true;
        isAcquiredInstantly = true;
        head.gameObject.SetActive(true);
    }

    /// <summary>
    /// 드론에게 조종권을 넘겨줍니다.
    /// </summary>
    public void ReleaseControl()
    {
        //Debug.Log("Character Release");
        isControlling = false;
        d.AcquireControl();
        head.gameObject.SetActive(false);
    }
    */
    
    public MouseLook GetMouseLook()
    {
        return mouseLook;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!networkObject.IsOwner) return;
        Rigidbody body = hit.collider.attachedRigidbody;
        // Don't move the rigidbody if the character is on top of it
        if (collisionFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(character.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }

}
