using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : ThiefBehavior
{
    public Transform camera;
    private Animator m_Animator;

    private float timer_BearTrap = 0f;

    // Start is called before the first frame update
    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsOwner)
        {
            //PlayerPrefs.SetInt("UnitySelectMonitor", 2);
            //Display.displays[1].Activate();
        }
        else
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
            m_Animator=GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (networkObject.IsOwner)
        {
            networkObject.position = transform.position;
            networkObject.rotation = transform.rotation;
            networkObject.cameraRotation = camera.rotation;
            networkObject.mHorizontal = GetComponent<PlayerMovement>().CurrentMovement.x;
            networkObject.mVertical = GetComponent<PlayerMovement>().CurrentMovement.z;
            networkObject.isRotatingLeft = GetComponent<PlayerMovement>().IsRotatingLeft;
            networkObject.isRotatingRight = GetComponent<PlayerMovement>().IsRotatingRight;

            Debug.Log(networkObject.mHorizontal);
            Debug.Log(networkObject.mVertical);
            Debug.Log(networkObject.isRotatingLeft);
            Debug.Log(networkObject.isRotatingRight);

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("MouseClick");
                networkObject.SendRpc(
                    RPC_TOUCH,
                    Receivers.Server
                );
            }

            if(timer_BearTrap > 0)
            {
                timer_BearTrap -= Time.deltaTime;
                GetComponentInChildren<PlayerMovement>().walkingSpeed = 0f;
                GetComponentInChildren<PlayerMovement>().runningSpeed = 0f;
                if (timer_BearTrap <= 0)
                {
                    GetComponentInChildren<PlayerMovement>().walkingSpeed = 2.5f;
                    GetComponentInChildren<PlayerMovement>().runningSpeed = 2.5f;
                }
            }
        }
        else
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            camera.rotation = networkObject.cameraRotation;
            m_Animator.SetFloat("Horizontal", networkObject.mHorizontal);
            m_Animator.SetFloat("Vertical", networkObject.mVertical);
            m_Animator.SetBool("RotateLeft", networkObject.isRotatingLeft);
            m_Animator.SetBool("RotateRight", networkObject.isRotatingRight);
        }
    }

    public override void Touch(RpcArgs args)
    {
        if (!NetworkManager.Instance.IsServer) return;
        BMSLogger.DebugLog("MouseClick");
        GetComponentInChildren<PlayerTouch>().Touch();
    }

    public override void Chat(RpcArgs args)
    {
        /*
        int id = args.GetNext<int>();
        if (networkObject.IsOwner && id == 2)
        {
            string message = args.GetNext<string>();
            GameManager.instance.ReceiveMessage(message);
        }
        */
    }

    public override void Stop(RpcArgs args)
    {
        if (!networkObject.IsOwner) return;
        float t = args.GetNext<float>();
        timer_BearTrap = t;
    }

    public override void Slow(RpcArgs args)
    {
        if (!networkObject.IsOwner) return;
        GetComponentInChildren<PlayerMovement>().walkingSpeed = 1.5f;
        GetComponentInChildren<PlayerMovement>().runningSpeed = 1.5f;
    }

    public override void Restore(RpcArgs args)
    {
        if (!networkObject.IsOwner) return;
        GetComponentInChildren<PlayerMovement>().walkingSpeed = 2.5f;
        GetComponentInChildren<PlayerMovement>().runningSpeed = 2.5f;
    }
}
