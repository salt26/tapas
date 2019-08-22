using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DisconnectedCanvas : MonoBehaviour
{
    public void Disconnect()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
