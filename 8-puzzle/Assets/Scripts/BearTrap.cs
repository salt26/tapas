using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : ItemBehavior
{
    public Transform left;
    public Transform right;

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
                networkObject.SendRpc(RPC_DESTROY_IT, Receivers.All);
            }
        }
    }

    public override void DestroyIt(RpcArgs args)
    {
        StartCoroutine(Activate());
        
    }

    IEnumerator Activate()
    {
        for(int i = 0; i <= 20; i++)
        {
            left.rotation = Quaternion.Slerp(Quaternion.Euler(-90f, 0f, 0f), Quaternion.Euler(-180f, 0f, 0f), i / 20f);
            right.rotation = Quaternion.Slerp(Quaternion.Euler(-90f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f), i / 20f);
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
