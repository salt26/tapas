using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Unity.Lobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GameManagerBehavior
{
    public static GameManager instance;

    private int m_TeamID = -1;
    private int win_TeamID = 0; // 0 : Game running, 1 : Police win, 2 : Thief win, 3 : Game end
    private float roundTime = 10f;
    public float time;

    public int Win_TeamID
    {
        get
        {
            return win_TeamID;
        }
        set
        {
            win_TeamID = value; // Exit.cs uses it
        }
    }
    
    void Awake()
    {
        instance = this;

        if (LobbyManager.lm != null)
        {
            m_TeamID = LobbyManager.lm.Myself.GetComponent<LobbyPlayerItem>().AssociatedPlayer.TeamID;
            LobbyManager.lm.GetComponent<Canvas>().enabled = false;
        }
    }

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

        if (NetworkManager.Instance.IsServer)
        {
            time = roundTime;
        }
    }
    
    void Update()
    {
        if (win_TeamID == 3) return; // game ended

        if (NetworkManager.Instance.IsServer)
        {
            if (win_TeamID != 0) // Police or thief win
            {
                networkObject.SendRpc(RPC_GAME_END, Receivers.Others, win_TeamID);
                win_TeamID = 3;
            }

            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 0;
                win_TeamID = 1;
            }
        }
    }

    public override void GameEnd(RpcArgs args)
    {
        if (args.GetNext<int>() == 1) // Police win
        {
            Debug.Log("Police win");
            // TODO: POLICE WIN
        }
        else if (args.GetNext<int>() == 2) // Thief win
        {
            Debug.Log("Thief win");
            // TODO: THIEF WIN
        }
    }
}
