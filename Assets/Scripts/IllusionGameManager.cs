using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Instance.InstantiatePlayer(/*position: new Vector3(0f, 0.5f, 0f), rotation: Quaternion.Euler(0f, 270f, 0f)*/);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
