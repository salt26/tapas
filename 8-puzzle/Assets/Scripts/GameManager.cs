using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Unity.Lobby;
using System.Collections;
using System.Collections.Generic;
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

    private int m_TeamID = -1;
    private int win_TeamID = 0; // 0 : Game running, 1 : Police win, 2 : Thief win, 3 : Game end
    private float roundTime = 320f;
    public bool timeOver = false;
    
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

        if (LobbyManager.lm != null)
        {
            m_TeamID = LobbyManager.lm.Myself.AssociatedPlayer.TeamID;
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

        if (NetworkManager.Instance.IsServer)
        {
            time = roundTime;
        }

        normalMsgShowTime = 0;
    }
    
    void Update()
    {
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
