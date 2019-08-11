using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : EnemyBehavior
{
    public Image statusUI;

    protected override void NetworkStart()
    {
        base.NetworkStart();

        if (!NetworkManager.Instance.IsServer) return;
        networkObject.isTriggered = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!NetworkManager.Instance.IsServer) return;

        if (!networkObject.isTriggered && other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Enemy Triggered");
            networkObject.isTriggered = true;
            networkObject.SendRpc(
                EnemyBehavior.RPC_CAUGHT,
                Receivers.All
            );
        }
    }
    
    public override void Caught(RpcArgs args)
    {
        statusUI.color = new Color(
            statusUI.color.r,
            statusUI.color.g,
            statusUI.color.b,
            0.2f
        );
        StatusText.st.Decrease();
        GetComponent<AudioSource>().Play();
    }
}
