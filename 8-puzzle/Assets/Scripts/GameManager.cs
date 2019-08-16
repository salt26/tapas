using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Unity.Lobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int m_TeamID = -1;
    void Awake()
    {
        if (LobbyManager.lm != null)
        {
            m_TeamID = LobbyManager.lm.Myself.GetComponent<LobbyPlayerItem>().AssociatedPlayer.TeamID;
            LobbyManager.lm.GetComponent<Canvas>().enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0, 4);
        List<Vector3> policePositions;
        List<Quaternion> policeRotations;
        policePositions = new List<Vector3>
        {
            new Vector3(2f, 0f, 2f),
            new Vector3(135f, 0f, 2f),
            new Vector3(135f, 0f, 135f),
            new Vector3(2f, 0f, 135f)
        };
        policeRotations = new List<Quaternion>
        {
            Quaternion.Euler(0f, 45f, 0f),
            Quaternion.Euler(0f, -45f, 0f),
            Quaternion.Euler(0f, -135f, 0f),
            Quaternion.Euler(0f, 135f, 0f)
        };
        if (m_TeamID == 1)
            NetworkManager.Instance.InstantiatePolice(position: policePositions[r], rotation: policeRotations[r]);
        else if (m_TeamID == 2)
            NetworkManager.Instance.InstantiateThief(position: new Vector3(68.5f, 0f, 68.5f));
        else if (m_TeamID == 3)
            NetworkManager.Instance.InstantiateSupporter(0, position: new Vector3(62.5f, 8f, 68.5f));
        else if (m_TeamID == 4)
            NetworkManager.Instance.InstantiateSupporter(1, position: new Vector3(74.5f, 8f, 68.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
