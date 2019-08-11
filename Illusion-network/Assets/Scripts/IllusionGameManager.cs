using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Unity.Lobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionGameManager : MonoBehaviour
{
    private int m_TeamID = -1;

    void Awake()
    {
        if (LobbyManager.lm != null)
        {
            m_TeamID = LobbyManager.lm.Myself.GetComponent<LobbyPlayerItem>().AssociatedPlayer.TeamID;
            LobbyManager.lm.GetComponent<Canvas>().enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (m_TeamID == 1)
            NetworkManager.Instance.InstantiatePlayer();
        else if (m_TeamID == 2)
            NetworkManager.Instance.InstantiateDrone();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
