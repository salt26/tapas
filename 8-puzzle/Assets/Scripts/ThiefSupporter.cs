﻿using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefSupporter : SupporterBehavior
{
    [SerializeField]
    private Transform cameraTransform;

    // Start is called before the first frame update
    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsOwner)
        {
            //PlayerPrefs.SetInt("UnitySelectMonitor", 4);
            //Display.displays[3].Activate();
        }
        else
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponent<DroneMovement>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (networkObject.IsOwner)
        {
            networkObject.position = transform.position;
            networkObject.droneRotation = transform.rotation;
            networkObject.cameraRotation = cameraTransform.rotation;
        }
        else
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.droneRotation;
            cameraTransform.rotation = networkObject.cameraRotation;
        }
    }

    public override void ChatToThief(RpcArgs args)
    {
        // TODO
        throw new System.NotImplementedException();
    }

    public override void ChatToPolice(RpcArgs args)
    {
        // Do nothing
    }
}
