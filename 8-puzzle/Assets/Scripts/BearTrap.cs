using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
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
                Debug.Log("Thief stepped on beartrap");
                other.GetComponent<Thief>().networkObject.SendRpc(ThiefBehavior.RPC_STOP, Receivers.All);
            }
        }
    }
}
