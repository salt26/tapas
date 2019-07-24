using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public GameObject doorL;
    public GameObject doorR;
    public float distance = 1.333333333f;

    private bool isOpen;
    private float openness;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        openness = 0;
        UpdateDoor();
    }

    // Update is called once per frame
    void Update()
    {
        openness = (1f + Mathf.Sin(Time.time)) / 2f;
        UpdateDoor();
    }

    void UpdateDoor()
    {
        doorL.transform.localPosition = new Vector3(- distance * (1f - openness), 0);
        doorR.transform.localPosition = new Vector3(distance * (1f - openness), 0);
    }
}
