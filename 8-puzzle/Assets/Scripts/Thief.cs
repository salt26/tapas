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
            //PlayerPrefs.SetInt("UnitySelectMonitor", 2);
            //Display.displays[1].Activate();
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
            
            if (Input.GetMouseButtonDown(0))
            {
                networkObject.SendRpc(
                    RPC_PRESS_SWITCH,
                    Receivers.Server
                );
            }
        }
        else
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
        }
    }

    public override void PressSwitch(RpcArgs args)
    {
        if (!NetworkManager.Instance.IsServer) return;
        GetComponentInChildren<PlayerTouch>().Touch();
        // TODO: 누른 스위치가 모든 클라이언트에서 보이도록 하기
    }

    public override void Chat(RpcArgs args)
    {
        // TODO
        throw new System.NotImplementedException();
    }
}
