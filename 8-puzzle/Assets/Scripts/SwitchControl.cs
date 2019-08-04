using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour
{
    public enum State
    {
        NonActive,
        Active,
        Dead
    }

    public SwitchControl[] connectedSwitch;
    public DoorControl[] connectedDoor;
    public State state;

    [ColorUsageAttribute(true, true)]
    public Color nonActiveColor = new Color(0.0f, 0.0f, 0.0f);
    [ColorUsageAttribute(true, true)]
    public Color activeColor = new Color(60.0f, 40.1f, 5.0f);
    [ColorUsageAttribute(true, true)]
    public Color deadColor = new Color(5.0f, 40.1f, 60.0f);

    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = transform.GetChild(0).GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Color color;
        if(state == State.NonActive)
        {
            color = nonActiveColor;
        }
        else if(state == State.Active)
        {
            color = activeColor;
        }
        else
        {
            color = deadColor;
        }
        material.SetColor("_EmissionColor", color);
    }

    public void Open()
    {
        if(state == State.NonActive) state = State.Active;
    }

    void OnTriggerEnter(Collider other)
    {
        if(state == State.Active)
        {
            for(int i = 0; i < connectedDoor.Length; i++)
            {
                connectedDoor[i].Open();
            }
            for(int i = 0; i < connectedSwitch.Length; i++)
            {
                connectedSwitch[i].Open();
            }
            state = State.Dead;
        }
    }
}
