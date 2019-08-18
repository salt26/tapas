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
    
    public int maxScore = 4;
    public Texture[] scoreTextures;
    public Texture[] indexTextures;

    private int score;
    private int recentIndex;
    private bool[] state;
    private int[] pairIndex;
    private SwitchPuzzle[] switches;
    private DoorControl[] doors;

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
    }

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

    public void CollisionEnter(Transform others)
    {
        Debug.Log("CollisionEnter");
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
    }

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

    public override void GetHistory(RpcArgs args)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateSwitches(RpcArgs args)
    {
        Debug.Log("UpdateSwitches");
        for (int j = 0; j < switches.Length; j++)
        {
            switches[j].SetScoreTexture(scoreTextures[networkObject.onGroupsNum]);
            switches[j].SetRotation(networkObject.lastTouch == j);
        }
        if (networkObject.onGroupsNum == maxScore)
        {
            for (int j = 0; j < doors.Length; j++)
            {
                doors[j].Open();
            }
        }
    }
}
