using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : ItemBehavior
{

    private void OnTriggerEnter(Collider other)
    {
        if (NetworkManager.Instance == null)
        {
            return;
        }
        else if (NetworkManager.Instance.IsServer && other != null)
        {
            if (other.tag.Equals("Thief"))
            {
                Debug.Log("Thief stepped on wire");
                other.GetComponent<Thief>().networkObject.SendRpc(ThiefBehavior.RPC_SLOW, Receivers.All);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (NetworkManager.Instance == null)
        {
            return;
        }
        else if (NetworkManager.Instance.IsServer && other != null)
        {
            if (other.tag.Equals("Thief"))
            {
                other.GetComponent<Thief>().networkObject.SendRpc(ThiefBehavior.RPC_RESTORE, Receivers.All);
            }
        }
    }

    public override void DestroyIt(RpcArgs args)
    {

    }

    public override void AlertOff(RpcArgs args)
    {

    }
}
