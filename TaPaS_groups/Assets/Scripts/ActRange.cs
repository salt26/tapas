using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActRange : MonoBehaviour
{
    public int switchNumInRange = 0;

    private void OnTriggerStay(Collider other)
    {
        switch(other.tag)
        {
            case "Switch1":
                switchNumInRange = 1;
                break;
            case "Switch2":
                switchNumInRange = 2;
                break;
            case "Switch3":
                switchNumInRange = 3;
                break;
            case "Switch4":
                switchNumInRange = 4;
                break;
            case "Switch5":
                switchNumInRange = 5;
                break;
            case "Switch6":
                switchNumInRange = 6;
                break;
            case "Switch7":
                switchNumInRange = 7;
                break;
            case "Switch8":
                switchNumInRange = 8;
                break;
            default:
                switchNumInRange = 0;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8) // Switch
        {
            switchNumInRange = 0;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && switchNumInRange != 0)
        {
            if (GameManager.instance.trial > 0 && (switchNumInRange - 1) == (int)((GameManager.instance.history & 0b1110) >> 1))
            {
                Debug.Log("같은 스위치를 연속으로 작동시킬 수 없습니다.");
            }
            else
            {
                GameManager.instance.check((uint)switchNumInRange);
            }
        }
    }
}
