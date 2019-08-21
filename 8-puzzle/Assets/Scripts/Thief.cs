using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : ThiefBehavior
{
    public Transform camera;

    private float timer_BearTrap = 0f;
    private bool canChat = false;

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
                    GetComponentInChildren<PlayerMovement>().walkingSpeed = 1f;
                    GetComponentInChildren<PlayerMovement>().runningSpeed = 1f;
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (canChat)
                {
                    GameManager.instance.SendPlayersMessage(2);
                    GameManager.instance.chatUI.alpha = 0.5f;
                    canChat = false;
                }
                else
                {
                    GameManager.instance.chatUI.alpha = 1f;
                    canChat = true;
                    GameManager.instance.chatInputBox.ActivateInputField();
                }
            }

        }
        else
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            camera.rotation = networkObject.cameraRotation;
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
        GetComponentInChildren<PlayerMovement>().walkingSpeed = 0.7f;
        GetComponentInChildren<PlayerMovement>().runningSpeed = 0.7f;
    }

    public override void Restore(RpcArgs args)
    {
        if (!networkObject.IsOwner) return;
        GetComponentInChildren<PlayerMovement>().walkingSpeed = 1f;
        GetComponentInChildren<PlayerMovement>().runningSpeed = 1f;
    }
}
