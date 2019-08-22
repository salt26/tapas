using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : PoliceBehavior
{
    public Transform myCamera;
    public List<AudioClip> footsteps;
    private Animator m_Animator;
    private bool isNetworkReady = false;
    private int[] itemNum;
    private Vector3 lastPosition;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsOwner)
        {
            float m = GameManager.instance.maze.localScale.x;
            transform.rotation = Quaternion.FromToRotation(transform.rotation * new Vector3(0f, 0f, 0f), new Vector3(53.3f * m, 0f, 53.3f * m) - transform.position);

            //PlayerPrefs.SetInt("UnitySelectMonitor", 1);
            //Display.displays[0].Activate();
        }
        else
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
        }
        lastPosition = transform.position;
        isNetworkReady = true;
    }

    void Start()
    {
        itemNum = new int[3] { 1, 1, 1 };
    }

    void Update()
    {
        if (!isNetworkReady) return;

        if (networkObject.IsOwner)
        {
            networkObject.position = transform.position;
            networkObject.rotation = transform.rotation;
            networkObject.cameraRotation = myCamera.rotation;
            networkObject.mHorizontal = GetComponent<PlayerMovement>().CurrentMovement.x;
            networkObject.mVertical = GetComponent<PlayerMovement>().CurrentMovement.z;
            networkObject.isRotatingLeft = GetComponent<PlayerMovement>().IsRotatingLeft;
            networkObject.isRotatingRight = GetComponent<PlayerMovement>().IsRotatingRight;
            
            if(!GameManager.instance.canChat)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    networkObject.SendRpc(RPC_TOUCH, Receivers.Server);
                }
                if (itemNum[0] > 0 && Input.GetKeyDown(KeyCode.Alpha1))
                {
                    itemNum[0]--;
                    networkObject.SendRpc(RPC_USE_ITEM, Receivers.Server, 1);
                }
                if (itemNum[1] > 0 && Input.GetKeyDown(KeyCode.Alpha2))
                {
                    itemNum[1]--;
                    networkObject.SendRpc(RPC_USE_ITEM, Receivers.Server, 2);
                }
                if (itemNum[2] > 0 && Input.GetKeyDown(KeyCode.Alpha3))
                {
                    itemNum[2]--;
                    networkObject.SendRpc(RPC_USE_ITEM, Receivers.Server, 3);
                }
                GetComponent<PlayerMovement>().walkingSpeed = 2.6f; //change these if you change default speed
                GetComponent<PlayerMovement>().runningSpeed = 4.2f;
            }
            
            GameManager.instance.item1Txt.text = itemNum[0].ToString();
            GameManager.instance.item2Txt.text = itemNum[1].ToString();
            GameManager.instance.item3Txt.text = itemNum[2].ToString();

            if(GameManager.instance.canChat)
            {
                GetComponent<PlayerMovement>().walkingSpeed = 0f;
                GetComponent<PlayerMovement>().runningSpeed = 0f;
            }
        }
        else
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            myCamera.rotation = networkObject.cameraRotation;
            m_Animator.SetFloat("Horizontal", networkObject.mHorizontal);
            m_Animator.SetFloat("Vertical", networkObject.mVertical);
            m_Animator.SetBool("RotateLeft", networkObject.isRotatingLeft);
            m_Animator.SetBool("RotateRight", networkObject.isRotatingRight);
        }
        Vector3 diff = transform.position - lastPosition;
        if (!(Mathf.Approximately(diff.x, 0f) && Mathf.Approximately(diff.z, 0f)) && !GetComponent<AudioSource>().isPlaying)
        {
            int index = Random.Range(0, footsteps.Count - 1);
            GetComponent<AudioSource>().clip = footsteps[index];
            GetComponent<AudioSource>().Play();
            footsteps.Add(footsteps[index]);
            footsteps.RemoveAt(index);
        }
        lastPosition = transform.position;
    }

    public override void Touch(RpcArgs args)
    {
        if (!NetworkManager.Instance.IsServer) return;
        //BMSDebug.Log("MouseClick");
        foreach (PlayerTouch pt in GetComponents<PlayerTouch>())
        {
            pt.Touch();
        }
    }

    public override void OpenBox(RpcArgs args) 
    {
        if (!networkObject.IsOwner) return; // only Police can modify itemNum variables

        int i = Random.Range(0, 3);

        itemNum[i]++;
    }

    public override void UseItem(RpcArgs args) // Police already validated itemNum
    {
        if (!NetworkManager.Instance.IsServer) return;  // Server will instantiate items

        int i = args.GetNext<int>(); // 1: Wire / 2: Trap / 3: Alert

        //Debug.Log("Used Item" + i);

        Quaternion rot;

        if (i == 1)
        {
            rot = Quaternion.Euler(-90f, 0f, 0f);
        }
        else
        {
            rot = Quaternion.identity;
        }

        NetworkManager.Instance.InstantiateItem(i - 1, transform.position, rot);
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
