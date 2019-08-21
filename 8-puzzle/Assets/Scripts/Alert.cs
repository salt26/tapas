using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : ItemBehavior
{
    public MeshRenderer laser;
    public AudioSource siren;
    public float alertTime = 10f;

    private float timer = 0f;

    private void Update()
    {
        if (NetworkManager.Instance == null)
        {
            return;
        }
        else if (NetworkManager.Instance.IsServer)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                Debug.Log(timer);
                if (timer <= 0)
                {
                    networkObject.SendRpc(RPC_ALERT_OFF, Receivers.All);
                }
            }
        }
    }

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

    private void OnTriggerStay(Collider other)
    {
        if (NetworkManager.Instance == null)
        {
            return;
        }
        else if (NetworkManager.Instance.IsServer && other != null)
        {
            if (other.tag.Equals("Thief"))
            {
                timer = alertTime;
            }
        }
    }

    public override void DestroyIt(RpcArgs args)
    {
        if(!siren.isPlaying)
        {
            siren.Play();
        }
        laser.enabled = true;
    }

    public override void AlertOff(RpcArgs args)
    {
        siren.Stop();
        laser.enabled = false;
    }
}
