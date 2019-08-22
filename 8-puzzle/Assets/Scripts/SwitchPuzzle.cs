using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    public GameObject[] switches;

    private int rotNum;
    private float rotationSpeed;

    public int RotNum
    {
        get
        {
            return rotNum;
        }
    }
    
    void Start()
    {
        rotNum = 0;
        rotationSpeed = 60f;
    }

    void Update()
    {
        if (rotNum == 0) return;

        switches[rotNum - 1].transform.Find("Switch/ApertureSwitchEmission/SwitchEmission").Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        switches[rotNum - 1].transform.Find("Switch/ApertureSwitchEmission/SwitchEmissionCenter").Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    public void SetRotNum(int rotNum)
    {
        this.rotNum = rotNum;
        //Debug.Log("SetRotNum : " + rotNum);
    }
}
