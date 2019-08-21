using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : PoliceBehavior
{
    public Transform camera;

    private bool canChat = false;

    // Start is called before the first frame update
    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsOwner)
        {
            float m = GameManager.instance.maze.localScale.x;
            transform.rotation = Quaternion.FromToRotation(transform.rotation * new Vector3(0f, 0f, 0f), new Vector3(53.3f * m, 0f, 53.3f * m) - transform.position);
            networkObject.item1Num = 5;
            networkObject.item2Num = 5;
            networkObject.item3Num = 5;
            //PlayerPrefs.SetInt("UnitySelectMonitor", 1);
            //Display.displays[0].Activate();
        }
        else
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
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
            networkObject.cameraRotation = camera.rotation;

            if (Input.GetMouseButtonDown(0))
            {
                networkObject.SendRpc(
                    RPC_TOUCH,
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
            GameManager.instance.item1Txt.text = networkObject.item1Num.ToString();
            GameManager.instance.item2Txt.text = networkObject.item2Num.ToString();
            GameManager.instance.item3Txt.text = networkObject.item3Num.ToString();

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
            transform.rotation = networkObject.rotation;
            camera.rotation = networkObject.cameraRotation;
        }
    }

    public override void Touch(RpcArgs args)
    {
        if (!NetworkManager.Instance.IsServer) return;
        Debug.Log("MouseClick");
        GetComponentInChildren<PlayerTouch>().Touch();
    }

    public override void OpenBox(RpcArgs args) 
    {
        if (!networkObject.IsOwner) return;
        int NumbertoIncrease = Random.Range(1, 3);
        if(NumbertoIncrease == 1) {
            networkObject.item1Num++;
        } else if(NumbertoIncrease == 2) {
            networkObject.item2Num++;
        } else if(NumbertoIncrease == 3) {
            networkObject.item3Num++;
        }
    }

    public override void UseItem(RpcArgs args)
    {
        if (!NetworkManager.Instance.IsServer) return;
        int i = args.GetNext<int>();
        if(networkObject.item1Num > 0 && i == 1)
        {
            // Wire
            Debug.Log("Used Item1");
            NetworkManager.Instance.InstantiateItem(i - 1, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            networkObject.item1Num--;
        }
        else if(networkObject.item2Num > 0 && i == 2)
        {
            // BearTrap
            Debug.Log("Used Item2");
            NetworkManager.Instance.InstantiateItem(i - 1, transform.position, Quaternion.identity);
            networkObject.item2Num--;
        }
        else if(networkObject.item3Num > 0 && i == 3)
        {
            // Alert
            Debug.Log("Used Item3");
            NetworkManager.Instance.InstantiateItem(i - 1, transform.position, Quaternion.identity);
            networkObject.item3Num--;
        }
    }

    public override void Chat(RpcArgs args)
    {
        /*
        int id = args.GetNext<int>();
        if (networkObject.IsOwner && id == 1)
        {
            string message = args.GetNext<string>();
            GameManager.instance.ReceiveMessage(message);
        }
        */
    }
}
