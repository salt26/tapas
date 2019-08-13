using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [System.Serializable]
    public class DoorData
    {
        public GameObject door;
        public Vector3 initialLocalPosition;
        public Vector3 finalLocalPosition;
    }

    public DoorData[] doorDatas;
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
            // openness = (1f - Mathf.Cos(Mathf.Min((Time.time - openTime) / openDuration, 1f) * Mathf.PI)) / 2f;
            openness = Mathf.Min((Time.time - openTime) / openDuration, 1f);
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
        for(int i = 0; i < doorDatas.Length; i++)
        {
            doorDatas[i].door.transform.localPosition = openness * doorDatas[i].finalLocalPosition 
                + (1f - openness) * doorDatas[i].initialLocalPosition;
        }
    }
}
