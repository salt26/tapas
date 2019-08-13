using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouch : MonoBehaviour
{
    public float maxDistance = 2f;

    public void Touch()
    {
        if (NetworkManager.Instance == null) return;
        else if (NetworkManager.Instance.IsServer)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, maxDistance))
            {
                GameData.Instance.CollisionEnter(hit.transform);
            }
        }
    }
}
