using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouch : MonoBehaviour
{
    public Collider touchArea;
    public List<string> touchableTags;
    private int touchTimer = 0;
    
    void Update()
    {
        if (touchTimer > 0)
        {
            touchTimer--;
        }
    }

    public void Touch()
    {
        BMSLogger.DebugLog("Touch");
        touchTimer = 5;
    }

    private void OnTriggerStay(Collider other)
    {
        if (NetworkManager.Instance == null)
        {
            return;
        }
        else if (NetworkManager.Instance.IsServer && touchTimer > 0 && other != null)
        {
            touchTimer = 0;
            foreach (string tag in touchableTags)
            {
                if (!other.tag.Equals(tag)) continue;

                if (other.tag.Equals("Switch"))
                {
                    BMSLogger.DebugLog("Touch with " + other.name);
                    GameData.Instance.CollisionEnter(other.transform);
                    // TODO: 누른 스위치가 모든 클라이언트에서 보이도록 하기
                }
                if (other.tag.Equals("Box"))
                {
                    // TODO: open box
                }
                if (other.tag.Equals("Thief"))
                {
                    GameManager.instance.Win_TeamID = 1;
                }
            }
        }
    }
}
