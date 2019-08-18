﻿using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : PoliceBehavior
{
    public Transform camera;

    // Start is called before the first frame update
    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsOwner)
        {
            //PlayerPrefs.SetInt("UnitySelectMonitor", 1);
            //Display.displays[0].Activate();
        }
        else
        {
            GetComponentInChildren<Camera>().enabled = false;
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
                networkObject.SendRpc(
                    RPC_TOUCH,
                    Receivers.Server
                );
            }
            if (networkObject.item1Num > 0 && Input.GetKeyDown(KeyCode.Alpha1))
            {
                networkObject.SendRpc(
                    RPC_USE_ITEM,
                    Receivers.Server,
                    1
                );
            }
            if (networkObject.item2Num > 0 && Input.GetKeyDown(KeyCode.Alpha2))
            {
                networkObject.SendRpc(
                    RPC_USE_ITEM,
                    Receivers.Server,
                    2
                );
            }
            if (networkObject.item3Num > 0 && Input.GetKeyDown(KeyCode.Alpha3))
            {
                networkObject.SendRpc(
                    RPC_USE_ITEM,
                    Receivers.Server,
                    3
                );
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
        // TODO
        if (!NetworkManager.Instance.IsServer) return;
        Debug.Log("MouseClick");
        GetComponentInChildren<PlayerTouch>().Touch();
    }

    public override void UseItem(RpcArgs args)
    {
        // TODO
        throw new System.NotImplementedException();
    }

    public override void Chat(RpcArgs args)
    {
        // TODO
        throw new System.NotImplementedException();
    }
}
