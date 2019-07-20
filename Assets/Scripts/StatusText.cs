using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusText : MonoBehaviour
{
    public static StatusText st;
    public int remain;

    void Awake()
    {
        st = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "미로 속에서 " + remain + "개의 캡슐을 찾아 터치하세요!";
    }

    public void Decrease()
    {
        if (remain > 1)
        {
            remain--;
            GetComponent<Text>().text = "미로 속에서 " + remain + "개의 캡슐을 찾아 터치하세요!";
        }
        else
        {
            remain = 0;
            GetComponent<Text>().text = "모든 캡슐을 찾았습니다!";
        }
    }
}
