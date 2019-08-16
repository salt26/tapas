using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject thief;

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (NetworkManager.Instance.IsServer && other.gameObject == thief && GameManager.instance.Win_TeamID == 0)
        {
            GameManager.instance.Win_TeamID = 2;
        }
    }
}
