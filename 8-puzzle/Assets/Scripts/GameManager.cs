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
    public Image mapGuide;
    public Image mapIcon;
    public Image mapAllyIcon;

    public InputField chatInputBox;
    public Text chatBox;
    public Image view;
    public Color deactivatedColor;
    public Color activatedColor;
    public ScrollRect scroll;

    public float normalMsgShowTime;

    public float time;
    public Transform maze;
    public bool timeOver = false;

    public float staminaValue;
    public float staminaValueMax = 2.5f;

    private int m_TeamID = -1;
    private int win_TeamID = 0; // 0 : Game running, 1 : Police win, 2 : Thief win, 3 : Game end, 4: Game exploited by disconnect
    private float roundTime = 600f;
    private float timeoutTimer = -1f;
    private bool isReady = false;   // Used by client and server
    private List<int> readyPlayers = new List<int>();   // Server only
    private GameObject drone;
    private GameObject ally;
    
    private float mazeScale = 1f;

    public bool canChat = false;

    public float scrollSpeed = 0.5f;

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
                timeoutTimer = 10f;
            }
        }
    }

    void Update()
    {
        if (!isReady || !networkObject.isReady)
        {
            if (timeoutTimer > 0f)
                timeoutTimer -= Time.deltaTime;
            else if (timeoutTimer > -1f)
            {
                networkObject.SendRpc(RPC_GAME_END, Receivers.All, 4, false);
            }
            return;
        }
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
            // Timer
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

            // Chat
            if(Input.GetKeyDown(KeyCode.Return))
            {
                if(canChat)
                {
                    SendPlayersMessage();
                    chatInputBox.Select();
                    chatInputBox.interactable = false;
                    view.enabled = false;
                    chatBox.color = deactivatedColor;
                    canChat = false;
                }
                else
                {
                    chatInputBox.interactable = true;
                    chatInputBox.Select();
                    chatInputBox.ActivateInputField();
                    view.enabled = true;
                    chatBox.color = activatedColor;
                    canChat = true;
                }
            }

            //Debug.Log(chatInputBox.isFocused);

            if(canChat)
            {
                float scrollpos = scroll.verticalNormalizedPosition + (Input.GetAxis("Mouse ScrollWheel") * scrollSpeed);
                scroll.verticalNormalizedPosition = Mathf.Clamp(scrollpos, 0f, 1f);
            }

            // Stamina Gauge
            if (m_TeamID == 1)
            {
                stamina.rectTransform.sizeDelta = new Vector2(150f * staminaValue, 60f);
            }

            // Map Icon
            if (m_TeamID == 3)
            {
                if (drone == null)
                {
                    drone = GameObject.Find("PoliceDrone(Clone)");
                }
                if (ally == null)
                {
                    ally = GameObject.Find(("Police(Clone)"));
                }

                mapIcon.rectTransform.anchoredPosition = new Vector3(drone.transform.position.z * 347f / 106.68f, drone.transform.position.x * (-347f) / 106.68f, 0);
                mapIcon.rectTransform.eulerAngles = new Vector3(0, 0, -drone.transform.eulerAngles.y);

                mapAllyIcon.rectTransform.anchoredPosition = new Vector3(ally.transform.position.z * 347f / 106.68f, ally.transform.position.x * (-347f) / 106.68f, 0);
                mapAllyIcon.rectTransform.eulerAngles = new Vector3(0, 0, -ally.transform.eulerAngles.y);
            }

            if (m_TeamID == 4)
            {
                if (drone == null)
                {
                    drone = GameObject.Find("ThiefDrone(Clone)");
                }
                if (ally == null)
                {
                    ally = GameObject.Find(("Thief(Clone)"));
                }

                mapIcon.rectTransform.anchoredPosition = new Vector3(ally.transform.position.z * 347f / 106.68f, drone.transform.position.x * (-347f) / 106.68f, 0);
                mapIcon.rectTransform.eulerAngles = new Vector3(0, 0, -drone.transform.eulerAngles.y);
                
                mapAllyIcon.rectTransform.anchoredPosition = new Vector3(ally.transform.position.z * 347f / 106.68f, ally.transform.position.x * (-347f) / 106.68f, 0);
                mapAllyIcon.rectTransform.eulerAngles = new Vector3(0, 0, -ally.transform.eulerAngles.y);
            }
        }
    }

    /// <summary>
    /// RPC를 호출할 대상(NetworkingPlayer)을 teamID로 찾습니다.
    /// 서버에서만 호출 가능합니다.
    /// </summary>
    /// <param name="teamID"></param>
    /// <returns></returns>
    public NetworkingPlayer GetNetworkingPlayerByTeamID(int teamID)
    {
        NetworkingPlayer player = null;
        if (NetworkManager.Instance.IsServer && isReady && teamID >= 1 && teamID <= 4)
        {
            var netID = LobbyManager.lm.LobbyPlayersStarted[teamID];
            NetworkManager.Instance.Networker.IteratePlayers((np) => {
                if (np.NetworkId == netID)
                {
                    player = np;
                }
            });
        }
        return player;
    }

    /// <summary>
    /// 클라이언트에서 서버로부터 자신의 직업을 할당받은 다음
    /// 캐릭터를 생성하고 서버에게 준비됨을 알립니다.
    /// </summary>
    /// <param name="args"></param>
    public override void GameStart(RpcArgs args)
    {
        m_TeamID = args.GetNext<int>();
        //BMSLogger.DebugLog("GameStart: my teamID is " + m_TeamID);

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
        
        staminaValue = staminaValueMax;

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
            NetworkManager.Instance.InstantiateSupporter(0, position: new Vector3(48.3f * mazeScale, 9f * mazeScale, 53.3f * mazeScale));
            map.enabled = true;
            mapGuide.enabled = true;
            mapIcon.enabled = true;
            mapAllyIcon.enabled = true;
            supportMsg.enabled = true;
            drone = GameObject.Find("PoliceDrone(Clone)");
        }
        else if (m_TeamID == 4) // Thief Supporter
        {
            NetworkManager.Instance.InstantiateSupporter(1, position: new Vector3(58.3f * mazeScale, 9f * mazeScale, 53.3f * mazeScale));
            map.enabled = true;
            mapGuide.enabled = true;
            mapIcon.enabled = true;
            mapAllyIcon.enabled = true;
            supportMsg.enabled = true;
            drone = GameObject.Find("ThiefDrone(Clone)");
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
        //BMSLogger.DebugLog("Ready: his/her teamID is " + playerTeamID);
        if (playerTeamID < 1 || playerTeamID > 4) return;

        if (readyPlayers.IndexOf(playerTeamID) == -1)
            readyPlayers.Add(m_TeamID);

        if (readyPlayers.Count == 4)
        {
            normalMsgShowTime = 0;
            time = roundTime;
            isReady = true;
            networkObject.isReady = true;
            //BMSLogger.DebugLog("Ready: Server ready!");
        }
    }
    
    private void ReturnLobby()
    {
        LobbyManager.lm.ReturnToLobby();
        SceneManager.LoadScene(1);
    }

    public void SendPlayersMessage()
    {
        Debug.Log("Send Message!");
        string chatMessage = chatInputBox.text;
        if (string.IsNullOrEmpty(chatMessage))
            return;

        if (m_TeamID == 1 || m_TeamID == 3)
        {
            networkObject.SendRpc(RPC_RECEIVE_MESSAGE, Receivers.All, chatMessage, 1, m_TeamID);
        }
        else if (m_TeamID == 2 || m_TeamID == 4)
        {
            networkObject.SendRpc(RPC_RECEIVE_MESSAGE, Receivers.All, chatMessage, 2, m_TeamID);
        }

        chatInputBox.text = string.Empty;
    }

    public override void ReceiveMessage(RpcArgs args)
    {
        string message = args.GetNext<string>();
        int team = args.GetNext<int>();
        int sender = args.GetNext<int>();
        
        if(team == 1)
        {
            if(m_TeamID == 1 || m_TeamID == 3)
            {
                if(sender == 1)
                {
                    chatBox.text += string.Format("경찰: {0}\n", message);
                }
                else if (sender == 3)
                {
                    chatBox.text += string.Format("경찰 조력자: {0}\n", message);
                }
                scroll.verticalNormalizedPosition = 0f;
            }
        }
        else if(team == 2)
        {
            if(m_TeamID == 2 || m_TeamID == 4)
            {
                if (sender == 2)
                {
                    chatBox.text += string.Format("도둑: {0}\n", message);
                }
                else if (sender == 4)
                {
                    chatBox.text += string.Format("도둑 조력자: {0}\n", message);
                }
            }
            scroll.verticalNormalizedPosition = 0f;
        }
    }

    public override void GameEnd(RpcArgs args)
    {
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
        else if (win == 4)  // Someone is disconnected
        {
            Debug.Log("Exploited!");
            importantMsg.text = "누군가가 게임을 나갔습니다. 게임이 곧 종료됩니다.";
        }

        Invoke("ReturnLobby", 5f);
    }
}
