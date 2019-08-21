using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefSupporter : SupporterBehavior
{
    private bool canChat = false;

    [SerializeField]
    private Transform cameraTransform;

    // Start is called before the first frame update
    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsOwner)
        {
            //PlayerPrefs.SetInt("UnitySelectMonitor", 4);
            //Display.displays[3].Activate();
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
        if (networkObject.IsOwner)
        {
            networkObject.position = transform.position;
            networkObject.droneRotation = transform.rotation;
            networkObject.cameraRotation = cameraTransform.rotation;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (canChat)
                {
                    GameManager.instance.SendPlayersMessage(1);
                    GameManager.instance.chatUI.alpha = 0.5f;
                    canChat = false;
                }
                else
                {
                    GameManager.instance.chatUI.alpha = 1f;
                    canChat = true;
                    GameManager.instance.chatInputBox.ActivateInputField();
                }
            }

        }
        else
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.droneRotation;
            cameraTransform.rotation = networkObject.cameraRotation;
        }
    }

    public override void ChatToThief(RpcArgs args)
    {
        // TODO
        throw new System.NotImplementedException();
    }

    public override void ChatToPolice(RpcArgs args)
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
