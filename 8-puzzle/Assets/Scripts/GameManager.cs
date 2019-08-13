using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Unity.Lobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
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
            NetworkManager.Instance.InstantiatePolice();
        else if (m_TeamID == 2)
            NetworkManager.Instance.InstantiateThief();
        else if (m_TeamID == 3)
            NetworkManager.Instance.InstantiateSupporter(0);
        else if (m_TeamID == 4)
            NetworkManager.Instance.InstantiateSupporter(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
