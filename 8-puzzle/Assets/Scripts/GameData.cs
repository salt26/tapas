using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : SwitchManagerBehavior
{
    // singleton implementation
    private static GameData _instance = null;

    public GameObject switchGroup;

    private int[] answer; // group1: index 0,1 / group2: index 2,3 / group3: index 4,5 / group4: index 6,7
    private bool[] state; // group1 on? / group2 on? / group3 on? / group4 on?
    private int recentNum;
    private bool puzzleEnd;
    private string policeSuppMsg;
    private string thiefSuppMsg;

    private int tryCount;
    private int historyLimit = 3;
    
    private DoorControl[] doors;
    private bool isReady = false;

    /*
    private int score;
    private int recentIndex;
    private bool[] state;
    private int[] pairIndex;
    private SwitchPuzzle[] switches;
    private DoorControl[] doors;
    */

    public static GameData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(GameData)) as GameData;
                if (_instance == null)
                {
                    Debug.LogError("There's no active GameData object");
                }
            }

            return _instance;
        }
    }
    
    protected override void NetworkStart()
    {
        base.NetworkStart();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        answer = new int[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        state = new bool[4] { false, false, false, false };
        recentNum = 0;
        tryCount = 0;

        puzzleEnd = false;
        
        doors = FindObjectsOfType(typeof(DoorControl)) as DoorControl[];
        switchGroup = GameObject.Find("SwitchGroup");

        if (networkObject.IsServer)
        {
            // answer shuffle
            for (int i = 0; i < answer.Length * 128; i++)
            {
                int j = Random.Range(0, answer.Length);
                int tmp = answer[i % 8];
                answer[i % 8] = answer[j];
                answer[j] = tmp;
            }

            Debug.Log("server answer: " + answer[0] + " " + answer[1] + " " + answer[2] + " " + answer[3] + " " + answer[4] + " " + answer[5] + " " + answer[6] + " " + answer[7]);
            Debug.Log("server state: " + state[0] + " " + state[1] + " " + state[2] + " " + state[3]);
            
            MakePoliceSuppMsg();
            MakeThiefSuppMsg();

            networkObject.SendRpc(RPC_UPDATE_POLICE_SUPP_MSG, Receivers.All, policeSuppMsg);
            networkObject.SendRpc(RPC_UPDATE_THIEF_SUPP_MSG, Receivers.All, thiefSuppMsg);
        }
        isReady = true;
    }

    void Update()
    {
        if (isReady && Score() == 4 && !puzzleEnd)
        {
            networkObject.SendRpc(RPC_OPEN_EXIT, Receivers.All);
            puzzleEnd = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void MakePoliceSuppMsg()
    {
        policeSuppMsg = ">>>>>>>>>> 스위치 작동 상태 <<<<<<<<<<";

        for (int i = 0; i < 4; i++)
        {
            int firstNum = answer[i * 2];
            int secondNum;
            if (answer[i * 2 + 1] < firstNum)
            {
                secondNum = firstNum;
                firstNum = answer[i * 2 + 1];
            }
            else
            {
                secondNum = answer[i * 2 + 1];
            }

            policeSuppMsg += "\n그룹 " + i + " - " + "스위치 " + firstNum + ", " + secondNum + " - " + (state[i] ? "ON" : "OFF");
        }
    }

    private void MakeThiefSuppMsg()
    {
        if (tryCount == 0)
        {
            thiefSuppMsg = ">>>>> 스위치 작동 내역 (최대 최근 " + historyLimit + "건) <<<<<";
        }
        else 
        {
            if (tryCount > historyLimit) // need to delete oldest history
            {
                thiefSuppMsg = thiefSuppMsg.Substring(64);
                thiefSuppMsg = ">>>>> 스위치 작동 내역 (최대 최근 " + historyLimit + "건) <<<<< " + thiefSuppMsg;
            }
                thiefSuppMsg += "\n- 작동된 스위치 번호 : " + recentNum + " / 켜진 그룹 개수 : " + Score();
        }
    }

    private int Score()
    {
        int ret = 0;

        for (int i = 0; i < state.Length; i++)
        {
            if (state[i] == true) ret++;
        }

        return ret;
    }

    private void Toggle(int switchNum)
    {
        for (int i = 0; i < answer.Length; i++)
        {
            if (answer[i] == switchNum)
            {
                state[i / 2] = !state[i / 2];
                break;
            }
        }
    }

    public void CollisionEnter(Transform others) // only server can call CollsionEnter method
    {
        BMSLogger.DebugLog("CollisionEnter");

        if (puzzleEnd) return; // puzzle already cleared
        
        for (int i = 0; i < answer.Length; i++)
        {
            if (others.transform == switchGroup.GetComponent<SwitchPuzzle>().switches[i].transform)
            {
                if (recentNum == i + 1)
                {
                    networkObject.SendRpc(RPC_CANT_PUSH_AGAIN, Receivers.All);
                    break;
                }
                else
                {
                    recentNum = i + 1;
                    Toggle(recentNum);
                    tryCount++;
                    MakePoliceSuppMsg();
                    MakeThiefSuppMsg();
                    BMSLogger.DebugLog("Switch " + recentNum + " pushed");

                    networkObject.SendRpc(RPC_UPDATE_SWITCHES, Receivers.All, recentNum);
                    networkObject.SendRpc(RPC_UPDATE_POLICE_SUPP_MSG, Receivers.All, policeSuppMsg);
                    networkObject.SendRpc(RPC_UPDATE_THIEF_SUPP_MSG, Receivers.All, thiefSuppMsg);
                    break;
                }
            }
        }
    }
    

    public override void UpdateSwitches(RpcArgs args)
    {
        int recentNum = args.GetNext<int>();

        switchGroup.GetComponent<SwitchPuzzle>().SetRotNum(recentNum);

        int teamID = GameManager.instance.M_TeamID;

        switch (teamID)
        {
            case 1: // Police
                break;
            case 2: // Thief
                GameManager.instance.normalMsg.text = "스위치를 눌렀습니다.";
                GameManager.instance.normalMsgShowTime = 3f;
                break;
            case 3: // Police Supporter
                GameManager.instance.normalMsg.text = "도둑이 스위치를 눌렀습니다.";
                GameManager.instance.normalMsgShowTime = 3f;
                break;
            case 4: // Thief Supporter
                GameManager.instance.normalMsg.text = "도둑이 스위치를 눌렀습니다.";
                GameManager.instance.normalMsgShowTime = 3f;
                break;
            default:
                break;
        }
    }

    public override void CantPushAgain(RpcArgs args)
    {
        if (GameManager.instance.M_TeamID == 2)
        {
            GameManager.instance.normalMsg.text = "같은 스위치를 연속해서 누를 수 없습니다.";
            GameManager.instance.normalMsgShowTime = 3f;
        }
    }

    public override void OpenExit(RpcArgs args)
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open();
        }

        GameManager.instance.importantMsg.text = "탈출구가 개방되었습니다!";
    }

    public override void UpdatePoliceSuppMsg(RpcArgs args)
    {
        if (GameManager.instance.M_TeamID == 3)
        {
            GameManager.instance.supportMsg.text = args.GetNext<string>();
        }
    }
    
    public override void UpdateThiefSuppMsg(RpcArgs args)
    {
        if (GameManager.instance.M_TeamID == 4)
        {
            GameManager.instance.supportMsg.text = args.GetNext<string>();
        }
    }
}
