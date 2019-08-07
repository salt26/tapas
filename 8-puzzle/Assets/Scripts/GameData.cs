using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // singleton implementation
    private static GameData _instance = null;

    public GameObject player;
    // public SwitchPuzzle[] switches;
    // public DoorControl[] doors;
    [ColorUsageAttribute(true, true)]
    public Color[] colors;

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
        switches = FindObjectsOfType(typeof(SwitchPuzzle)) as SwitchPuzzle[];
        doors = FindObjectsOfType(typeof(DoorControl)) as DoorControl[];

        score = 0;
        recentIndex = -1;
        state = new bool[switches.Length];
        pairIndex = new int[switches.Length];

        for(int i = 0; i < switches.Length; i++)
            pairIndex[i] = i;

        for(int i = 0; i < switches.Length; i++)
        {
            int j = Random.Range(0, switches.Length);
            int tmp = pairIndex[i];
            pairIndex[i] = pairIndex[j];
            pairIndex[j] = tmp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Calculate();
        for (int i = 0; i < switches.Length; i++)
        {
            switches[i].SetColor(colors[score]);
        }
        if(score == 4)
        {
            for(int i = 0; i < doors.Length; i++)
            {
                doors[i].Open();
            }
        }
    }

    void Calculate()
    {
        score = 0;
        for(int i = 0; i < switches.Length; i += 2)
        {
            if (state[pairIndex[i]] != state[pairIndex[i + 1]])
                score++;
        }
    }

    public void CollisionEnter(SwitchPuzzle mySwitch, Collision other)
    {
        if (other.gameObject == player)
        {
            for(int i = 0; i < switches.Length; i++)
            {
                if(recentIndex != i && mySwitch == switches[i])
                {
                    recentIndex = i;
                    state[i] = !state[i];
                    Debug.Log("Switch " + i + " Toggled");
                }
            }
        }
    }
}
