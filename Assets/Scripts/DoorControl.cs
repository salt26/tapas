using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public GameObject doorL;
    public GameObject doorR;
    public float distance = 1.333333333f;
    public float openDuration = 1f;

    private bool isOpen;
    private float openness;
    private float openTime;

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
        if (isOpen)
        {
            openness = (1f - Mathf.Cos(Mathf.Min((Time.time - openTime) / openDuration, 1f) * Mathf.PI)) / 2f;
        }
        else
        {
            openness = 0;
        }
        UpdateDoor();
    }

    public void Open()
    {
        if (!isOpen)
        {
            isOpen = true;
            openTime = Time.time;
        }
    }

    void UpdateDoor()
    {
        doorL.transform.localPosition = new Vector3(- distance * (1f - openness), 0);
        doorR.transform.localPosition = new Vector3(distance * (1f - openness), 0);
    }
}
