using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Unity.Lobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GameManagerBehavior
{
    public static GameManager instance;

    private int m_TeamID = -1;
    private int win_TeamID = 0; // 0 : Game running, 1 : Police win, 2 : Thief win, 3 : Game end
    private float roundTime = 10000000f;
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
        int r = Random.Range(0, 4);
        List<Vector3> policePositions;
        List<Quaternion> policeRotations;
        policePositions = new List<Vector3>
        {
            new Vector3(2f, 0f, 2f),
            new Vector3(135f, 0f, 2f),
            new Vector3(135f, 0f, 135f),
            new Vector3(2f, 0f, 135f)
        };
        policeRotations = new List<Quaternion>
        {
            Quaternion.Euler(0f, 45f, 0f),
            Quaternion.Euler(0f, -45f, 0f),
            Quaternion.Euler(0f, -135f, 0f),
            Quaternion.Euler(0f, 135f, 0f)
        };
        if (m_TeamID == 1)
            NetworkManager.Instance.InstantiatePolice(position: policePositions[r], rotation: policeRotations[r]);
        else if (m_TeamID == 2)
            NetworkManager.Instance.InstantiateThief(position: new Vector3(68.5f, 0f, 68.5f));
        else if (m_TeamID == 3)
            NetworkManager.Instance.InstantiateSupporter(0, position: new Vector3(62.5f, 8f, 68.5f));
        else if (m_TeamID == 4)
            NetworkManager.Instance.InstantiateSupporter(1, position: new Vector3(74.5f, 8f, 68.5f));

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
                networkObject.SendRpc(RPC_GAME_END, Receivers.All, win_TeamID);
                win_TeamID = 3;
                return;
            }

            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 0;
                Win_TeamID = 1;
            }

            BMSLogger.DebugLog(win_TeamID.ToString());
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
