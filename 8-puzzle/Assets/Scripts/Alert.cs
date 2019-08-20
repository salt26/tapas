using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : ItemBehavior
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
                Debug.Log("Thief stepped on alert");
                networkObject.SendRpc(RPC_DESTROY_IT, Receivers.All);
            }
        }
    }

    public override void DestroyIt(RpcArgs args)
    {
        Destroy(this.gameObject);
    }
}
