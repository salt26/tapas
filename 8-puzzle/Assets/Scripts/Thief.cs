using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : ThiefBehavior
{
    // Start is called before the first frame update
    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsOwner)
        {
            PlayerPrefs.SetInt("UnitySelectMonitor", 2);
        }
        else
        {
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
        }
        else
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
        }
    }

    public override void PressSwitch(RpcArgs args)
    {
        throw new System.NotImplementedException();
    }

    public override void Chat(RpcArgs args)
    {
        throw new System.NotImplementedException();
    }
}
