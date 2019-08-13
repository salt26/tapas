using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : PoliceBehavior
{
    // Start is called before the first frame update
    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsOwner)
        {
            PlayerPrefs.SetInt("UnitySelectMonitor", 1);
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

            if (Input.GetMouseButtonDown(0))
            {
                networkObject.SendRpc(
                    RPC_CATCH_THIEF,
                    Receivers.Server
                );
                networkObject.SendRpc(
                    RPC_OPEN_BOX,
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
        }
    }

    public override void CatchThief(RpcArgs args)
    {
        // TODO
        throw new System.NotImplementedException();
    }

    public override void OpenBox(RpcArgs args)
    {
        // TODO
        throw new System.NotImplementedException();
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
