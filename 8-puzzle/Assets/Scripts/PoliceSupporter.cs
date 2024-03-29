﻿using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSupporter : SupporterBehavior
{
    [SerializeField]
    private Transform cameraTransform;

    public float wingRpm = 10000f;
    public Transform[] wingOrdered;
    public Transform[] wingReversed;

    // Start is called before the first frame update
    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsOwner)
        {
            //PlayerPrefs.SetInt("UnitySelectMonitor", 3);
            //Display.displays[2].Activate();
        }
        else
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
            GetComponent<DroneMovement>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float angle = wingRpm * 6f * Time.deltaTime;
        for(int i = 0; i < wingOrdered.Length; i++)
        {
            wingOrdered[i].Rotate(Vector3.forward, -angle);
        }
        for(int i = 0; i < wingReversed.Length; i++)
        {
            wingReversed[i].Rotate(Vector3.forward, angle);
        }

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

    public override void ChatToPolice(RpcArgs args)
    {
        // TODO
        throw new System.NotImplementedException();
    }

    public override void ChatToThief(RpcArgs args)
    {
        // Do nothing
    }

    /*
    public override void Chat(RpcArgs args)
    {
        int id = args.GetNext<int>();
        if(networkObject.IsOwner && id == 1)
        {
            string message = args.GetNext<string>();
            GameManager.instance.ReceiveMessage(message);
        }
    }
    */
}
