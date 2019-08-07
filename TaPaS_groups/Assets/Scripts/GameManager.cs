using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    uint answer = 0x87654321; // 87|65|43|21 4 groups
    bool group1 = false;
    bool group2 = false;
    bool group3 = false;
    bool group4 = false;
    public ulong history = 0;
    public int trial = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        shuffle();

        /*
        Debug.Log(string.Format("{0:X}", answer));
        Debug.Log(string.Format("{0:X}", history));
        for (uint i = 8; i >= 1; i--)
        {
            check(i);
            Debug.Log(string.Format("{0:X}", history));
            Debug.Log(count());
        }
        for (uint i = 1; i <= 8; i++)
        {
            check(i);
            Debug.Log(string.Format("{0:X}", history));
            Debug.Log(count());
        }
        for (uint i = 1; i <= 8; i++)
        {
            check(i);
            Debug.Log(string.Format("{0:X}", history));
            Debug.Log(count());
        }
        */
    }

    private void swap(int index1, int index2)
    {
        /*
        if (index1 > 7 || index1 < 0 || index2 > 7 || index2 < 0)
        {
            Debug.Log("swap input error");
            return;
        }
        */
        if (index1 == index2)
        {
            return;
        }

        uint tmp = 0x0000000f;
        uint stmp = 0x0000000f;
        uint ttmp = 0x0000000f;

        stmp = stmp << (4*index1);
        stmp = stmp & answer;
        stmp = stmp >> (4*index1);

        tmp = tmp << (4*index1);
        answer = ~tmp & answer;

        ttmp = ttmp << (4*index2);
        ttmp = ttmp & answer;
        ttmp = ttmp >> (4*index2);

        tmp = 0x0000000f;
        tmp = tmp << (4*index2);
        answer = ~tmp & answer;

        stmp = stmp << (4*index2);
        answer = stmp | answer;

        ttmp = ttmp << (4*index1);
        answer = ttmp | answer;
    }

    private void shuffle()
    {
        for (int i = 0; i < 1024; i++)
        {
            swap(i % 8, Random.Range(0, 8));
        }
    }

    public void check(uint switchNum)
    {
        int indexNum = 0;
        while (indexNum < 8)
        {
            if (((answer >> 4*indexNum) & 0x0000000f) == switchNum)
            {
                //Debug.Log("switchNum " + switchNum);
                switch (indexNum / 2)
                {
                    case 0: // index 0, 1
                        group1 = !group1;
                        writeHistory(switchNum, group1);
                        break;
                    case 1: // index 2, 3
                        group2 = !group2;
                        writeHistory(switchNum, group2);
                        break;
                    case 2: // index 4, 5
                        group3 = !group3;
                        writeHistory(switchNum, group3);
                        break;
                    case 3: // index 6, 7
                        group4 = !group4;
                        writeHistory(switchNum, group4);
                        break;
                    default:
                        break;
                }
                Debug.Log("Switch " + (((history & 0b1110) >> 1) + 1) + ", " + ((history & 0b1) == 1 ? "on" : "off") + ", total " + count());
                return;
            }
            indexNum++;
        }
    }

    public int count()
    {
        int ret = 0;
        if (group1) { ret++; }
        if (group2) { ret++; }
        if (group3) { ret++; }
        if (group4) { ret++; }
        return ret;
    }

    private void writeHistory(uint switchNum, bool on)
    {
        history = (history << 4) + (ulong)(((switchNum - 1) << 1) + (on ? 0b1 : 0b0));
        trial++;
    }

    public void readHistory()
    {
        int onCount = 0;

        Debug.Log("--- History ---");

        if (trial > 16)
        {
            onCount += count();
            for (int i = 0; i < 16; i++)
            {
                if (((history >> 4 * i) & 0b1) == 0)
                {
                    onCount++;
                }
                else // (((history >> 4 * i) & 0b1) == 1)
                {
                    onCount--;
                }
            }

            for (int i = 0; i < 16; i++)
            {
                Debug.Log(i + ") Switch " + ((((history >> 4 * (15 - i)) & 0b1110) >> 1) + 1) + ", " + (((history >> 4 * (15 - i)) & 0b1) == 1 ? ("on, total " + (++onCount)) : ("off, total " + (--onCount))));
            }
        }
        else
        {
            for (int i = 0; i < trial; i++)
            {
                Debug.Log(i + ") Switch " + ((((history >> 4 * (trial - 1 - i)) & 0b1110) >> 1) + 1) + ", " + (((history >> 4 * (trial - 1 - i)) & 0b1) == 1 ? ("on, total " + (++onCount)) : ("off, total " + (--onCount))));
            }
        }
        
        Debug.Log("---------------");
    }
}
