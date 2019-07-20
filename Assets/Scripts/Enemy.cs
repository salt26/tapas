using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Image ui;

    bool isTriggered = false;
    

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.gameObject.name.Equals("Player"))
        {
            isTriggered = true;
            ui.color = new Color(ui.color.r, ui.color.g, ui.color.b, 0.2f);
            StatusText.st.Decrease();
            GetComponent<AudioSource>().Play();
        }
    }
}
