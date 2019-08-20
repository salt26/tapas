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
    
    private DoorControl[] doors;

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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        answer = new int[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        state = new bool[4] { false, false, false, false };
        recentNum = 0;
        
        doors = FindObjectsOfType(typeof(DoorControl)) as DoorControl[];
        switchGroup = GameObject.Find("SwitchGroup");

        /*
        switches = FindObjectsOfType(typeof(SwitchPuzzle)) as SwitchPuzzle[];
        doors = FindObjectsOfType(typeof(DoorControl)) as DoorControl[];

        score = 0;
        recentIndex = -1;
        state = new bool[switches.Length];
        pairIndex = new int[switches.Length];

        
        for(int i = 0; i < switches.Length; i++)
            pairIndex[i] = i;

        // shuffle!
        for (int i = 0; i < switches.Length; i++)
        {
            int j = Random.Range(0, switches.Length);
            int tmp = pairIndex[i];
            pairIndex[i] = pairIndex[j];
            pairIndex[j] = tmp;
        }

        for(int i = 0; i < switches.Length; i++)
        {
            //switches[i].SetIndexTexture(indexTextures[i]);
            //switches[i].SetScoreTexture(scoreTextures[0]);
        }
        */
    }

    /*
    void Calculate()
    {
        if (score == maxScore) return; // puzzle already cleared

        score = 0;
        for (int i = 0; i < switches.Length - 1; i += 2)
        {
            if (state[pairIndex[i]] != state[pairIndex[i + 1]])
                score++;
        }
    }
    */
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

    public void CollisionEnter(Transform others)
    {
        BMSLogger.DebugLog("CollisionEnter");

        if (Score() == state.Length) return; // puzzle already cleared
        
        for (int i = 0; i < answer.Length; i++)
        {
            if (others.transform == switchGroup.GetComponent<SwitchPuzzle>().switches[i].transform)
            {
                if (recentNum == i + 1)
                {
                    networkObject.SendRpc(RPC_CANT_PUSH_AGAIN, Receivers.All);
                    BMSLogger.DebugLog("thief tried to push a single switch again");
                    break;
                }

                recentNum = i + 1;
                Toggle(recentNum);
                BMSLogger.DebugLog("Switch " + recentNum + " pushed");

                networkObject.recentNum = recentNum; // it can be executed by server only (only server can call CollsionEnter method)

                byte stateByte = 0b0000; // 0b group4on?/group3on?/group2on?/group1on?
                for (int j = 0; j < state.Length; j++)
                {
                    if (state[j] == true)
                    {
                        stateByte = (byte)(stateByte | 0b1000);
                    }
                    stateByte = (byte)(stateByte >> 1);
                }
                networkObject.state = stateByte;

                networkObject.SendRpc(RPC_UPDATE_SWITCHES, Receivers.All);

            }
        }

        /*
        if (score == maxScore) return; // puzzle already cleared

        for (int i = 0; i < switches.Length; i++)
        {
            if (recentIndex != i && others.transform == switches[i].transform)
            {
                recentIndex = i;
                state[i] = !state[i];
                Debug.Log("Switch " + (i + 1) + " Toggled");
                Calculate();

                networkObject.lastTouch = i;
                networkObject.onGroupsNum = score;
                networkObject.switchesState = StateToInt();

                networkObject.SendRpc(RPC_UPDATE_SWITCHES, Receivers.All);
                break;
            }
        }
        */
    }

    /*
    private int StateToInt()
    {
        int ret = 0;
        for (int i = 0; i < switches.Length; i++)
        {
            if (state[i])
            {
                ret += 1 << i;
            }
        }
        return ret;
    }
    */

    public override void UpdateSwitches(RpcArgs args)
    {
        switchGroup.GetComponent<SwitchPuzzle>().SetRotNum(networkObject.recentNum);

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
                // TODO: Change SupportMsg
                break;
            case 4: // Thief Supporter
                GameManager.instance.normalMsg.text = "도둑이 스위치를 눌렀습니다.";
                GameManager.instance.normalMsgShowTime = 3f;
                // TODO: Change(Add) SupportMsg
                break;
            default:
                break;
        }

        if (networkObject.state == 0b1111)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].Open();
            }

            GameManager.instance.importantMsg.text = "탈출구가 개방되었습니다!";
        }
        /*
        Debug.Log("UpdateSwitches");
        for (int j = 0; j < switches.Length; j++)
        {
            //switches[j].SetScoreTexture(scoreTextures[networkObject.onGroupsNum]);
            //switches[j].SetRotation(networkObject.lastTouch == j);
        }
        if (networkObject.onGroupsNum == maxScore)
        {
            for (int j = 0; j < doors.Length; j++)
            {
                doors[j].Open();
            }
        }
        */
    }

    public override void CantPushAgain(RpcArgs args)
    {
        if (GameManager.instance.M_TeamID == 2)
        {
            GameManager.instance.normalMsg.text = "같은 스위치를 연속해서 누를 수 없습니다.";
            GameManager.instance.normalMsgShowTime = 3f;
        }
    }
}
