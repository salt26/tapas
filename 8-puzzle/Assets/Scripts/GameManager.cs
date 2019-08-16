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

    public float time;
    public Transform maze;

    private int m_TeamID = -1;
    private int win_TeamID = 0; // 0 : Game running, 1 : Police win, 2 : Thief win, 3 : Game end
    private float roundTime = 10000000f;
    
    private float mazeScale = 1f;

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
        mazeScale = maze.localScale.x;

        int r = Random.Range(0, 4);
        List<Vector3> policePositions;
        List<Quaternion> policeRotations;
        policePositions = new List<Vector3>
        {
            new Vector3(2f * mazeScale, 0f, 2f * mazeScale),
            new Vector3(104.7f * mazeScale, 0f, 2f * mazeScale),
            new Vector3(104.7f * mazeScale, 0f, 104.7f * mazeScale),
            new Vector3(2f * mazeScale, 0f, 104.7f * mazeScale)
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
            NetworkManager.Instance.InstantiateThief(position: new Vector3(53.3f * mazeScale, 0f, 53.3f * mazeScale));
        else if (m_TeamID == 3)
            NetworkManager.Instance.InstantiateSupporter(0, position: new Vector3(48.3f * mazeScale, 7f * mazeScale, 53.3f * mazeScale));
        else if (m_TeamID == 4)
            NetworkManager.Instance.InstantiateSupporter(1, position: new Vector3(58.3f * mazeScale, 7f * mazeScale, 53.3f * mazeScale));

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

            //BMSLogger.DebugLog(win_TeamID.ToString());
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
