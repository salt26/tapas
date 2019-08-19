using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenetoTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeScenetoMiniGame() {
        SceneManager.LoadScene("Tutorial");
    }
    // Update is called once per frame
}
