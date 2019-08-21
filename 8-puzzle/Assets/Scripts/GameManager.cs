using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Unity.Lobby;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : GameManagerBehavior
{
    public static GameManager instance;
    public Text timeMsg;
    public Text normalMsg;
    public Text importantMsg;
    public Text supportMsg;
    public Text item1Txt;
    public Text item2Txt;
    public Text item3Txt;
    public Image item1;
    public Image item2;
    public Image item3;
    public Image staminaBackground;
    public Image stamina;
    public Image staminaBar;
    public Image map;
    public Image mapIconSupp;
    public Image mapIconAlly;

    public CanvasGroup chatUI;
    public InputField chatInputBox;
    public Text chatBox;

    public float normalMsgShowTime;

    public float time;
    public Transform maze;
    public bool timeOver = false;

    private int m_TeamID = -1;
    private int win_TeamID = 0; // 0 : Game running, 1 : Police win, 2 : Thief win, 3 : Game end
    private float roundTime = 320f;
    private bool isReady = false;   // Used by client and server
    private List<int> readyPlayers = new List<int>();   // Server only
    
    private float mazeScale = 1f;

    public int M_TeamID
    {
        get
        {
            return m_TeamID;
        }
    }

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
    }

    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (LobbyManager.lm != null)
        {
            //m_TeamID = LobbyManager.lm.Myself.AssociatedPlayer.TeamID;
            LobbyManager.lm.GetComponent<Canvas>().enabled = false;

            if (NetworkManager.Instance.IsServer)
            {
                networkObject.isReady = false;
                foreach (var p in LobbyManager.lm.LobbyPlayersStarted)
                {
                    NetworkManager.Instance.Networker.IteratePlayers((np) => {
                        if (np.NetworkId == p.Value && p.Key >= 1 && p.Key <= 4)
                        {
                            // 각 클라이언트에게 어떤 직업인지 알려줌
                            networkObject.SendRpc(np, RPC_GAME_START, p.Key);
                        }
                    });
                }
            }
        }
    }

    void Update()
    {
        if (!isReady || !networkObject.isReady) return;
        if (NetworkManager.Instance.IsServer)
        {
            if (win_TeamID == 3) return; // game ended

            if (win_TeamID != 0) // Police or thief win
            {
                networkObject.SendRpc(RPC_GAME_END, Receivers.All, win_TeamID, timeOver);
                win_TeamID = 3;
                return;
            }

            time -= Time.deltaTime;
            networkObject.time = time;

            if (time <= 0)
            {
                time = 0;
                networkObject.time = time;
                timeOver = true;
                Win_TeamID = 1;
            }

            //BMSLogger.DebugLog(win_TeamID.ToString());
        }
        else // IsClient
        {
            int t = Mathf.CeilToInt(networkObject.time);
            if (t % 60 < 10)
            {
                timeMsg.text = (t / 60) + " : 0" + (t % 60);
            }
            else
            {
                timeMsg.text = (t / 60) + " : " + (t % 60);
            }

            if (normalMsgShowTime > 0)
            {
                normalMsg.enabled = true;
                normalMsgShowTime -= Time.deltaTime;
            }
            else
            {
                normalMsg.enabled = false;
            }
        }
    }

    /// <summary>
    /// 클라이언트에서 서버로부터 자신의 직업을 할당받은 다음
    /// 캐릭터를 생성하고 서버에게 준비됨을 알립니다.
    /// </summary>
    /// <param name="args"></param>
    public override void GameStart(RpcArgs args)
    {
        m_TeamID = args.GetNext<int>();
        BMSLogger.DebugLog("GameStart: my teamID is " + m_TeamID);

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
        if (m_TeamID == 1) // Police
        {
            NetworkManager.Instance.InstantiatePolice(position: policePositions[r], rotation: policeRotations[r]);
            item1.enabled = true;
            item2.enabled = true;
            item3.enabled = true;
            item1Txt.enabled = true;
            item2Txt.enabled = true;
            item3Txt.enabled = true;
            staminaBackground.enabled = true;
            stamina.enabled = true;
            staminaBar.enabled = true;
        }
        else if (m_TeamID == 2) // Thief
        {
            NetworkManager.Instance.InstantiateThief(position: new Vector3(53.3f * mazeScale, 0f, 53.3f * mazeScale));

        }
        else if (m_TeamID == 3) // Police Supporter
        {
            NetworkManager.Instance.InstantiateSupporter(0, position: new Vector3(48.3f * mazeScale, 7f * mazeScale, 53.3f * mazeScale));
            map.enabled = true;
            mapIconSupp.enabled = true;
            mapIconAlly.enabled = true;
        }
        else if (m_TeamID == 4) // Thief Supporter
        {
            NetworkManager.Instance.InstantiateSupporter(1, position: new Vector3(58.3f * mazeScale, 7f * mazeScale, 53.3f * mazeScale));
            map.enabled = true;
            mapIconSupp.enabled = true;
            mapIconAlly.enabled = true;
        }

        normalMsgShowTime = 0;
        if (!NetworkManager.Instance.IsServer)
            isReady = true;
        networkObject.SendRpc(RPC_READY, Receivers.Server, m_TeamID);
    }

    /// <summary>
    /// 서버에서, 클라이언트가 준비되었음을 알고
    /// 모든 클라이언트가 준비되면 게임을 시작합니다.
    /// </summary>
    /// <param name="args"></param>
    public override void Ready(RpcArgs args)
    {
        if (!NetworkManager.Instance.IsServer) return;

        int playerTeamID = args.GetNext<int>();
        BMSLogger.DebugLog("Ready: his/her teamID is " + playerTeamID);
        if (playerTeamID < 1 || playerTeamID > 4) return;

        if (readyPlayers.IndexOf(playerTeamID) == -1)
            readyPlayers.Add(m_TeamID);

        if (readyPlayers.Count == 4)
        {
            normalMsgShowTime = 0;
            time = roundTime;
            isReady = true;
            networkObject.isReady = true;
            BMSLogger.DebugLog("Ready: Server ready!");
        }
    }

    public void SendPlayersMessage(int teamID)
    {
        string chatMessage = chatInputBox.text;
        if (string.IsNullOrEmpty(chatMessage))
            return;
        /*
         GetComponent<Police>().networkObject.SendRpc(PoliceBehavior.RPC_CHAT, Receivers.All, chatMessage, teamID);
        GetComponent<Thief>().networkObject.SendRpc(PoliceBehavior.RPC_CHAT, Receivers.All, chatMessage, teamID);
        GetComponent<SupporterNetworkObject>().SendRpc(SupporterBehavior.RPC_CHAT, Receivers.All, chatMessage, teamID);
        */

        chatInputBox.text = string.Empty;
        chatInputBox.DeactivateInputField();
    }

    /*
    public void ReceiveMessage(string message)
    {
        chatBox.text += (message + "\n");
    }
    */

    public override void GameEnd(RpcArgs args)
    {
        if (!isReady || !networkObject.isReady) return;
        int win = args.GetNext<int>();
        bool timeOver = args.GetNext<bool>();
        if (win == 1) // Police win
        {
            Debug.Log("Police win");
            if (timeOver)
            {
                importantMsg.text = "경찰의 승리! 도둑이 탈출에 실패하였습니다!";
            }
            else
            {
                importantMsg.text = "경찰의 승리! 도둑이 경찰에게 잡혔습니다!";
            }
        }
        else if (win == 2) // Thief win
        {
            Debug.Log("Thief win");
            importantMsg.text = "도둑의 승리! 도둑이 탈출에 성공하였습니다!";
        }

        //SceneManager.LoadScene(1); //is it right??
    }
}
