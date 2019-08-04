using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    private Color color;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = transform.GetChild(0).GetComponent<Renderer>().material;
        color = new Color();
    }

    // Update is called once per frame
    void Update()
    {
        material.SetColor("_EmissionColor", color);
    }

    void OnTriggerEnter(Collider other)
    {
        GameData.Instance.CollisionEnter(this, other);
    }

    public void SetColor(Color c)
    {
        color = c;
    }
}
