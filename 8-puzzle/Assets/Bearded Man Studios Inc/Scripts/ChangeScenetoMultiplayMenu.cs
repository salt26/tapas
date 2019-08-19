using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenetoMultiplayMenu : MonoBehaviour
{
    public void ChangeScenetoMultiplay() {
        SceneManager.LoadScene("MultiplayerMenu");
    }
}
